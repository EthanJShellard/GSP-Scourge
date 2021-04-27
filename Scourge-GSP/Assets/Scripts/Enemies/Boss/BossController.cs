using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private enum Attack
    {
        SingleShot,
        ShotGun,
        Mellee,
    }

    [Header("General Controls")]
    [SerializeField] private float health = 30.0f;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private GameObject arm;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float viewDistance = 10.0f;
    [Header("Attack Controls")]
    [SerializeField] private float melleeRange = 1.0f;
    [SerializeField] private float shootRange = 10.0f;
    [SerializeField] private float shotSpeed = 5.0f;
    [SerializeField] private float shotSpreadAngle = 45.0f;
    [SerializeField] private float attackTimer = 1.0f;
    [Header("")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private AudioClip roar;
    [Header("Cycle Controls")]
    [SerializeField] private int fastCycleCount; //Number of cycles before break
    [SerializeField] private float breakTimerModifier; //Multiplicative modifier of time between next attack used to create break
    int cycleCounter = 0;

    private float attackTime = .0f;

    private Attack currentAttack;

    private GameObject player;
    private bool facingRight = false;

    private LoadManager lm;
    private Animator anim;
    private AudioSource audioSource;

    private void Start()
    {
        lm = FindObjectOfType<LoadManager>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        arm.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            anim.SetBool("Dead", true);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("BossDeath") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0)
            {
                Destroy(this.gameObject);
                lm.WinToMainMenu();
            }

            return;
        }

        if (player.transform.position.x < gameObject.transform.position.x && facingRight)
        {
            Flip();
        }else if (player.transform.position.x > gameObject.transform.position.x && !facingRight)
        {
            Flip();
        }

        if (Mathf.Abs(transform.position.x - player.transform.position.x) < viewDistance)
        {
            if (attackTime <= .0f)
            {
                currentAttack = GetRandomAttack();
                cycleCounter++;
                switch (currentAttack)
                {
                    case Attack.Mellee:
                        MelleeAttack();
                        break;
                    case Attack.SingleShot:
                        ShootSingleShot();
                        break;
                    case Attack.ShotGun:
                        ShootShotgunShot();
                        break;
                }
                if (cycleCounter == fastCycleCount)
                {
                    cycleCounter = 0;
                    attackTime = attackTimer * breakTimerModifier;
                }
                else 
                {
                    attackTime = attackTimer;
                }
                
            }
            else
            {
                attackTime -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < viewDistance)
        {

            if (player.transform.position.x < gameObject.transform.position.x - melleeRange)
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                anim.SetBool("Walking", true);
            }
            else if (player.transform.position.x > gameObject.transform.position.x + melleeRange)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                anim.SetBool("Walking", true);
            }
            else 
            {
                anim.SetBool("Walking", false);
            }
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    void ShootSingleShot()
    {
        anim.SetBool("Mouth", true);
        audioSource.PlayOneShot(roar);
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < shootRange)
        {
            GameObject firedBullet;
            if (facingRight)
            {
                firedBullet = Instantiate(bullet, new Vector3(transform.position.x + 1f, transform.position.y + 0.35f, .0f), Quaternion.identity);
                firedBullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * shotSpeed;
            }
            else
            {
                firedBullet = Instantiate(bullet, new Vector3(transform.position.x - 1f, transform.position.y + 0.35f, .0f), Quaternion.identity);
                firedBullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * shotSpeed;
            }
        }
        StartCoroutine(StopMouthAfterOneFrame());
    }

    void ShootShotgunShot()
    {
        anim.SetBool("Mouth", true);
        audioSource.PlayOneShot(roar);
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < shootRange)
        {

            GameObject[] firedBullets = new GameObject[3];
            if (facingRight)
            {
                firedBullets[0] = Instantiate(bullet, new Vector3(transform.position.x + 1, transform.position.y + 0.35f, .0f), Quaternion.identity);
                firedBullets[1] = Instantiate(bullet, new Vector3(transform.position.x + 1, transform.position.y + 0.35f, .0f), Quaternion.identity);
                firedBullets[2] = Instantiate(bullet, new Vector3(transform.position.x + 1, transform.position.y + 0.35f, .0f), Quaternion.identity);
                for (int i = -1; i < 2; i++)
                {
                    firedBullets[i+1].transform.rotation = Quaternion.Euler(new Vector3(.0f, .0f, 90.0f + (shotSpreadAngle * i)) * -1);
                    firedBullets[i+1].GetComponent<Rigidbody2D>().velocity = firedBullets[i+1].transform.up * shotSpeed;

                }
            }
            else
            {
                firedBullets[0] = Instantiate(bullet, new Vector3(transform.position.x - 1, transform.position.y + 0.35f, .0f), Quaternion.identity);
                firedBullets[1] = Instantiate(bullet, new Vector3(transform.position.x - 1, transform.position.y + 0.35f , .0f), Quaternion.identity);
                firedBullets[2] = Instantiate(bullet, new Vector3(transform.position.x - 1, transform.position.y + 0.35f , .0f), Quaternion.identity);
                for (int i = -1; i < 2; i++)
                {
                    firedBullets[i + 1].transform.rotation = Quaternion.Euler(new Vector3(.0f, .0f, 90.0f + (shotSpreadAngle * i)));
                    firedBullets[i + 1].GetComponent<Rigidbody2D>().velocity = firedBullets[i+1].transform.up * shotSpeed;

                }
            }

        }
        StartCoroutine(StopMouthAfterOneFrame());
    }

    void MelleeAttack()
    {
        anim.SetBool("Melee", true);        
        StartCoroutine(Punch());
    }

    IEnumerator Punch()
    {
        arm.SetActive(true);
        yield return new WaitForSeconds(.5f);
        arm.SetActive(false);
        anim.SetBool("Melee", false);
    }

    public void Damage(int damage)
    {
        health -= damage;
    }

    Attack GetRandomAttack()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) <= melleeRange) return Attack.Mellee;
        
        Attack nextAttack;
        int randomNumber = Random.Range(0, 2);

        nextAttack = (Attack)randomNumber;

        return nextAttack;
    }

    IEnumerator StopMouthAfterOneFrame() 
    {
        yield return 0;
        anim.SetBool("Mouth", false);
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

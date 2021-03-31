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

    [Header("Movement Controls")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [Header("Attack Controls")]
    [SerializeField] private float melleeDamage = 2.0f;
    [SerializeField] private float melleeRange = 1.0f;
    [SerializeField] private float rangedDamage = 1.0f;
    [SerializeField] private float shootRange = 10.0f;
    [SerializeField] private float shotSpeed = 5.0f;
    [SerializeField] private float shotSpreadAngle = 45.0f;
    [SerializeField] private float shotCooldownTimer = .5f;
    [SerializeField] private float attackTimer = 1.0f;
    [Header("")]
    [SerializeField] private GameObject bullet;

    private float attackTime = .0f;

    private Attack currentAttack;
    private bool currentlyAttacking = false;

    private GameObject player;
    private bool facingRight = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player.transform.position.x < gameObject.transform.position.x && facingRight)
        {
            Flip();
        }else if (player.transform.position.x > gameObject.transform.position.x && !facingRight)
        {
            Flip();
        }

        if (attackTime <= .0f)
        {
            currentAttack = getRandomAttack();
            switch (currentAttack)
            {
                case Attack.Mellee:
                    Debug.Log("Mellee");
                    MelleeAttack();
                    break;
                case Attack.SingleShot:
                    Debug.Log("Single Shot");
                    ShootSingleShot();
                    break;
                case Attack.ShotGun:
                    Debug.Log("Shotgun shot");
                    ShootShotgunShot();
                    break;
            }
            attackTime = attackTimer;
        }else
        {
            attackTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (player.transform.position.x < gameObject.transform.position.x - melleeRange)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        if (player.transform.position.x > gameObject.transform.position.x + melleeRange)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    void ShootSingleShot()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < shootRange)
        {
            GameObject firedBullet;
            if (facingRight)
            {
                firedBullet = Instantiate(bullet, new Vector3(transform.position.x + 2, transform.position.y, .0f), Quaternion.identity);
                firedBullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * shotSpeed;
            }
            else
            {
                firedBullet = Instantiate(bullet, new Vector3(transform.position.x - 2, transform.position.y, .0f), Quaternion.identity);
                firedBullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * shotSpeed;
            }
        }
    }

    void ShootShotgunShot()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < shootRange)
        {

            GameObject[] firedBullets = new GameObject[3];
            if (facingRight)
            {
                firedBullets[0] = Instantiate(bullet, new Vector3(transform.position.x + 2, transform.position.y, .0f), Quaternion.identity);
                firedBullets[1] = Instantiate(bullet, new Vector3(transform.position.x + 2, transform.position.y, .0f), Quaternion.identity);
                firedBullets[2] = Instantiate(bullet, new Vector3(transform.position.x + 2, transform.position.y, .0f), Quaternion.identity);
                for (int i = -1; i < 2; i++)
                {
                    firedBullets[i+1].transform.rotation = Quaternion.Euler(new Vector3(.0f, .0f, 90.0f + (shotSpreadAngle * i)) * -1);
                    firedBullets[i+1].GetComponent<Rigidbody2D>().velocity = firedBullets[i+1].transform.up * shotSpeed;

                }
            }
            else
            {
                firedBullets[0] = Instantiate(bullet, new Vector3(transform.position.x - 2, transform.position.y, .0f), Quaternion.identity);
                firedBullets[1] = Instantiate(bullet, new Vector3(transform.position.x - 2, transform.position.y, .0f), Quaternion.identity);
                firedBullets[2] = Instantiate(bullet, new Vector3(transform.position.x - 2, transform.position.y, .0f), Quaternion.identity);
                for (int i = -1; i < 2; i++)
                {
                    firedBullets[i + 1].transform.rotation = Quaternion.Euler(new Vector3(.0f, .0f, 90.0f + (shotSpreadAngle * i)));
                    firedBullets[i + 1].GetComponent<Rigidbody2D>().velocity = firedBullets[i+1].transform.up * shotSpeed;

                }
            }

        }
    }

    void MelleeAttack()
    {

    }

    Attack getRandomAttack()
    {
        Attack nextAttack;

        int randomNumber = Random.Range(0, 3);

        nextAttack = (Attack)randomNumber;

        return nextAttack;
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

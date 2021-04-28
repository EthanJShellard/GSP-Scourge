using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class containing core player attributes such as HP.
/// </summary>
public class Player : MonoBehaviour
{
    private Respawn respawn;

    bool facingRight = true;
    [SerializeField] int HP; //Only assigned to 5 for testing
    [SerializeField] int maxHP;

    [SerializeField] private Vector3 defaultSpawnPoint;
    private LoadManager lm;

    [SerializeField] private float iFramesTimer = 1.0f; //set to 0.5 for testing
    [SerializeField] private bool canBeHit = true;
    [SerializeField] private float flashInterval = .15f;
    private float iTimeLeft;

    private Material mat;
    private Color[] colors = new Color[2];
    private HealthBar healthBar;

    private bool knockBackActive = false;
    private Vector3 colliderPos;

    private Animator anim;

    public bool getKnockBackState() { return knockBackActive; }
    public void setKnockBackState( bool _knockBack) { knockBackActive = _knockBack; }
    public Vector3 getColliderTransform() { return colliderPos; }

    private void Start()
    {
        anim = GetComponent<Animator>();
        iTimeLeft = iFramesTimer;

        HP = maxHP;
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxValue(maxHP);
        healthBar.SetValue(HP);
        HealFull();

        lm = FindObjectOfType<LoadManager>();
        mat = GetComponent<SpriteRenderer>().material;
        colors[0] = mat.color;
        colors[1] = Color.red;

        
    }

    private void Update()
    {
        if (iTimeLeft <= 0)
        {
            mat.color = colors[0];
            canBeHit = true;
        }
        else
        {
            iTimeLeft -= Time.deltaTime;
            canBeHit = false;
        }
    }

    IEnumerator PlayerFlash(float time, float intervalTime)
    {

        float elapsedTime = .0f;
        int index = 0;

        while (elapsedTime < time)
        {
            mat.color = colors[index % 2];
            elapsedTime += intervalTime;
            index++;
            yield return new WaitForSeconds(intervalTime);
        }
        //knockBackActive = false;
    }

    public void HealFull() 
    {
        HP = maxHP;
        //Update health bar
        healthBar.SetValue(HP);
    }

    /// <summary>
    /// Damage the player. Will cause death of HP is reduced below 0.
    /// </summary>
    /// <param name="n">Amount of damage inflicted.</param>
    public void Damage(int n)
    {
        if (canBeHit)
        {
            anim.SetBool("Dead", false);
            knockBackActive = true;
            
            iTimeLeft = iFramesTimer;
            HP -= n;
            if (HP <= 0)
            {
                anim.SetBool("Dead", true);
                Kill();
            }
            StartCoroutine(PlayerFlash(iFramesTimer, flashInterval));
            //Update healthbar
            healthBar.SetValue(HP);
        }
    }

    public void Damage(int n, Vector3 colPos)
    {
        if (canBeHit)
        {
            anim.SetBool("Dead", false);
            knockBackActive = true;
            colliderPos = colPos;

            iTimeLeft = iFramesTimer;
            HP -= n;
            if (HP <= 0)
            {
                anim.SetBool("Dead", true);
                Kill();
               
            }
            StartCoroutine(PlayerFlash(iFramesTimer, flashInterval));
            //Update healthbar
            healthBar.SetValue(HP);
        }
    }

    /// <summary>
    /// Call to trigger player death.
    /// </summary>
    public void Kill()
    {
        //reset to checkpoint
        canBeHit = false;
        iTimeLeft = float.MaxValue;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerAttackStick>().enabled = false;
        GetComponent<PlayerShoot>().enabled = false;
        GetComponent<PlayerShoot>().HideArms();
        lm.ReloadToCheckpoint();
    }

    /// <summary>
    /// Flips the player sprite to the opposite direction in the x axis
    /// </summary>
    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    /// <summary>
    /// Returns whether or not the player sprite is facing to the right.
    /// </summary>
    public bool IsFacingRight()
    {
        return facingRight;
    }

    public void SetRespawnPoint(Respawn r) 
    {
        //Deactive previous censer animation
        if(respawn != null)respawn.GetComponent<Animator>().SetBool("On", false);
        respawn = r;

        //Notify LoadManager
        lm.SetRespawnPosition(r.transform.position);

        //Activate censer animation
        r.GetComponent<Animator>().SetBool("On", true);

    }
}

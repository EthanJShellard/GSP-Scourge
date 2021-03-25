using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaguedPersonCrossbow : Enemy
{
    [SerializeField] private float aggroRange;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackCooldown; //Time between attacks
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject crossbowBolt;

    float attackTimer;
    bool attackReady;
    SpriteRenderer sprite;
    Animator animator;

    private void Start()
    {
        attackReady = false;
        attackTimer = attackCooldown;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!attackReady)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                attackReady = true;
            }
        }
            
    }

    private void FixedUpdate()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, aggroRange, playerLayer);
        if (player.Length > 0) 
        {
            Vector3 diff = player[0].transform.position - transform.position;

            //Face player
            if (diff.x > 0) sprite.flipX = true;
            else sprite.flipX = false;

            if (attackReady)
            {
                animator.SetBool("Attack", true);
                attackReady = false;
                attackTimer = attackCooldown;
                StartCoroutine(DisableAttackAfterOneFrame());
                //Spawn Projectile(Maybe change to object pooling)
                GameObject cb = Instantiate(crossbowBolt);
                cb.transform.position = transform.position;
                cb.GetComponent<Bolt>().SetDirection(diff.x > 0);
            }
        }
    }

    public override void Damage(int n, Vector3 impactDirection)
    {
        hitPoints -= n;
        if (hitPoints <= 0)
        {

            Kill();
        }
    }

    public override void Kill()
    {
        Destroy(this.gameObject);
    }

    IEnumerator DisableAttackAfterOneFrame ()
    {
        yield return 0;

        animator.SetBool("Attack", false);
    }
}

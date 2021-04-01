using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class PlaguedPersonCrossbow : Enemy
{
    [SerializeField] private float aggroRange;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackCooldown; //Time between attacks
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject crossbowBolt;
    [SerializeField] private float leftPatrolDistance;
    [SerializeField] private float rightPatrolDistance;
    [SerializeField] private float walkSpeed;

    float rightBoundX;
    float leftBoundX;
    bool doPatrol;
    bool patrolDir = true;
    Rigidbody2D rb;

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
        rb = GetComponent<Rigidbody2D>();

        doPatrol = ((leftPatrolDistance != 0) || (rightPatrolDistance != 0));
        rightBoundX = transform.position.x + rightPatrolDistance;
        leftBoundX = transform.position.x - leftPatrolDistance;
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
            animator.SetBool("Walking", false);
            //Face player
            sprite.flipX = (diff.x > 0);

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
        else if (doPatrol) //Patrol
        {
            animator.SetBool("Walking", true);
            if (patrolDir) //Going right
            {
                sprite.flipX = true;
                if (transform.position.x > rightBoundX) 
                {
                    patrolDir = !patrolDir;
                }

                Vector3 vel = rb.velocity;
                vel.x = walkSpeed;
                rb.velocity = vel;
            }
            else //Going left
            {
                sprite.flipX = false;
                if (transform.position.x < leftBoundX) 
                {
                    patrolDir = !patrolDir;
                    
                }

                Vector3 vel = rb.velocity;
                vel.x = -walkSpeed;
                rb.velocity = vel;
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




#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        rightBoundX = transform.position.x + rightPatrolDistance;
        leftBoundX = transform.position.x - leftPatrolDistance;
    }

    [DrawGizmo(GizmoType.Selected)]
    static void DrawBounds(PlaguedPersonCrossbow ppc, GizmoType gt)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ppc.transform.position, new Vector3(ppc.leftBoundX, ppc.transform.position.y, ppc.transform.position.z));
        Gizmos.DrawLine(ppc.transform.position, new Vector3(ppc.rightBoundX, ppc.transform.position.y, ppc.transform.position.z));
    }
#endif
}

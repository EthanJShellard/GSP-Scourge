using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Main script of melee plagued person.
/// </summary>
public class PlaguedPersonMelee : Enemy
{
    [SerializeField] private int attackDamage;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float aggroRange;
    [SerializeField] private LayerMask playerLayer;
    /// <summary>
    /// Horizontal distance from the player that this enemy will aim for before stopping to attack
    /// </summary>
    [SerializeField] private float desiredRange;
    [SerializeField] private float bloodForce;

    private Rigidbody2D rb;
    bool attack = false;
    int attackStateHash;
    Animator animator;
    ParticleSystem bloodParticleSystem;
    ParticleSystemForceField forceField;
    SpriteRenderer spriteRenderer;

    private enum MoveDirection { NONE, LEFT, RIGHT }
    MoveDirection moveDir = MoveDirection.NONE;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bloodParticleSystem = GetComponentInChildren<ParticleSystem>();
        forceField = GetComponent<ParticleSystemForceField>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
        
    }

    private void FixedUpdate()
    {
        if (!attack)
        {

            Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, aggroRange, playerLayer);
            if (player.Length > 0)
            {
                PathToPlayer(player[0].transform);
            }
            else
            {
                moveDir = MoveDirection.NONE;
            }


            Vector2 vel = rb.velocity;
            switch (moveDir)
            {
                case MoveDirection.NONE:
                    animator.SetBool("Walking", false);
                    rb.velocity.Set(0, 0);
                    break;
                case MoveDirection.LEFT:
                    animator.SetBool("Walking", true);
                    vel.x = -walkSpeed;
                    rb.velocity = vel;
                    break;
                case MoveDirection.RIGHT:
                    animator.SetBool("Walking", true);
                    vel.x = walkSpeed;
                    rb.velocity = vel;
                    break;
            }

        }
        else 
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                attack = false;
                animator.SetBool("Attacking", false);
            }
        }
    }

    /// <summary>
    /// Determine how to move towards the player. Currently just moves in their direction regardless of obstacles. This enemy cannot jump.
    /// </summary>
    /// <param name="player">The transform from the target player GameObject</param>
    private void PathToPlayer(Transform player) 
    {
        float diff = player.position.x - transform.position.x;
        if (diff > desiredRange) //If outside of melee range
        {
            moveDir = MoveDirection.RIGHT;
            spriteRenderer.flipX = true;
        }
        else if (diff < -desiredRange)
        {
            moveDir = MoveDirection.LEFT;
            spriteRenderer.flipX = false;
        }
        else 
        {
            //Stop Walking
            moveDir = MoveDirection.NONE;
            //Start Attacking
            animator.SetBool("Attacking", true);
            spriteRenderer.flipX = (diff > 0);  
            attack = true;
            //TODO ADD ATTACK IMPLEMENTATION. LIKELY INVOLVING SOME KIND OF MOVING TRIGGER?
        }
    }

    override public void Kill() 
    {
        /*
            TODO:
            Blood Effect
            Corpse?? Delete Enemy?
         */
        Destroy(this.gameObject);
    }

    public override void Damage(int n, Vector3 direction)
    {
        direction.Normalize();

        //Orient blood particles to away from the attack.
        if (direction.x <= 0) forceField.directionX = bloodForce * direction.x;
        else forceField.directionX = bloodForce * direction.x;

        forceField.directionY = direction.y * bloodForce;

        bloodParticleSystem.Play();        

        hitPoints -= n;
        if (hitPoints <= 0) 
        {

            Kill();
        }
    }

    public void DamageFrameStart() 
    {
        Debug.Log("Start");
    }

    public void DamageFramesEnd() 
    {
        Debug.Log("End");
    }

    //Default enemy values
    private void Reset()
    {
        hitPoints = 2;
        attackDamage = 1;
        walkSpeed = 1;
        aggroRange = 10;
    }

}

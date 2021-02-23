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

    private Rigidbody2D rb;
    bool attack = false;
    Animator animator;
    ParticleSystem bloodParticleSystem;
    SpriteRenderer spriteRenderer;

    private enum MoveDirection { NONE, LEFT, RIGHT }
    MoveDirection moveDir = MoveDirection.NONE;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bloodParticleSystem = GetComponentInChildren<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();       
    }

    private void FixedUpdate()
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
                rb.velocity.Set(0,0);
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

    /// <summary>
    /// Determine how to move towards the player. Currently just moves in their direction regardless of obstacles. This enemy cannot jump.
    /// </summary>
    /// <param name="player">The transform from the target player GameObject</param>
    private void PathToPlayer(Transform player) 
    {
        if (player.position.x - transform.position.x > desiredRange) //If outside of melee range
        {
            moveDir = MoveDirection.RIGHT;
            spriteRenderer.flipX = true;
        }
        else if (player.position.x - transform.position.x < -desiredRange)
        {
            moveDir = MoveDirection.LEFT;
            spriteRenderer.flipX = false;
        }
        else 
        {
            moveDir = MoveDirection.NONE;
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
        //Orient blood particles to away from the attack.
        if (direction.x <= 0) bloodParticleSystem.transform.rotation.eulerAngles.Set(0,0,180);
        else bloodParticleSystem.transform.rotation.eulerAngles.Set(0, 0, 0);
        bloodParticleSystem.Play();        

        hitPoints -= n;
        if (hitPoints <= 0) 
        {

            Kill();
        }
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

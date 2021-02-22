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


    private enum MoveDirection { NONE, LEFT, RIGHT }
    MoveDirection moveDir = MoveDirection.NONE;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
            if (transform.localScale.x < 0) 
            {
                Vector2 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
            
        }
        else if (player.position.x - transform.position.x < -desiredRange)
        {
            moveDir = MoveDirection.LEFT;
            if (transform.localScale.x > 0)
            {
                Vector2 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
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
    }

    public override void Damage(int n)
    {
        hitPoints -= n;
        Debug.Log(hitPoints);
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

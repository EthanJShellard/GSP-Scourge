using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float AttackSlowdownFactor;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpTime;
    [SerializeField] private float knockbackForce;
    [SerializeField] private AudioSource footstepSource;

    private Rigidbody2D rb;
    private Animator animator;

    private float horizontalInput;
    private bool grounded = false;
    private float jumpTimeCounter;
    private bool isJumping;
    private bool keyReleased;
    private float attackSlowdown;
    private bool isShooting = false;

    const float groundedRadius = .2f;
    private Player player;

    private bool knocked = false;

    void Start()
    {
        keyReleased = true;
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackSlowdown = 1f;
    }

    void Update()
    {
        //jump controls
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);

        if (grounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && keyReleased)
            {
                animator.SetBool("Jumping", true);
                isJumping = true;
                jumpTimeCounter = jumpTime;

                Vector2 vel = rb.velocity;
                vel.y = jumpForce;
                rb.velocity = vel;

                keyReleased = false;
            }
            else if (!isJumping)
            {
                animator.SetBool("Jumping", false);
            }
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                Vector2 vel = rb.velocity;
                vel.y = jumpForce;
                rb.velocity = vel;

                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            keyReleased = true;
        }
        //end jump controls

    }

    void FixedUpdate()
    {
        if (!player.getKnockBackState())
        {
            if (!isShooting) 
            {
                knocked = false;
                //horizontal movement
                horizontalInput = Input.GetAxis("Horizontal");
                rb.velocity = new Vector2(horizontalInput * speed * attackSlowdown, rb.velocity.y);

                if (horizontalInput != 0)
                {
                    animator.SetBool("Running", true);
                    if (!footstepSource.isPlaying) 
                    {
                        footstepSource.loop = true;
                        footstepSource.Play();
                    }
                    
                }
                else
                {
                    animator.SetBool("Running", false);
                    footstepSource.loop = false;
                }

                if (horizontalInput > 0 && !player.IsFacingRight())
                {
                    player.Flip();
                }
                else if (horizontalInput < 0 && player.IsFacingRight())
                {
                    player.Flip();
                }
                //end horizontal movement
            }
        }
        else
        {
            if (!knocked)
            {
                rb.velocity = new Vector2(.0f, .0f);

                if (player.getColliderTransform().x > transform.position.x)
                {
                    rb.AddForce(transform.right * -knockbackForce * 5);
                    rb.AddForce(transform.up * knockbackForce * 5);
                }
                else
                {
                    rb.AddForce(transform.right * knockbackForce * 5);
                    rb.AddForce(transform.up * knockbackForce * 5);
                }
                knocked = true;
            }
            else
            {
                grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
                if (grounded)
                {
                    player.setKnockBackState(false);
                }
            }
        }

    }

    public void SetAttacking(bool attacking) 
    {
        if (attacking)
        {
            attackSlowdown = AttackSlowdownFactor;
        }
        else 
        {
            attackSlowdown = 1f;
        }
    }

    public void SetShooting(bool shooting) 
    {
        isShooting = shooting;
        animator.SetBool("Shooting", shooting);
        //if (isShooting) animator.SetBool("Running", false);
    }
}

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

    private Rigidbody2D rb;
    private Animator animator;

    private float horizontalInput;
    private bool grounded = false;
    private float jumpTimeCounter;
    private bool isJumping;
    private bool keyReleased;
    private float attackSlowdown;

    const float groundedRadius = .2f;
    private Player player;

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

        if (grounded && Input.GetKeyDown(KeyCode.Space) && keyReleased)
        {
            animator.SetBool("Jumping", true);
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            keyReleased = false;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
                animator.SetBool("Jumping", false);
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
        //horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed * attackSlowdown, rb.velocity.y);

        if (horizontalInput != 0)
        {
            animator.SetBool("Running", true);
        }
        else 
        {
            animator.SetBool("Running", false);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    [Range(0, .3f)] [SerializeField] private float moveSmoothing = .05f;
    [SerializeField] private float JumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody2D rb;

    private float horizontalInput;
    private bool jump = false;
    private bool grounded = false;

    const float groundedRadius = .2f;
    private Vector3 baseVelocity = Vector3.zero;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
        if (Input.GetButtonUp("Jump"))
            jump = false;

    }

    void FixedUpdate()
    {

        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }

        Vector3 targetVelocity = new Vector2(horizontalInput * speed * Time.fixedDeltaTime * 10, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref baseVelocity, moveSmoothing);

        if (horizontalInput > 0 && !facingRight)
        {
            FlipPlayer();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            FlipPlayer();
        }

        if (grounded && jump)
        {
            grounded = false;
            rb.AddForce(new Vector2(.0f, JumpForce));
        }

    }

    private void FlipPlayer()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}

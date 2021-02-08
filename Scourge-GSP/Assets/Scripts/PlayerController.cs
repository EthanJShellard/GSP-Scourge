using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    [Range(0, .3f)] [SerializeField] private float moveSmoothing = .05f;

    private Rigidbody2D rb;
    private float horizontalInput;
    private Vector3 baseVelocity = Vector3.zero;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
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
    }

    private void FlipPlayer()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}

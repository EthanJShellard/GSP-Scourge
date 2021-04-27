using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    [SerializeField] private int damage = 2;
    [SerializeField] private float bulletLifetime = 1;

    private float lifeTimeLeft;
    private Rigidbody2D rb2d;
    private bool facingRight = false;
    private bool flippedY = false;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        lifeTimeLeft = bulletLifetime;
    }

    private void Update()
    {
        float angle = Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (rb2d.velocity.x < 0 && !flippedY)
        {
            Flip();
            FlipY();
        }

        if (lifeTimeLeft <= .0f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            lifeTimeLeft -= Time.deltaTime;
        }
    }

    void FlipY()
    {
        flippedY = true;
        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<Enemy>().Damage((int)damage, GetComponent<Rigidbody2D>().velocity);
        }
        else if (col.CompareTag("Boss"))
        {
            col.gameObject.GetComponent<BossController>().Damage((int)damage);
        }
        else if (col.isTrigger) 
        {
            return; //Early return to stop projectile destroying itself on other triggers
        }
        Destroy(this.gameObject);
    }
}

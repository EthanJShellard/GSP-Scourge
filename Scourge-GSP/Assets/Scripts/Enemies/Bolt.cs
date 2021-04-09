using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] float knockbackForce;

    public void SetDirection(bool goRight) 
    {
        if (goRight)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
        else 
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Damage(damage, gameObject.transform);

            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50f, 2f), ForceMode2D.Impulse);

            Destroy(this.gameObject); //Pooling would be better to use. Maybe switch to that
        }
        else if (!collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }


}

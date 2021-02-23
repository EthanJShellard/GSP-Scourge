using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    [SerializeField] private float damage = 2;
    [SerializeField] private float bulletLifetime = 1;

    private float lifeTimeLeft;

    private void Start()
    {
        lifeTimeLeft = bulletLifetime;
    }

    private void Update()
    {
        if (lifeTimeLeft <= .0f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            lifeTimeLeft -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().Damage(2, GetComponent<Rigidbody2D>().velocity);
        }
    }
}

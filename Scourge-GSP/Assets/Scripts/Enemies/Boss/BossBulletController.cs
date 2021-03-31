using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletController : MonoBehaviour
{

    [SerializeField] private int damage = 1;
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player>().Damage((int)damage);
        }
        else if (col.isTrigger)
        {
            return; //Early return to stop projectile destroying itself on other triggers
        }
        Destroy(this.gameObject);
    }

}

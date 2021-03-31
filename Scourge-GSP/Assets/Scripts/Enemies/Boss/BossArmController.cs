using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArmController : MonoBehaviour
{

    [SerializeField] private int damage = 5;

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player>().Damage(damage);
        }

    }

}

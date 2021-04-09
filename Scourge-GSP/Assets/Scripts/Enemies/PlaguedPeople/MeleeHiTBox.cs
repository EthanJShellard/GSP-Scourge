using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHiTBox : MonoBehaviour
{

    [SerializeField] private int damageDealt;

    public void SetDamage(int dam) 
    {
        damageDealt = dam;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Player p = col.gameObject.GetComponent<Player>();
            p.Damage(damageDealt, gameObject.transform);
        }
    }
}

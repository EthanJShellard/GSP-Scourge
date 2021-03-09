using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHiTBox : MonoBehaviour
{

    [SerializeField] private int damageDealt;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("here");
            Player p = col.gameObject.GetComponent<Player>();
            p.Damage(damageDealt);
        }
    }
}

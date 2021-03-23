//Simple bit of code that allows the game to add checkpoints and take away the ones that are no longer needed. 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Respawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().SetRespawnPoint(this);
            //Activate censer animation
            GetComponent<Animator>().SetBool("On", true);
        }
    }

}

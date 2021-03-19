//Simple bit of code that allows the game to add checkpoints and take away the ones that are no longer needed. 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Respawn : MonoBehaviour
{
    public Transform player;
    public Transform respawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.transform.position = respawnPoint.transform.position;
        }
    }
}

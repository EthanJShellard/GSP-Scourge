//Simple bit of code that allows the game to add checkpoints and take away the ones that are no longer needed. 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Respawn : MonoBehaviour
{
    public bool isActive;
    public Transform respawnPoint;

    void Start()
    {
        isActive = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SetRespawnPoint();
        }
    }

    void SetRespawnPoint()
    {
        isActive = true;
        transform.position = respawnPoint.position;
    }
}

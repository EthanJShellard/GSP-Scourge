﻿//Simple bit of code that allows the game to add checkpoints and take away the ones that are no longer needed. 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Respawn : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip activateSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!GetComponent<Animator>().GetBool("On"))
            {
                audioSource.PlayOneShot(activateSound);
                collision.GetComponent<Player>().SetRespawnPoint(this);
                collision.GetComponent<Player>().HealFull();
            }
        }
    }
}

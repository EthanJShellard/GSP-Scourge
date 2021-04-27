using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    [SerializeField] AudioClip BossMusic;
    private bool switched = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !switched)
        {
            AudioSource aus = GetComponent<AudioSource>();
            aus.Stop();
            aus.PlayOneShot(BossMusic);
            switched = true;
        }
    }
}

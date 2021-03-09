using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class containing core player attributes such as HP.
/// </summary>
public class Player : MonoBehaviour
{

    bool facingRight = true;

    int HP = 5; //Only assigned to 5 for testing

    [SerializeField] public Transform player;
    [SerializeField] public Transform spawnPoint;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Kill();
        }
    }

    /// <summary>
    /// Damage the player. Will cause death of HP is reduced below 0.
    /// </summary>
    /// <param name="n">Amount of damage inflicted.</param>
    public void Damage(int n) 
    {
       HP -= n;
        if (HP < 0) 
        {
            Kill();
        }
    }

    /// <summary>
    /// Call to trigger player death.
    /// </summary>
    public void Kill()
    {
        //KILL
        player.transform.position = GameObject.FindWithTag("Respawn").transform.position;
    }

    /// <summary>
    /// Flips the player sprite to the opposite direction in the x axis
    /// </summary>
    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    /// <summary>
    /// Returns whether or not the player sprite is facing to the right.
    /// </summary>
    public bool IsFacingRight() 
    {
        return facingRight;
    }
}

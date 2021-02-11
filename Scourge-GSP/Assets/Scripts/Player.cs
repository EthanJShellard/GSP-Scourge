using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class containing core player attributes such as HP.
/// </summary>
public class Player : MonoBehaviour
{
    int HP = 5; //Only assigned to 5 for testing

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
        Debug.Log("Player Killed");
    }
}

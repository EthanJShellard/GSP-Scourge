using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract Enemy superclass. Contains functions needed for all standard enemies.
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int hitPoints; //Hit Points, Health Points

    /// <summary>
    /// Deal n damage to this enemy.
    /// </summary>
    public abstract void Damage(int n);

    /// <summary>
    /// Kill this enemy/ cause it to die.
    /// </summary>
    public abstract void Kill();
}

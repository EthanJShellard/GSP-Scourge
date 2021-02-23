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
    /// 
    /// </summary>
    /// <param name="n">Amount of damage to deal</param>
    /// <param name="impactDirection">Direction the damage is travelling in. (e.g. the normalised velocity of an arrow)</param>
    public abstract void Damage(int n, Vector3 impactDirection);

    /// <summary>
    /// Kill this enemy/ cause it to die.
    /// </summary>
    public abstract void Kill();
}

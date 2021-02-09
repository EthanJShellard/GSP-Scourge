using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaguedPersonMelee : Enemy
{
    [SerializeField] private int attackDamage;
    [SerializeField] private int walkSpeed;
    [SerializeField] private int aggroRange;
    [SerializeField] private LayerMask playerLayer;

    private void FixedUpdate()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, aggroRange, playerLayer);
        if (player.Length > 0) 
        {
            FindPathToPlayer(player[0].transform);
        }
    }

    private void FindPathToPlayer(Transform player) 
    {
        
    }

    override public void Kill() 
    {
        /*
            TODO:
            Blood Effect
            Corpse?? Delete Enemy?
         */
    }

    public override void Damage(int n)
    {
        hitPoints -= n;

        if (hitPoints <= 0) 
        {
            Kill();
        }
    }

    //Default enemy values
    private void Reset()
    {
        hitPoints = 2;
        attackDamage = 1;
        walkSpeed = 1;
        aggroRange = 10;
    }

}

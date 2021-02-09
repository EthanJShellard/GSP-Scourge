using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaguedPersonMelee : Enemy
{
    [SerializeField] private int attackDamage;
    [SerializeField] private int walkSpeed;
    [SerializeField] private int aggroRange;


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

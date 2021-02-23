using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackStick : MonoBehaviour
{
    private float timeAttack = 0;
    [SerializeField] float startTimeAttack;

    [SerializeField] Transform attackPos;
    [SerializeField] LayerMask enemies;
    [SerializeField] float attackRange;
    [SerializeField] int damage;

    void Update()
    {
        if(timeAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().Damage(damage, enemiesToDamage[i].transform.position - transform.position);
                }
                timeAttack = startTimeAttack;
            }
        }
        else
        {
            timeAttack -= Time.deltaTime;
        }
    }
}

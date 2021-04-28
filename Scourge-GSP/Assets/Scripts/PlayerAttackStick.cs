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
    [SerializeField] AudioClip attackSound;

    private PlayerController pc;
    private Animator anim;
    private AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(timeAttack <= 0 && !pc.GetShooting())
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetBool("Attacking", true);
                pc.SetAttacking(true);
                audioSource.PlayOneShot(attackSound);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemies);

                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].gameObject.CompareTag("Boss"))
                    {
                        enemiesToDamage[i].GetComponent<BossController>().Damage(damage);
                    }
                    else
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().Damage(damage, enemiesToDamage[i].transform.position - transform.position);
                    }
                }
                timeAttack = startTimeAttack;
            }
            else 
            {
                pc.SetAttacking(false);
            }
        }
        else
        {
            anim.SetBool("Attacking", false);
            timeAttack -= Time.deltaTime;
        }
    }
}

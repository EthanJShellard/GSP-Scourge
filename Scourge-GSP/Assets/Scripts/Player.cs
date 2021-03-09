using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class containing core player attributes such as HP.
/// </summary>
public class Player : MonoBehaviour
{

    bool facingRight = true;
    [SerializeField] int HP; //Only assigned to 5 for testing

    [SerializeField] private Transform player;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxHP;

    [SerializeField] private float iFramesTimer = 0.5f; //set to 0.5 for testing
    [SerializeField] bool canBeHit = true;
    private float iTimeLeft;

    private void Start()
    {
        iTimeLeft = iFramesTimer;
        HP = maxHP;
    }

    private void Update()
    {
        if (HP <= 0)
        {
            Kill();
        }
        if (Input.GetKey(KeyCode.Z))
        {
            Kill();
        }

        if (iTimeLeft <= 0)
        {
            canBeHit = true;
        }
        else
        {
            iTimeLeft -= Time.deltaTime;
            canBeHit = false;
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
        HP = maxHP;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyMeleeHitBox")
        {
            Debug.Log("here");
            if (canBeHit)
            {
                Debug.Log("here2");
                PlaguedPersonMelee pm = collision.gameObject.GetComponent<PlaguedPersonMelee>();
                Damage(pm.GetDamageDealt());
                iTimeLeft = iFramesTimer;
            }
        }
    }
}

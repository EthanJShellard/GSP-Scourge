using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float bulletSpeed;
    [Header("Time between attacks")]
    [SerializeField] private float AttackTimer;

    [Header("Ability Visuals")]
    [SerializeField] private Image shootImage1;

    private float currentTime = 0.0f;
    private Vector2 lookDirection;
    private Player player;

    private void Start()
    {
        shootImage1.fillAmount = 0;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookDirection.Normalize();

        if (currentTime <= 0.0f)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (lookDirection.x >= 0 != transform.localScale.x >= 0) player.Flip();
                FireProjectile();
                shootImage1.fillAmount = 1;
                currentTime = AttackTimer;
            }
        }
        else
        {
            shootImage1.fillAmount -= 1 / AttackTimer * Time.deltaTime;
            currentTime -= Time.deltaTime;

            if (shootImage1.fillAmount <= 0)
            {
                shootImage1.fillAmount = 0;
            }

        }
    }

    private void FireProjectile()
    {
        GameObject firedProjectile = Instantiate(projectile, shootPoint.position, Quaternion.identity);

        Rigidbody2D rigBod = firedProjectile.GetComponent<Rigidbody2D>();

        rigBod.velocity = new Vector2(lookDirection.x, lookDirection.y) * bulletSpeed;

        if (rigBod.velocity.x < 0)
        {
            firedProjectile.GetComponent<SpriteRenderer>().flipX = true;
        }

    }

}

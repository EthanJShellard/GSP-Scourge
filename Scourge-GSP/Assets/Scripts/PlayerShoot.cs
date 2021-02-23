using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float bulletSpeed;
    [Header("Time between attacks")]
    [SerializeField] private float AttackTimer;

    private float currentTime = 0.0f;
    private Vector2 lookDirection;
    private Player player;

    private void Start()
    {
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
                currentTime = AttackTimer;
            }
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }

    private void FireProjectile()
    {
        GameObject firedProjectile = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        firedProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(lookDirection.x, lookDirection.y) * bulletSpeed;
    }

}

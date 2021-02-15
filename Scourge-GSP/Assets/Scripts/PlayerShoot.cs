using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float bulletSpeed;

    private Vector2 lookDirection;
    private bool facingRight = false;

    private void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookDirection.Normalize();

        if (Input.GetMouseButtonDown(0))
        {
            if (lookDirection.x >= 0 != transform.localScale.x >= 0) Flip();
            FireProjectile();
        }

    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void FireProjectile()
    {
        GameObject firedProjectile = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        firedProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(lookDirection.x, lookDirection.y) * bulletSpeed;
    }

}

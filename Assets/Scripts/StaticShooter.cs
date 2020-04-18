using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShooter : ShooterBase
{
    public Vector2 shootDirection;

    public override void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,Vector3.zero,Quaternion.identity,FindObjectOfType<BulletHolder>().transform);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * shootSpeed;
    }
}

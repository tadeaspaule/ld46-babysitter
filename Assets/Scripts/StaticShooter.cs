using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShooter : ShooterBase
{
    public override void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,Vector3.zero,Quaternion.identity,FindObjectOfType<BulletHolder>().transform);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = GetShootDirection() * shootSpeed;
    }

    Vector2 GetShootDirection()
    {
        float rads = (Mathf.PI*2) * (transform.rotation.eulerAngles.z / 360f) - Mathf.PI/2;
        return new Vector2(
            Mathf.Cos(rads),
            Mathf.Sin(rads)
        );
    }
}

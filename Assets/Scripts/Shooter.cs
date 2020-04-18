using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : ShooterBase
{
    Carrier target;
    
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Carrier>();
    }

    public override void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,Vector3.zero,Quaternion.identity,FindObjectOfType<BulletHolder>().transform);
        bullet.transform.position = transform.position;
        Vector3 direction = (target.transform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * shootSpeed;
    }
}

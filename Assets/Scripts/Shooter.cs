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

    void Update()
    {
        BaseUpdate();
        float dif = Mathf.Abs(target.transform.position.x - transform.position.x);
        if (target.transform.position.x > transform.position.x && dif > 1.5f) {
            // look right
            spriteRenderer.sprite = sprites[1];
        }
        else if (target.transform.position.x < transform.position.x && dif > 1.5f) {
            // look left
            spriteRenderer.sprite = sprites[2];
        }
        else {
            // default look
            spriteRenderer.sprite = sprites[0];
        }
    }

    public override void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,Vector3.zero,Quaternion.identity,FindObjectOfType<BulletHolder>().transform);
        bullet.transform.position = transform.position;
        Vector3 direction = (target.transform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * shootSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootFrequency = 1f;
    public float shootSpeed = 6f;
    Carrier target;
    float timer = 0f;
    bool frozen = false;
    
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Carrier>();
    }

    // Update is called once per frame
    void Update()
    {
        if (frozen) return;
        timer += Time.deltaTime;
        if (timer > shootFrequency) {
            timer = 0f;
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,Vector3.zero,Quaternion.identity,FindObjectOfType<BulletHolder>().transform);
        bullet.transform.position = transform.position;
        Vector3 direction = (target.transform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * shootSpeed;
    }

    public void ToggleFreeze(bool value)
    {
        frozen = value;
    }
}

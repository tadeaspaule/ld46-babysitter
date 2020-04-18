using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : ShooterBase
{
    public SpriteRenderer laserSR;
    public BoxCollider2D laserBox2D;
    public float shootDelay = 0.6f;
    float laserDuration = 0.1f;

    Color hiddenLaser = new Color(1f,1f,1f,0f);
    Color visibleLaser = new Color(1f,1f,1f,1f);
    Color warningLaser = new Color(1f,1f,1f,0.3f);
    Carrier target;

    void Start()
    {
        target = FindObjectOfType<Carrier>();
    }
    
    
    public override void Shoot()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float rads = Mathf.Acos(direction.x);
        if (direction.y < 0f) rads = Mathf.PI - rads;
        rads -= Mathf.PI / 2;
        transform.rotation = Quaternion.Euler(0f,0f,180f * (rads / Mathf.PI));
        laserSR.color = warningLaser;
        StartCoroutine(DelayedShot());
    }

    IEnumerator DelayedShot()
    {
        yield return new WaitForSeconds(shootDelay);
        laserSR.color = visibleLaser;
        laserBox2D.enabled = true;
        StartCoroutine(DelayedHideLaser());
    }

    IEnumerator DelayedHideLaser()
    {
        yield return new WaitForSeconds(laserDuration);
        laserSR.color = hiddenLaser;
        laserBox2D.enabled = false;
    }
}

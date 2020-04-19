using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : ShooterBase
{
    public SpriteRenderer laserSR;
    public BoxCollider2D laserBox2D;
    public float shootDelay = 0.6f;
    float laserDuration = 0.1f;

    float hiddenLaser = 0f;
    float visibleLaser = 1f;
    float warningLaser = 0.3f;
    Carrier target;

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
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float rads = Mathf.Acos(direction.x);
        if (direction.y < 0f) rads = Mathf.PI - rads;
        rads += Mathf.PI / 2;
        laserSR.transform.rotation = Quaternion.Euler(0f,0f,180f * (rads / Mathf.PI));
        SetAlpha(warningLaser);
        StartCoroutine(DelayedShot());
    }

    IEnumerator DelayedShot()
    {
        yield return new WaitForSeconds(shootDelay);
        SetAlpha(visibleLaser);
        laserBox2D.enabled = true;
        StartCoroutine(DelayedHideLaser());
    }

    IEnumerator DelayedHideLaser()
    {
        yield return new WaitForSeconds(laserDuration);
        SetAlpha(hiddenLaser);
        laserBox2D.enabled = false;
    }

    void SetAlpha(float alpha)
    {        
        laserSR.color = new Color(laserSR.color.r,laserSR.color.g,laserSR.color.b,alpha);
    }
}

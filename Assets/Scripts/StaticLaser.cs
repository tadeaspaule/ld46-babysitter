using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLaser : ShooterBase
{
    public SpriteRenderer laserSR;
    public BoxCollider2D laserBox2D;
    public float shootDelay = 0.6f;
    float laserDuration = 0.1f;

    float hiddenLaser = 0f;
    float visibleLaser = 1f;
    float warningLaser = 0.3f;

    protected new void CallWhenFrozen()
    {
        Debug.Log("LASR");
        StopAllCoroutines();
    }
    
    
    public override void Shoot()
    {
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

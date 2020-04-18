using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShooterBase : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public GameObject bulletPrefab;
    public float timer = 0f;
    public float shootFrequency = 1f;
    public float shootSpeed = 6f;
    bool frozen = false;

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
    }

    protected void BaseUpdate()
    {
        if (frozen) return;
        timer += Time.deltaTime;
        if (timer > shootFrequency) {
            timer = 0f;
            Shoot();
        }
    }

    public abstract void Shoot();

    public void ToggleFreeze(bool value)
    {
        frozen = value;
    }
}

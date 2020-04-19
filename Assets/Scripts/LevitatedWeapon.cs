using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitatedWeapon : MonoBehaviour
{
    float degs = 0f;
    float rotateSpeed = 15f;

    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        degs += (Time.deltaTime * rotateSpeed) + (Mathf.Min(rb.velocity.magnitude,15f) / 15f) * 3f;
        transform.rotation = Quaternion.Euler(0f,0f,degs);
    }
}

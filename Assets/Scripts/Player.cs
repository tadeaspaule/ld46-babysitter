using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D rb;
    float moveSpeed = 7f;
    bool freezeMovement = false;

    // Update is called once per frame
    void Update()
    {
        if (freezeMovement) return;
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.A)) {
            movement += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D)) {
            movement += Vector3.right;
        }
        rb.velocity = movement * moveSpeed;
    }

    public void ToggleFreezeMovement(bool value)
    {
        freezeMovement = value;
    }
}

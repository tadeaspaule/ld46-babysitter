using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    float moveSpeed = 7f;
    bool freezeMovement = false;

    float alpha = 0.7f;
    float alphaRadians = 0f;
    float alphaSpeed = 1f;
    float alphaImpact = 0.2f;

    float levitateSpeed = 15f;
    public Rigidbody2D levitatedObject;

    // Update is called once per frame
    void Update()
    {
        // alpha change
        alphaRadians += Time.deltaTime * alphaSpeed;
        if (alphaRadians > Mathf.PI * 2) alphaRadians -= Mathf.PI * 2;        
        spriteRenderer.color = new Color(1f,1f,1f,alpha + Mathf.Sin(alphaRadians)*alphaImpact);

        if (freezeMovement) return;
        // movement
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.A)) {
            movement += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D)) {
            movement += Vector3.right;
        }
        rb.velocity = movement * moveSpeed;

        // rotation
        if (movement.x > 0f) {
            transform.rotation = Quaternion.Euler(0f,0f,0f);
        }
        else if (movement.x < 0f) {
            transform.rotation = Quaternion.Euler(0f,180f,0f);
        }

        // levitating a weapon
        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - levitatedObject.transform.position);
        direction = new Vector3(direction.x,direction.y,0f);
        if (direction.magnitude > 1f) direction = direction.normalized;
        levitatedObject.velocity = direction * levitateSpeed;

    }

    public void ToggleFreezeMovement(bool value)
    {
        freezeMovement = value;
        levitatedObject.velocity = Vector2.zero;
    }

    public void Reset()
    {
        levitatedObject.velocity = Vector2.zero;
        levitatedObject.transform.position = new Vector3(-5.5f,-3f,0f);
    }
}

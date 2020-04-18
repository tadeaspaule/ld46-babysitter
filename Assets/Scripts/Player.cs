using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D rb;
    float moveSpeed = 7f;
    bool freezeMovement = false;

    public GameObject bulletPrefab;
    Vector3 shotOffset = new Vector3(0f,1f,0f);
    float shootSpeed = 15f;
    float shotTimer = 0f;
    float shotDelay = 0.2f;

    float floatRadians = 0f;
    float floatSpeed = 2f;
    float floatHeight = 0.08f;
    float floatBaseY = 0.2f;

    // Update is called once per frame
    void Update()
    {
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

        // floating
        floatRadians += Time.deltaTime * floatSpeed;
        if (floatRadians > Mathf.PI * 2) floatRadians -= Mathf.PI * 2;
        transform.localPosition = new Vector3(transform.localPosition.x,floatBaseY + Mathf.Sin(floatRadians)*floatHeight,transform.localPosition.z);


        // shooting
        shotTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && shotTimer > shotDelay) {
            shotTimer = 0f;
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - (transform.position + shotOffset));
            direction = new Vector3(direction.x,direction.y,0f);
            direction = direction.normalized;
            Debug.Log(direction.normalized);
            GameObject bullet = Instantiate(bulletPrefab,Vector3.zero,Quaternion.identity,FindObjectOfType<BulletHolder>().transform);
            bullet.transform.position = transform.position + shotOffset;
            bullet.GetComponent<PlayerBullet>().SetVelocity(direction * shootSpeed);
        }
    }

    public void ToggleFreezeMovement(bool value)
    {
        freezeMovement = value;
    }
}

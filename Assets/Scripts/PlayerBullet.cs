using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    Vector2 startingVelocity;
    public Rigidbody2D rb;
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("wallground")) {
            DestroyBullet();
        }
        else if (other.gameObject.tag.Equals("bullet")) {
            // TODO maybe some cool effect
            other.gameObject.GetComponent<Bullet>().DestroyBullet();
            rb.velocity = startingVelocity;
        }
        // else if (other.gameObject.name.Equals("carrier")) {
        //     // Game over
        //     FindObjectOfType<GameManager>().GameOver();
        //     // DestroyBullet();
        // }
    }

    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    public void SetVelocity(Vector2 velocity)
    {
        startingVelocity = velocity;
        rb.velocity = startingVelocity;
    }
}

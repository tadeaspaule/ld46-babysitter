using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("wallground")) {
            DestroyBullet();
        }
        // else if (other.gameObject.name.Equals("player")) {
        //     // TODO maybe some cool effect
        //     DestroyBullet();
        // }
        else if (other.gameObject.name.Equals("carrier")) {
            // Game over
            FindObjectOfType<GameManager>().GameOver();
            // DestroyBullet();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("playerbullet")) {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}

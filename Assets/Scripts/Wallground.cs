using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallground : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.gameObject.tag.Equals("playerbullet")) {
        //     other.gameObject.GetComponent<PlayerBullet>().DestroyBullet();
        // }
    }
}

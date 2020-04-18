using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHolder : MonoBehaviour
{
    public void FreezeAll()
    {
        foreach (Transform bullet in transform) {
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void DestroyAll()
    {
        foreach (Transform bullet in transform) {
            Destroy(bullet.gameObject);
        }        
    }
}

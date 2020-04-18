using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterHolder : MonoBehaviour
{
    public void FreezeAll()
    {
        foreach (Transform shooter in transform) {
            shooter.GetComponent<Shooter>().ToggleFreeze(true);
        }
    }

    public void DestroyAll()
    {
        foreach (Transform shooter in transform) {
            Destroy(shooter.gameObject);
        }        
    }
}

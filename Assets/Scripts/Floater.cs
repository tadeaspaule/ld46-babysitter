using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    float floatRadians = 0f;
    public float floatSpeed = 2f;
    public float floatHeight = 0.08f;
    float baseFloatY;

    void Start()
    {
        baseFloatY = transform.localPosition.y;
    }
    
    // Update is called once per frame
    void Update()
    {
        floatRadians += Time.deltaTime * floatSpeed;
        if (floatRadians > Mathf.PI * 2) floatRadians -= Mathf.PI * 2;
        transform.localPosition = new Vector3(transform.localPosition.x,baseFloatY + Mathf.Sin(floatRadians)*floatHeight,transform.localPosition.z);
    }
}

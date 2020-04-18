using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float leftBoundary;
    public float speed = 5f;
    float rightGap;
    
    // Start is called before the first frame update
    void Start()
    {
        rightGap = Mathf.Abs(
            transform.GetChild(transform.childCount-1).position.x
            -
            transform.GetChild(transform.childCount-2).position.x
        );
        Debug.Log(rightGap);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform) {
            child.position += Vector3.left * Time.deltaTime * speed;
            if (child.position.x < leftBoundary) {
                child.position = new Vector3(GetRightmostChildX()+rightGap,child.position.y,0f);
            }
        }
    }

    float GetRightmostChildX()
    {
        float x = transform.GetChild(transform.childCount-1).position.x;
        foreach (Transform child in transform) {
            if (child.position.x > x) x = child.position.x;
        }
        return x;
    }
}

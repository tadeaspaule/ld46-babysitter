using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    public GameManager gameManager;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public float timer = 0f;
    float animAtTime = 3f;
    bool animating = false;
    float animDelay = 0.1f;
    int index = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (animating && timer > animDelay) {
            timer = 0f;
            index++;
            if (index >= sprites.Length) {
                // end of animation
                animating = false;
                spriteRenderer.sprite = sprites[0];
                index = 0;
                animAtTime = 3f + Random.Range(0f,4f);
            }
            else {
                // change frame of animation
                spriteRenderer.sprite = sprites[index];
            }
        }
        else if (!animating && timer > animAtTime) {
            // starting animation
            animating = true;
            timer = 0f;
            index = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("stairs-up")) {
            gameManager.EnterLevel();
        }
        else if (other.gameObject.tag.Equals("bullet")) {
            gameManager.GameOver();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("bullet")) {
            gameManager.GameOver();
        }
    }
}

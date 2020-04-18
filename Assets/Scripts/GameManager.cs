using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoxCollider2D stairsUp;
    public Player player;
    public Animation levelTransitionAnim;
    public AnimationClip levelTransitionClip;

    int level = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterLevel()
    {
        level++;
        // stairsUp.enabled = false;
        levelTransitionAnim.Play("levelTransition");
        player.ToggleFreezeMovement(true);
        StartCoroutine(SetupLevel());
        StartCoroutine(AfterLevelTransition());
    }

    IEnumerator SetupLevel()
    {
        yield return new WaitForSeconds(levelTransitionClip.averageDuration / 2);
        player.transform.position = new Vector3(-6.5f,-5f,0f);
        // reset enemies
    }

    IEnumerator AfterLevelTransition()
    {
        yield return new WaitForSeconds(levelTransitionClip.averageDuration);
        player.ToggleFreezeMovement(false);
        // activate enemies
    }
}

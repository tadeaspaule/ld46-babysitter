using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoxCollider2D stairsUp;
    public Player player;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Level stuff

    public Animation levelTransitionAnim;
    public AnimationClip levelTransitionClip;
    int level = 0;

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

    #endregion

    #region Enemy stuff
    
    public ShooterHolder shooterHolder;
    public BulletHolder bulletHolder;
    
    

    #endregion

    #region Game stuff

    public Animation canvasAnim;
    public GameObject gameOverPanel;
    
    public void TryAgain()
    {
        gameOverPanel.SetActive(false);
        StartGame();
    }
    
    // called at the very start, or when clicking "try again"
    public void StartGame()
    {
        shooterHolder.DestroyAll();
        bulletHolder.DestroyAll();
    }

    public void GameOver()
    {
        Debug.Log("over");
        shooterHolder.FreezeAll();
        bulletHolder.FreezeAll();
        canvasAnim.Play("gameOver");

    }

    #endregion

}

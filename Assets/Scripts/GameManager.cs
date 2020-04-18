using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoxCollider2D stairsUp;
    public Player player;
    public Carrier carrier;

    
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
        stairsUp.enabled = false;
        levelTransitionAnim.Play("levelTransition");
        player.ToggleFreezeMovement(true);
        CompleteLevelSetup(levelTransitionClip.averageDuration);
    }

    void CompleteLevelSetup(float delay)
    {
        StartCoroutine(SetupLevel(delay / 2));
        StartCoroutine(AfterLevelTransition(delay));
    }

    IEnumerator SetupLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        carrier.transform.position = new Vector3(-5f,-5f,0f);
        player.Reset();
        // reset enemies
        // shooterHolder.DestroyAll(); TODO uncomment
    }

    IEnumerator AfterLevelTransition(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.ToggleFreezeMovement(false);
        shooterHolder.ToggleFreezeAll(false);
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
        // shooterHolder.DestroyAll();
        bulletHolder.DestroyAll();
        level = 0;
        CompleteLevelSetup(0f);
    }

    public void GameOver()
    {
        Debug.Log("over");
        shooterHolder.ToggleFreezeAll(true);
        bulletHolder.FreezeAll();
        canvasAnim.Play("gameOver");

    }

    #endregion

}

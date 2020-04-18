using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoxCollider2D stairsUp;
    public Player player;
    public Carrier carrier;

    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (playingLevel) levelTimer += Time.deltaTime;
        if (playingLevel && levelTimer > levelTimerMax) {
            playingLevel = false;
            OpensStairsUp();
        }
    }

    #region Level stuff

    public Animation levelTransitionAnim;
    public AnimationClip levelTransitionClip;
    public Animation openDoorsAnim;
    int level = 0;
    float levelTimer = 0f;
    float levelTimerMax = 5f;
    bool playingLevel = true;

    public GameObject[] levels;

    void OpensStairsUp()
    {
        Debug.Log("stairs open");
        stairsUp.enabled = true;
        openDoorsAnim.Play("openDoors");
    }

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
        openDoorsAnim.Play("resetDoors");
        // reset enemies
        shooterHolder.DestroyAll();
        bulletHolder.DestroyAll();
        if (level >= levels.Length) {
            // on final floor with daddy, just end of game stuff
            stairsUp.gameObject.SetActive(false);
        }
        else {
            GameObject currentLevel = Instantiate(levels[level],Vector3.zero,Quaternion.identity);
            shooterHolder = currentLevel.GetComponent<ShooterHolder>();
            shooterHolder.ToggleFreezeAll(true);
        }
    }

    IEnumerator AfterLevelTransition(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.ToggleFreezeMovement(false);
        shooterHolder.ToggleFreezeAll(false);
        playingLevel = true;
        levelTimer = 0f;
        // activate enemies
        shooterHolder.ToggleFreezeAll(false);
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

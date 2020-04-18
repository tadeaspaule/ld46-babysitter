using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public Animation stairsDownAnim;
    int level = 0;
    float levelTimer = 0f;
    float levelTimerMax = 5f;
    bool playingLevel;

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
        DelayedCompleteLevelSetup(levelTransitionClip.averageDuration);
    }

    void DelayedCompleteLevelSetup(float delay)
    {
        StartCoroutine(DelayedSetupLevel(delay / 2));
        StartCoroutine(DelayedAfterLevelTransition(delay));
    }

    void CompleteLevelSetup()
    {
        SetupLevel();
        AfterLevelTransition();
    }

    void SetupLevel()
    {
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
        // reset UI
        stairsDownAnim.Play("openStairsDown");
    }

    IEnumerator DelayedSetupLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetupLevel();
    }

    void AfterLevelTransition()
    {
        player.ToggleFreezeMovement(false);
        shooterHolder.ToggleFreezeAll(false);
        playingLevel = true;
        levelTimer = 0f;
        stairsDownAnim.Play("closeStairsDown");
        // activate enemies
        shooterHolder.ToggleFreezeAll(false);

        if (level == 0) {
            // intro sequence
            ShowLevelIntroFirst(new string[]{
                "Of course. Dad left the security system on again...",
                "Good thing I have LEVITATRON 9000"
            });
        }
        else if (level == 3) {
            // first dynamic shooter
            ShowLevelIntroFirst(new string[]{
                "That thing can track my movement"
            });
        }
        else if (level == 6) {
            // first laser shooter
            ShowLevelIntroFirst(new string[]{
                "Damn, I'll have to watch out for that laser..."
            });
        }
    }

    void ShowLevelIntroFirst(string[] textQueue)
    {
        string[] portraitQueue = new string[textQueue.Length];
        for (int i = 0; i < textQueue.Length; i++) portraitQueue[i] = "player";
        ShowLevelIntroFirst(textQueue,portraitQueue);
    }

    void ShowLevelIntroFirst(string[] textQueue, string[] portraitQueue)
    {
        playingLevel = false;
        textPanel.SetTextQueue(textQueue,portraitQueue);
        player.ToggleFreezeMovement(true);
        shooterHolder.ToggleFreezeAll(true);
    }

    IEnumerator DelayedAfterLevelTransition(float delay)
    {
        yield return new WaitForSeconds(delay);
        AfterLevelTransition();
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
        // StartGame();
        level--;
        EnterLevel();
    }
    
    // called at the very start, or when clicking "try again"
    public void StartGame()
    {
        shooterHolder.DestroyAll();
        bulletHolder.DestroyAll();
        level = 5;
        CompleteLevelSetup();
    }

    public void GameOver()
    {
        Debug.Log("over");
        shooterHolder.ToggleFreezeAll(true);
        bulletHolder.FreezeAll();
        canvasAnim.Play("gameOver");

    }

    #endregion

    #region Text stuff

    public TextPanel textPanel;

    public void TextQueueEnded()
    {
        player.ToggleFreezeMovement(false);
        shooterHolder.ToggleFreezeAll(false);
        playingLevel = true;
    }
    #endregion

}

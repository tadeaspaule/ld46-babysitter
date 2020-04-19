using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if (duringLevelTransition) {
            partyHolder.position += partyMoveVector * Time.deltaTime;
            if (partyHolder.position.x > 12f) {
                // they are up the stairs, end animation
                levelTransitionAnim.Play("levelTransition");
                DelayedCompleteLevelSetup(levelTransitionClip.averageDuration);
                StartCoroutine(DelayedDisableLevelTransitionOuter());
                duringLevelTransition = false;
            } 
            return;
        }
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
    public GameObject dad;
    int level = 0;
    float levelTimer = 0f;
    float levelTimerMax = 5f;
    bool playingLevel;

    public GameObject levelTransitionOuter;
    public Transform partyHolder;
    Vector3 partyMoveVector = new Vector3(8f,6f,0f);
    bool duringLevelTransition = false;

    

    public GameObject[] levels;

    void OpensStairsUp()
    {
        Debug.Log("stairs open");
        stairsUp.enabled = true;
        openDoorsAnim.Play("openDoors");
    }

    public void EnterLevel()
    {
        bulletHolder.FreezeAll();
        shooterHolder.ToggleFreezeAll(true);
        level++;
        stairsUp.enabled = false;
        levelTransitionAnim.Play("levelTransition");
        player.ToggleFreezeMovement(true);
        StartCoroutine(DelayedEnableLevelTransitionOuter());
        // DelayedCompleteLevelSetup(levelTransitionClip.averageDuration);
    }

    IEnumerator DelayedEnableLevelTransitionOuter()
    {
        yield return new WaitForSeconds(levelTransitionClip.averageDuration / 2);
        levelTransitionOuter.SetActive(true);
        partyHolder.position = new Vector3(-12f,-9.65f,0f);
        duringLevelTransition = true;
    }

    IEnumerator DelayedDisableLevelTransitionOuter()
    {
        yield return new WaitForSeconds(levelTransitionClip.averageDuration / 2);
        partyHolder.position = new Vector3(-12f,-9.65f,0f);
        levelTransitionOuter.SetActive(false);
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
        // reset enemies
        shooterHolder.DestroyAll();
        bulletHolder.DestroyAll();
        carrier.transform.position = new Vector3(-5f,-5f,0f);
        player.Reset();
        openDoorsAnim.Play("resetDoors");
        if (level >= levels.Length) {
            // on final floor with daddy, just end of game stuff
            stairsUp.gameObject.SetActive(false);
            dad.SetActive(true);
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
                "Typical dad, putting up lasers. Of course, I can just wait for the heat warning",
                "D... Da? Dada? ... Dada!",
                "Yes, yes, we're going to Dada."
            }, new string[]{"player","baby","player"});
        }
        else if (level == levels.Length) {
            // end of game
            ShowLevelIntroFirst(new string[]{
                "dad zone line 1",
                "D... Da? Dada? ... Dada!",
                "yesyes"
            }, new string[]{"player","baby","player"});

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
        // level--;
        // EnterLevel();
        CompleteLevelSetup();
    }
    
    // called at the very start, or when clicking "try again"
    public void StartGame()
    {
        shooterHolder.DestroyAll();
        bulletHolder.DestroyAll();
        level = 0;
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
        if (level < levels.Length) {
            player.ToggleFreezeMovement(false);
            shooterHolder.ToggleFreezeAll(false);
            playingLevel = true;
        }
        else {
            // last text in the game ended
            canvasAnim.Play("showCredits");
        }
    }

    #endregion

    public void ClickedMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ClickedQuit()
    {
        Application.Quit();
    }

}

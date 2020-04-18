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
    float textDelay = 5f;
    
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
        CompleteLevelSetup();
        ShowText("Damn! This is a test text and it shows!");
        player.ToggleFreezeMovement(true);
        shooterHolder.ToggleFreezeAll(true);
        Debug.Log("1");
        StartCoroutine(DelayedUnfreezeAll());
    }

    IEnumerator DelayedUnfreezeAll()
    {
        yield return new WaitForSeconds(textDelay);
        player.ToggleFreezeMovement(false);
        shooterHolder.ToggleFreezeAll(false);
        Debug.Log("2");
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

    public TextMeshProUGUI popupText;
    public Animation textPopupAnim;

    public void ShowText(string text)
    {
        popupText.text = text;
        textPopupAnim.Play("showTextPanel");
        StartCoroutine(DelayedHideTextPanel());
    }

    IEnumerator DelayedHideTextPanel()
    {
        yield return new WaitForSeconds(textDelay);
        textPopupAnim.Play("hideTextPanel");
    }

    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Player player;
    public Carrier carrier;
    public Animation canvasAnim;
    public AnimationClip hideMenuClip;
    public Animation playerAnim;
    public Animation officeAnim;
    public AnimationClip showOfficeClip;
    public Animation officeDoorAnim;

    void Start()
    {
        player.ToggleFreezeMovement(true);
    }

    #region Intro sequence
    
    public void ClickedPlay()
    {
        canvasAnim.Play("hideMenu");
        StartCoroutine(DelayedPlayIntroSequence());
    }

    IEnumerator DelayedPlayIntroSequence()
    {
        yield return new WaitForSeconds(hideMenuClip.averageDuration);
        playerAnim.Play("introSequence");
        // ShowOfice(); only for debugging, will have some exposition first in main
    }

    void ShowOfice()
    {
        officeAnim.Play("showOffice");
        StartCoroutine(DelayedOpenOfficeDoor());
    }

    IEnumerator DelayedOpenOfficeDoor()
    {
        yield return new WaitForSeconds(showOfficeClip.averageDuration);
        officeDoorAnim.Play("openDoors");
        carrier.GetComponent<BoxCollider2D>().enabled = true;
        carrier.GetComponent<Rigidbody2D>().simulated = true;
        player.ToggleFreezeMovement(false);
    }

    public void EnteredOffice()
    {
        SceneManager.LoadScene("GameScene");
    }

    #endregion

    public void ClickedSoundsToggle()
    {

    }

    public void ClickedQuit()
    {
        Application.Quit();
    }
}

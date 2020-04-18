using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        player.ToggleFreezeMovement(true);
    }

    #region Intro sequence
    
    public Player player;
    public Carrier carrier;
    public Animation menuBackgroundAnim;
    public Animation canvasAnim;
    public AnimationClip hideMenuClip;
    public Animation playerAnim;
    public AnimationClip playerClip;
    public Animation officeAnim;
    public AnimationClip showOfficeClip;
    public Animation officeDoorAnim;
    public TextPanel textPanel;

    public ParallaxLayer[] parallaxLayers;
    
    public void ClickedPlay()
    {
        canvasAnim.Play("hideMenu");
        menuBackgroundAnim.Play("fadeMenuBackground");
        StartCoroutine(DelayedPlayIntroSequence());
    }

    IEnumerator DelayedPlayIntroSequence()
    {
        yield return new WaitForSeconds(hideMenuClip.averageDuration);
        playerAnim.Play("introSequence");
        StartCoroutine(DelayedStartIntroText());
        // ShowOfice(); only for debugging, will have some exposition first in main
    }

    IEnumerator DelayedStartIntroText()
    {
        yield return new WaitForSecondsRealtime(playerClip.averageDuration);
        textPanel.SetTextQueue(new string[]{"this is the first long text","this is the second and it's different look d look"});
    }

    public void TextQueueEnded()
    {
        ShowOfice();
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
        foreach (ParallaxLayer layer in parallaxLayers) {
            layer.enabled = false;
        }
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

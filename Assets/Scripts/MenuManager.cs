using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    #region Intro sequence
    
    public Animation canvasAnim;
    public AnimationClip hideMenuClip;
    public TextPanel textPanel;
    public Animation textPanelAnim;
    public AnimationClip hideTextClip;

    public ParallaxLayer[] parallaxLayers;
    
    public void ClickedPlay()
    {
        canvasAnim.Play("hideMenu");
        StartCoroutine(DelayedPlayIntroSequence());
    }

    IEnumerator DelayedPlayIntroSequence()
    {
        yield return new WaitForSeconds(hideMenuClip.averageDuration);
        StartCoroutine(DelayedStartIntroText());
    }

    IEnumerator DelayedStartIntroText()
    {
        yield return new WaitForSecondsRealtime(1f);
        textPanel.SetTextQueue(new string[]{
            "this is the first long text",
            "this is the second and it's different look d look"
        });
    }

    public void TextQueueEnded()
    {
        textPanelAnim.Play("hideTextPanel");
        StartCoroutine(DelayedSwitchScene());
    }

    IEnumerator DelayedSwitchScene()
    {
        yield return new WaitForSeconds(hideTextClip.averageDuration + 0.1f);
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

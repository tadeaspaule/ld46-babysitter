using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPanel : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Animation panelAnim;

    string[] textQueue;
    int index = 0;
    int currentTextIndex;
    float charDelay = 0.1f;
    
    bool writingText;

    public GameManager gameManager;
    public MenuManager menuManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) Skip();
    }

    void WriteCurrentText()
    {
        currentTextIndex = 0;
        text.text = "";
        writingText = true;
        StartCoroutine(WriteTextChar());
    }

    IEnumerator WriteTextChar()
    {
        yield return new WaitForSeconds(charDelay);
        text.text =textQueue[index].Substring(0,currentTextIndex);
        currentTextIndex++;
        if (currentTextIndex <= textQueue[index].Length) {
            StartCoroutine(WriteTextChar());
        }
        else {
            writingText = false;
        }
    }

    public void SetTextQueue(string[] queue)
    {
        panelAnim.Play("showTextPanel");
        textQueue = queue;
        index = 0;
        WriteCurrentText();
    }

    public void Skip()
    {
        if (textQueue == null || textQueue.Length == 0) return;
        if (writingText) {
            // skip the writing animation and show full text
            text.text = textQueue[index];
            writingText = false;
            StopAllCoroutines();
        }
        else {
            // skip to next text, or hide panel
            index++;
            if (index < textQueue.Length) {
                WriteCurrentText();
            }
            else {
                panelAnim.Play("hideTextPanel");
                textQueue = null;
                NotifyOfEndOfQueue();
            }
        }        
    }

    void NotifyOfEndOfQueue()
    {
        if (menuManager != null) menuManager.TextQueueEnded();
        if (gameManager != null) gameManager.TextQueueEnded();
    }
}

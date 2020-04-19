using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextPanel : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Animation panelAnim;
    public Image image;

    string[] textQueue;
    Sprite[] portraitQueue;
    int index = 0;
    int currentTextIndex;
    float charDelay = 0.03f;
    
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
        image.sprite = portraitQueue[index];
        text.text = "";
        writingText = true;
        StartCoroutine(WriteTextChar());
    }

    IEnumerator WriteTextChar()
    {
        yield return new WaitForSeconds(charDelay);
        if (index < textQueue.Length) {
            text.text = textQueue[index].Substring(0,currentTextIndex);
            currentTextIndex++;
            if (currentTextIndex <= textQueue[index].Length) {
                StartCoroutine(WriteTextChar());
            }
            else {
                writingText = false;
            }
        }
        else writingText = false;        
    }

    public void SetTextQueue(string[] queue, string[] portraits)
    {
        panelAnim.Play("showTextPanel");
        textQueue = queue;
        portraitQueue = new Sprite[portraits.Length];
        for (int i = 0; i < portraits.Length; i++) portraitQueue[i] = GetPortrait(portraits[i]);
        index = 0;
        WriteCurrentText();
    }

    public void SetTextQueue(string[] queue)
    {
        string[] portraits = new string[queue.Length];
        for (int i = 0; i < queue.Length; i++) portraits[i] = "player";
        SetTextQueue(queue,portraits);
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

    [System.Serializable]
    public class NamePortraitPair
    {
        public string name;
        public Sprite portrait;
    }
    public NamePortraitPair[] portraits;

    Sprite GetPortrait(string name)
    {
        foreach (NamePortraitPair pair in portraits) {
            if (pair.name.Equals(name)) return pair.portrait;
        }
        return null;
    }

}

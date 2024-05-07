using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Assistant : MonoBehaviour {

    private TMP_Text messageText;
    [SerializeField] private GameObject GameObjectText;
    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource talkingAudioSource;
    private int i = 0, j=0;
    private int l=1;
    [SerializeField] string[] messageArraySF;
    [SerializeField] string[] messageArrayLvlUp;
    private bool called = false;
    [SerializeField] private GameObject ttp;
    private ReputationSystem reputationSystem;
    public void SetLevelSystem(ReputationSystem reputationSystem)
    {
        this.reputationSystem = reputationSystem;
    }
    private void Start() {
        messageText=GameObjectText.GetComponent<TMP_Text>();
        talkingAudioSource = transform.Find("talkingSound").GetComponent<AudioSource>();
        StopTalkingSound();
        NextText();
    }
    public void NextText()
    {
        if(reputationSystem.GetLevelNumber()>=1)
        {
            called = true;
            l = messageArrayLvlUp.Length;
            if (j < l)
            {
                ttp.SetActive(false);
                transform.Find("Button").gameObject.SetActive(true);
                if (textWriterSingle != null && textWriterSingle.IsActive())
                {
                    // Currently active TextWriter
                    textWriterSingle.WriteAllAndDestroy();
                }
                else
                {
                    // Rimuovi il testo attuale
                    messageText.text = "";
                    string message = messageArrayLvlUp[j];

                    StartTalkingSound();
                    textWriterSingle = TextWriter.AddWriter_Static(messageText, message, .02f, true, true, StopTalkingSound);
                    j++;
                }
            }
            else
            {
                transform.Find("Button").gameObject.SetActive(false);
            }
        }
        else
        {
            l = messageArraySF.Length;

            if (i < l)
            {
                if (textWriterSingle != null && textWriterSingle.IsActive())
                {
                    // Currently active TextWriter
                    textWriterSingle.WriteAllAndDestroy();
                }
                else
                {
                    // Rimuovi il testo attuale
                    messageText.text = "";
                    string message = messageArraySF[i];

                    StartTalkingSound();
                    textWriterSingle = TextWriter.AddWriter_Static(messageText, message, .02f, true, true, StopTalkingSound);
                    i++;
                }
            }
            else
            {
                ttp.SetActive(true);
                transform.Find("Button").gameObject.SetActive(false);

            }
        }
    }
    private void StartTalkingSound() {
        talkingAudioSource.Play();
    }

    private void StopTalkingSound() {
        talkingAudioSource.Stop();
    }

    /*private void Start() {
        //TextWriter.AddWriter_Static(messageText, "This is the assistant speaking, hello and goodbye, see you next time!", .1f, true);
    }*/
    private void Update()
    {
        if (reputationSystem.GetLevelNumber() >= 1 && called==false)
        {
            NextText();
        }
    }
}

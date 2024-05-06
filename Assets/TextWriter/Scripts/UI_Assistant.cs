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
    private int i = 0;
    private int l=1;
    [SerializeField] private GameObject ttp;
    private void Awake() {
        messageText=GameObjectText.GetComponent<TMP_Text>();
        talkingAudioSource = transform.Find("talkingSound").GetComponent<AudioSource>();
        StopTalkingSound();
        NextText();
    }
    public void NextText()
    {
        if (i < l) { 
            if (textWriterSingle != null && textWriterSingle.IsActive())
            {
                // Currently active TextWriter
                textWriterSingle.WriteAllAndDestroy();
            }
            else
            {
                // Rimuovi il testo attuale
                messageText.text = "";

                string[] messageArray = new string[] {
                "Yo <color=#FFFF00>Shady</color>! <color=#FFFF00>Club Doggo</color> just claimed another <color=#FFFF00>Business</color>, meowmba.\r\nTonight, we'll teach them a lesson",
                "But first, we have to make a name for ourselves. We gotta raise our <color=#FFFF00>Reputation</color>, so that everyone in this town will acknowledge <color=#FFFF00>Meow-Tang Clan</color>!",
                "Fine",
            };

                string message = messageArray[i];
                l = messageArray.Length;
                StartTalkingSound();
                textWriterSingle = TextWriter.AddWriter_Static(messageText, message, .02f, true, true, StopTalkingSound);
                i++;
            }
        }
        else
        {
            ttp.SetActive(true);
        }
        
    }
    private void StartTalkingSound() {
        talkingAudioSource.Play();
    }

    private void StopTalkingSound() {
        talkingAudioSource.Stop();
    }

    private void Start() {
        //TextWriter.AddWriter_Static(messageText, "This is the assistant speaking, hello and goodbye, see you next time!", .1f, true);
    }

}

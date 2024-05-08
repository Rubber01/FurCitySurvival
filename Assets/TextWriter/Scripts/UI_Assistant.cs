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
    [SerializeField] private int i = 0, j=0, l=1;
    [SerializeField] string[] messageArraySF;
    [SerializeField] string[] messageArrayLvlUp; 
    [SerializeField] private int repCost;

    private bool called = false;
    [SerializeField] private GameObject ttp;
    private ReputationSystem reputationSystem;
    private Animator animator;
    public void SetLevelSystem(ReputationSystem reputationSystem)
    {
        this.reputationSystem = reputationSystem;
    }
    private void Start() {
        animator = transform.GetComponent<Animator>();
        messageText=GameObjectText.GetComponent<TMP_Text>();
        talkingAudioSource = transform.Find("talkingSound").GetComponent<AudioSource>();
        StopTalkingSound();
        transform.Find("Button").gameObject.SetActive(false);
        Debug.Log("If repLevel " +reputationSystem.GetLevelNumber() + "==" + (repCost- 1));
        if (reputationSystem.GetLevelNumber() == (repCost - 1))
        {
            NextText();
        }
    }
    public void NextText()
    {
        if(reputationSystem.GetLevelNumber()>= repCost)
        {
            called = true;
            l = messageArrayLvlUp.Length;
            if (j < l)
            {
                animator.Play("TutorialText");
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
                animator.Play("Close");
                transform.Find("Button").gameObject.SetActive(false);
                Destroy(1.2f);
            }
        }
        else
        {
            l = messageArraySF.Length;
            Debug.Log("Dentro else");
            if (i < l)
            {
                Debug.Log("Dentro i<l");

                animator.Play("TutorialText");
                Debug.Log("Dentro dopo Play animation");

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
                    string message = messageArraySF[i];

                    StartTalkingSound();
                    textWriterSingle = TextWriter.AddWriter_Static(messageText, message, .02f, true, true, StopTalkingSound);
                    i++;
                }
            }
            else
            {
                animator.Play("Close");
                ttp.SetActive(true);
                transform.Find("Button").gameObject.SetActive(false);

            }
        }
    }
    private void StartTalkingSound() {
        talkingAudioSource.Play();
    }
    IEnumerator Destroy(float t)
    {
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);
    }
    private void StopTalkingSound() {
        talkingAudioSource.Stop();
    }

    /*private void Start() {
        //TextWriter.AddWriter_Static(messageText, "This is the assistant speaking, hello and goodbye, see you next time!", .1f, true);
    }*/
    private void Update()
    {
        if (reputationSystem.GetLevelNumber() >= repCost && called==false)
        {
            NextText();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private TMP_Text text;
    [SerializeField] private int time = 5; 
    [SerializeField] bool disable=false;

    private void Awake()
    {
        text = transform.Find("Respawning").GetComponent<TMP_Text>();
        DefaultText();
    }
    public void Restarting()
    {
        StartCoroutine(ChangingText());
    }
    IEnumerator ChangingText()
    {
        for (int i = time; i > 0; i--)
        {
            text.text = "Respawning in " + i + "...";
            yield return new WaitForSeconds(1f);
        }
        DefaultText();
    }
    public void DefaultText()
    {
        text.text = "";

    }
    public int GetTime()
    {
        return time;
    }
}

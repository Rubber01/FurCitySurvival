using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    //public static PlayerManager instance;

    public static int credits;
    public static int influencePoints;
    public static int henchmenSlots = 3;
    public static int currentHench = 0;
    public static int metalScrapNumber;
    public static int metalNumber;
    public static int plasticWasteNumber;
    public static int plasticNumber;
    public static int chipNumber;

    [SerializeField] private TextMeshProUGUI creditsText;
    //[SerializeField] private TextMeshProUGUI influencePointText;
    [SerializeField] private TextMeshProUGUI henchmenSlotsText;
    //[SerializeField] private TextMeshProUGUI metalScrapText;
    //[SerializeField] private TextMeshProUGUI metalText;
    //[SerializeField] private TextMeshProUGUI plasticWasteText;
    //[SerializeField] private TextMeshProUGUI plasticText;
    //[SerializeField] private TextMeshProUGUI chipText;

    //public static PlayerManager Instance { get { return instance; } }

    private void Awake()
    {
        //henchmenSlotsText = transform.Find("GangNumber").GetComponent<TextMeshProUGUI>();
        //creditsText = transform.Find("CreditText").GetComponent<TextMeshProUGUI>();

        Application.targetFrameRate = 60;
    }

    

    private void Update()
    {
        creditsText.text = "x: " + credits.ToString();
        //metalScrapText.text = "MS:" + metalScrapNumber.ToString();
        //metalText.text = "M:" + metalNumber.ToString();
        //plasticWasteText.text = "PW:" + plasticWasteNumber.ToString();
        //plasticText.text = "P:" + plasticNumber.ToString();
        //chipText.text = "Ch:" + chipNumber.ToString();
        //influencePointText.text = "Inf:" + influencePoints.ToString();
        henchmenSlotsText.text = currentHench + "/" + henchmenSlots.ToString();
    }
}

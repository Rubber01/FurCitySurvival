using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public static int credits;
    public static int influencePoints;
    public static int metalScrapNumber;
    public static int metalNumber;
    public static int plasticWasteNumber;
    public static int plasticNumber;
    public static int chipNumber;

    [SerializeField] private TextMeshProUGUI creditsText;
    [SerializeField] private TextMeshProUGUI influencePointText;
    [SerializeField] private TextMeshProUGUI metalScrapText;
    [SerializeField] private TextMeshProUGUI metalText;
    [SerializeField] private TextMeshProUGUI plasticWasteText;
    [SerializeField] private TextMeshProUGUI plasticText;
    [SerializeField] private TextMeshProUGUI chipText;

    public static PlayerManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        creditsText.text = "Cr:" + credits.ToString();
        metalScrapText.text = "MS:" + metalScrapNumber.ToString();
        metalText.text = "M:" + metalNumber.ToString();
        plasticWasteText.text = "PW:" + plasticWasteNumber.ToString();
        plasticText.text = "P:" + plasticNumber.ToString();
        chipText.text = "Ch:" + chipNumber.ToString();
        influencePointText.text = "Inf:" + influencePoints.ToString();
    }
}

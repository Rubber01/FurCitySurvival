using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReputationController : MonoBehaviour
{
    private TMP_Text levelText;
    private Image experienceBarImage;
    private ReputationSystem levelSystem;
    private ReputationSystemAnimated levelSystemAnimated;

    private void Awake()
    {
        levelText = transform.Find("LevelText").GetComponent<TMP_Text>();
        experienceBarImage = transform.Find("ExperienceBar").Find("Bar").GetComponent<Image>();
        transform.Find("Experience5Btn").GetComponent<Button>().onClick.AddListener(() => levelSystem.AddExperience(5));
        Debug.Log("bottone " + transform.Find("Experience5Btn").GetComponent<Button>());
        transform.Find("Experience50Btn").GetComponent<Button>().onClick.AddListener(() => levelSystem.AddExperience(50));
        transform.Find("Experience500Btn").GetComponent<Button>().onClick.AddListener(() => levelSystem.AddExperience(500));
        //levelText.text = "Level 5";


    }
    private void SetExperienceBarSize(float experinceNormalized)
    {
        Debug.Log("experinceNormalized " + experinceNormalized);
        experienceBarImage.fillAmount = experinceNormalized;
    }
    private void SetLevelNumber(int levelNumber)
    {
        Debug.Log("Level number " + levelNumber);
        levelText.text = "Level " + (levelNumber + 1);
    }
    public void SetLevelSystem(ReputationSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }

    public void SetLevelSystemAnimated(ReputationSystemAnimated  levelSystemAnimated)
    {
        //setta l'oggetto ReputationSystem
        this.levelSystemAnimated = levelSystemAnimated;

        //aggiorna i valori di partenza
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());

        //iscriviti all'evento
        levelSystemAnimated.OnExperinceChanged += LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;

        levelSystemAnimated.OnExperinceChangedLower += LevelSystemAnimated_OnExperienceChangedLower;
        levelSystemAnimated.OnLevelChangedLower += LevelSystemAnimated_OnLevelChangedLower;

    }
    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e)
    {
        //se cambia livello, aggiorna il testo
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());

    }
    private void LevelSystemAnimated_OnExperienceChanged(object sender, System.EventArgs e)
    {
        //se cambia l'esperienza, aggiorna la barra
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());   
    }
    private void LevelSystemAnimated_OnLevelChangedLower(object sender, System.EventArgs e)
    {
        //se cambia livello, aggiorna il testo
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());

    }
    private void LevelSystemAnimated_OnExperienceChangedLower(object sender, System.EventArgs e)
    {
        //se cambia l'esperienza, aggiorna la barra
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());
    }
}

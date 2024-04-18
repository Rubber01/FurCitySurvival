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
        //setta l'oggetto ReputationSystem
        this.levelSystem = levelSystem;

        //aggiorna i valori di partenza
        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

        //iscriviti all'evento
        levelSystem.OnExperinceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;

    }
    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        //se cambia livello, aggiorna il testo
        SetLevelNumber(levelSystem.GetLevelNumber());

    }
    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        //se cambia l'esperienza, aggiorna la barra
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());   
    }
}

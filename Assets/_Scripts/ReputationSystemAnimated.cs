using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationSystemAnimated 
{
    private ReputationSystem levelSystem;
    private bool isAnimating;

    private int level;
    private int experience;
    private int experienceToNextLevel;

    public ReputationSystemAnimated(ReputationSystem levelSystem)
    {
        SetLevelSystem(levelSystem);

    }
    public void SetLevelSystem(ReputationSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        level = levelSystem.GetLevelNumber();
        experience = levelSystem.GetExperienceNumber();
        experienceToNextLevel = levelSystem.GetExperienceToNextLevelNumber();
        levelSystem.OnExperinceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }
    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;

    }
    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }
    public void Update()
    {
        if (isAnimating)
        {
            if (level < levelSystem.GetLevelNumber())
            {
                //livello locale<target level
                AddExperience();
            }
            else
            {
                //livello locale=target level
                if (experience < levelSystem.GetExperienceNumber())
                {
                    AddExperience();
                }
                else
                {
                    isAnimating = false;
                }
            }
        }
        Debug.Log("Level " + level + " Experience " + experience);
    }
    private void AddExperience()
    {
        experience++;
        if (experience >= experienceToNextLevel)
        {
            level++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ReputationSystemAnimated 
{
    public event EventHandler OnExperinceChanged;
    public event EventHandler OnLevelChanged;

    private ReputationSystem levelSystem;
    private bool isAnimating;
    private float updateTimer;
    private float updateTimerMax;
    private int level;
    private int experience;
    private int experienceToNextLevel;

    public ReputationSystemAnimated(ReputationSystem levelSystem)
    {
        SetLevelSystem(levelSystem);
        updateTimerMax = 0.016f;
        FunctionUpdater.OnUpdate += Update;
    }
    ~ReputationSystemAnimated()
    {
        FunctionUpdater.OnUpdate -= Update;
    }
    public void SetLevelSystem(ReputationSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        level = levelSystem.GetLevelNumber();
        experience = levelSystem.GetExperienceNumber();
        experienceToNextLevel = levelSystem.GetExperienceToNextLevelNumber(levelSystem.GetLevelNumber());
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
        Debug.Log("Update chiamato");
        if (isAnimating)
        {
            updateTimer += Time.deltaTime;
            while (updateTimer > updateTimerMax)
            {
                updateTimer -= updateTimerMax;
                UpdateAddExperience();
            }
            
        }
        Debug.Log("Level " + level + " Experience " + experience);
    }
    private void UpdateAddExperience()
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
    private void AddExperience()
    {
        experience++;
        if (experience >= experienceToNextLevel)
        {
            level++;
            experience = 0;
            if(OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if (OnExperinceChanged != null) OnExperinceChanged(this, EventArgs.Empty);

    }

    public int GetLevelNumber()
    {
        return level;
    }
 
    public float GetExperienceNormalized()
    {
        return (float)experience / experienceToNextLevel;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReputationSystem
{
    public event EventHandler OnExperinceChanged;
    public event EventHandler OnLevelChanged;

    private int level;
    private int experience;
    private int experienceToNextLevel;
    
    public ReputationSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 100;
    }
    public void AddExperience(int amount)
    {
        Debug.Log("Add experince " + amount);
        experience += amount;
        Debug.Log("experince " + experience + " experienceToNextLevel "+ experienceToNextLevel);

        while (experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if (OnExperinceChanged != null) OnExperinceChanged(this, EventArgs.Empty);
    }
    public int GetLevelNumber()
    {
        return level;
    }
    public int GetExperienceNumber()
    {
        return experience;
    }
    public int GetExperienceToNextLevelNumber()
    {
        return experienceToNextLevel;
    }
    public float GetExperienceNormalized()
    {
        return (float)experience / experienceToNextLevel;
    }
}

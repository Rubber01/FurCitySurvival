using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReputationSystem
{
    public event EventHandler OnExperinceChanged;
    public event EventHandler OnLevelChanged;
    public event EventHandler OnExperinceChangedLower;
    public event EventHandler OnLevelChangedLower;
    private int coeff=2;
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
            PlayerManager.henchmenSlots += 1;
            GameObject linker = GameObject.Find("FuncitionUpdater&ReputationLinker");
            ReputationLinker replink = linker.GetComponent<ReputationLinker>();
            if (linker != null && replink != null)
            {
                foreach (BasicTile bt in replink.basicTile)
                {
                    if (bt.RepCost <= level+1 && bt.isLocked)
                    {
                        if (bt.RepCost < 0)
                        {
                            //Do Nothing
                        }
                        else
                        {
                            bt.UnlockTile();
                        }
                    }
                }
            }
            else
            {
                Debug.Log("linker not found");
            }

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
    public int GetExperienceToNextLevelNumber(int level)
    {
        return experienceToNextLevel^(level* coeff);
    }
    public float GetExperienceNormalized()
    {
        return (float)experience / experienceToNextLevel;
    }
}

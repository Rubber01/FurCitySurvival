using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationLinker : MonoBehaviour
{
    [SerializeField] private ReputationController reputationController;
    //[SerializeField] private Player player;
    private void Awake()
    {
        ReputationSystem reputationSystem = new ReputationSystem();
        reputationController.SetLevelSystem(reputationSystem);
        //player.SetLevelSystem(reputationSystem); 

        ReputationSystemAnimated levelSystemAnimated = new ReputationSystemAnimated(reputationSystem);
        reputationController.SetLevelSystemAnimated(levelSystemAnimated);
    }
}

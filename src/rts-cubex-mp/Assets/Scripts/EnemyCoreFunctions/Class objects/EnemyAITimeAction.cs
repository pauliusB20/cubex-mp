using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAITimeAction : System.Object
{
    [Header("Main configuration parameters")]
    [SerializeField] int actionPerformedTime;
    [SerializeField] int howManyWorkers;
    [SerializeField] int howManyCollectors;
    [SerializeField] int howManyBarracks;
    [SerializeField] int howManyTroops;
    [SerializeField] int howManyTurrets;
    [SerializeField] string troopCode;
    [SerializeField] int requiredMlCenterTechLvl;
    [SerializeField] int requiredTroopTechLvl;
    [SerializeField] int requiredBaseLvl;
    [SerializeField] int requiredTurretLvl;

    //Add methods for getting required parameters
}

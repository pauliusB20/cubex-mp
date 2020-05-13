using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerEnemyAI : MonoBehaviour
{
    [SerializeField] MTroopUnits currentTroopList;   
    [SerializeField] MBaseUpgrades baseTroopUpgradesList;
    [SerializeField] MBaseUpgrades baseBuildingUpgradesList;

    [SerializeField] MTroopUnits originalTroopList;
    [SerializeField] MBaseUpgrades originalBaseTroopUpgradesList;
    [SerializeField] MBaseUpgrades originalBaseBuildUpgradesList;

    private void OnDestroy() 
    {
        handleTroopUpgrades();
        cleanUpgrades();    
    }
    private void handleTroopUpgrades()
    {
        var _originalTroopList = originalTroopList.getTroopList();
        var _currentList = currentTroopList.getTroopList();
        var resetTroopList = originalTroopList.getTroopList()[0];
        
        for (int tIndex = 0; tIndex < _currentList.Count; tIndex++)
        {
            _currentList[tIndex].setHealth(_originalTroopList[tIndex].getStartingHealth());
            _currentList[tIndex].setDmgPoints(_originalTroopList[tIndex].getSDmgPoints());
        }
         

         resetTroopList.resetStateParameters();
    }
    private void cleanUpgrades()
    {
        for (int bUIndex = 0; bUIndex < baseTroopUpgradesList.getTechLevels().Count; bUIndex++)
        {
            baseTroopUpgradesList.getTechLevels()[bUIndex]
                .setStartingLvl(originalBaseTroopUpgradesList.getTechLevels()[bUIndex]
                .getLvl());

             baseBuildingUpgradesList.getTechLevels()[bUIndex]
                .setStartingLvl(originalBaseBuildUpgradesList.getTechLevels()[bUIndex]
                .getLvl());
        }
    }
}

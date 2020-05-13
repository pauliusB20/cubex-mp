using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopUnitUpgradeManager : MonoBehaviour
{
    //NOTE: add a system that could revert changes on the file...
    [Header("Main Configuration parameters")]
    [SerializeField] MBaseUpgrades troopUpgradeData;
    [SerializeField] MTroopUnits troops;
    [SerializeField] int currentTechLvl = 0;
    [SerializeField] string cRezerveName = "Rtul";
    [SerializeField] bool isUpgradedTechLvl = false; 
    [Header("Main troop chraracteristics")]
    [SerializeField] string troopTag = "enemyTroop";
    [SerializeField] string troopRangeTag = "eADome";

    private GameObject enemyBase;
    private AIResourcesManagement am;
    private EnemyAIBase eb;
    private AITroopManager atm;
    private ResearchCenterManager rcm;
    private EnemyTag enemyTag;
    private void Start() {

        rcm = GetComponent<ResearchCenterManager>();
        enemyTag = GetComponent<EnemyTag>();
        enemyBase = new EnemyTagMgr().getCurrentEnemyBaseByTag(enemyTag.getEnemyTag(), "EnemyBase");
        am = enemyBase.GetComponent<AIResourcesManagement>();
        eb = enemyBase.GetComponent<EnemyAIBase>();
        atm = enemyBase.GetComponent<AITroopManager>();
    }
    private void Update() {
        

        if (!isUpgradedTechLvl && (rcm != null && rcm.Upgraded()))
            increaseTroopTechLvl();
    }
//Optimize this..
    public int getCurrentTroopTechLevel()
    {
        return currentTechLvl;
    }
    private void increaseTroopTechLvl()
    {
        //Upgrading troop units in the sr. object file conf.

         var rCMIndex = rcm.getResearchTechLevel();
         
         //Checks if current level up range reached it's max level cap... If it reached, then jump to another level
         if (troopUpgradeData.getTechLevels()[rCMIndex].ReachedMax())
         {
            rcm.upgradeResearchLvl();
            rCMIndex = rcm.getResearchTechLevel();
         }

        if (am.buy("Rtul", troopUpgradeData.getTechLevels()[rCMIndex].getUpgradePrice()))
        {
         if (troopUpgradeData.getTechLevels()[rCMIndex].ReachedMax()) 
         {
             Debug.Log("Reached max tech level upgrade capacity! Need to upgraded tech level now!...");
             return;
         }
         troopUpgradeData.getTechLevels()[rCMIndex].increaseCurrentLvl();
         if (troopUpgradeData.getTechLevels()[rCMIndex].upgradedLevelState() && !isUpgradedTechLvl)
         {
            currentTechLvl = troopUpgradeData.getTechLevels()[rCMIndex].getLvl();
           //Upgrade system for conf. file stored troop units..  
           bool upgradedInFile = false, upgradedInGameArea = false;
           int upgradeIndex = 0;
           for (int tIndex = 0; tIndex < troops.getTroopList().Count; tIndex++)
           {
               troops.getTroopList()[tIndex]
               .setHealth( 
                   troops.getTroopList()[tIndex].getStartingHealth() + 
                    troopUpgradeData.getTechLevels()[rCMIndex].getHealthUpgradeOffset()
                   );
               troops.getTroopList()[tIndex]
               .setDmgPoints( 
                   troops.getTroopList()[tIndex].getSDmgPoints() + 
                    troopUpgradeData.getTechLevels()[rCMIndex].getDamagePointsOffset()
                   );

                if (troops.getTroopList()[tIndex].parUpdated()) upgradeIndex++;
               
           }
            if (upgradeIndex > 0) upgradedInFile = true;


            var troopsInArea = GameObject.FindGameObjectsWithTag(troopTag);

            if (troopsInArea.Length > 0 && troopsInArea != null)
            {
                Debug.Log("Upgrading troops in game area");
                for (int troopInAreaIndex = 0; troopInAreaIndex < troopsInArea.Length; troopInAreaIndex++)
                {
                    if (troopsInArea[troopInAreaIndex].GetComponent<TroopCode>().getCode() == "tlu") 
                    {    
                        var spawnedTroop = troopsInArea[troopInAreaIndex];   
                        var spawnedTroopDome = FindGameObjectInChildWithTag (troopsInArea[troopInAreaIndex], troopRangeTag);   
                        var spinGun = spawnedTroopDome.GetComponent<AutoAttackTest>(); 
                        var laserBeam = spinGun.getSpinGun().GetComponent<FireLaser>();

                        spawnedTroop.GetComponent<HealthEnemyAI>()
                            .setHealth(
                                spawnedTroop.GetComponent<HealthEnemyAI>().getHealth() + 
                                    troopUpgradeData.getTechLevels()[0].getHealthUpgradeOffset()
                            );
                        laserBeam.setMaxDmgPoints(
                            laserBeam.getDmgPoints() +  troopUpgradeData.getTechLevels()[rCMIndex].getDamagePointsOffset()
                            );
                        
                    }
                    if (troopsInArea[troopInAreaIndex].GetComponent<TroopCode>().getCode() == "thu")
                    {
                        var spawnedTroop = troopsInArea[troopInAreaIndex];  
                        var spawnedTroopDome = FindGameObjectInChildWithTag (troopsInArea[troopInAreaIndex], troopRangeTag);
                        var spinGun = spawnedTroopDome.GetComponent<AutoAttackTest>(); 
                        var laserBeam = spinGun.getSpinGun().GetComponent<FireLaser>();
                        
                        spawnedTroop.GetComponent<HealthEnemyAI>()
                            .setHealth(
                                spawnedTroop.GetComponent<HealthEnemyAI>().getHealth() +
                                     troopUpgradeData.getTechLevels()[1].getHealthUpgradeOffset()
                            ); 
                         laserBeam.setMaxDmgPoints(
                            laserBeam.getDmgPoints() +  troopUpgradeData.getTechLevels()[rCMIndex].getDamagePointsOffset()
                            );
                    }

                }
                upgradedInGameArea = true;
            }
          //Upgrade system for troop units in game area..

          isUpgradedTechLvl = (upgradedInFile == upgradedInGameArea); 
          Debug.Log(isUpgradedTechLvl ? "Succesfully upgraded troops In game area" : "Error! no upgrades were done");
         }
         else
         {
             Debug.Log("Error can not upgrade troop tech level");
         }
       }
       else
       {
           Debug.Log("ERROR.. Insuficient credits for troop upgrades!");
       }
    }
    
    public GameObject FindGameObjectInChildWithTag (GameObject parent, string tag)
     {
         Transform t = parent.transform;
 
         for (int i = 0; i < t.childCount; i++) 
         {
             if(t.GetChild(i).gameObject.tag == tag)
             {
                 return t.GetChild(i).gameObject;
             }
                 
         }
             
         return null;
     }
}

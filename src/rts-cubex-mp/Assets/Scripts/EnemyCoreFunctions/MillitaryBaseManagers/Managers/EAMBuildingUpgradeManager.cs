using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAMBuildingUpgradeManager : MonoBehaviour
{
    [Header("Main Configuration parameters")]
    [SerializeField] MBaseUpgrades buildUpgradeData;
    [SerializeField] int currentTechLvl = 0;
    [SerializeField] string cRezerveName = "RtulB";
    [SerializeField] bool isUpgradedTechLvl = false; 

    [Header("Main structure chraracteristics")]
    [SerializeField] string buildTag = "enemyTroop";
    [SerializeField] string buildRangeTag = "eADome";

    [Header("Commands for choosing on what to upgrade")]
    [SerializeField] string buildingCode = "empty";
    [SerializeField] string bUpgradeRezerve = "empty";    


    private GameObject enemyBase;
    private AIResourcesManagement am;
    private EnemyAIBase eb;
    private ResearchCenterManager rcm;
    private EnemyTag enemyTag;

    void Start()
    {
        rcm = GetComponent<ResearchCenterManager>();
        enemyTag = GetComponent<EnemyTag>();    
        enemyBase = new EnemyTagMgr().getCurrentEnemyBaseByTag(enemyTag.getEnemyTag(), "EnemyBase");
        am = enemyBase.GetComponent<AIResourcesManagement>();
        eb = enemyBase.GetComponent<EnemyAIBase>();
    }

    // Update is called once  per frame
    void Update()
    {
        if (!isUpgradedTechLvl && buildsPresent())
        {
            upgradeBuilding(buildingCode, buildTag);
        }
    }
    private bool buildsPresent()
    {
        return new EnemyTagMgr().locateEnemyObjectsWithTag(enemyTag.getEnemyTag(), buildTag).Length > 0;
    }
    private void upgradeBuilding(string bCode, string buildTag)
    {
       var buildingTechLvl = rcm.getResearchTechLevel();

        if (buildUpgradeData.getTechLevels()[buildingTechLvl].ReachedMax())
         {
            rcm.upgradeResearchLvl();
            buildingTechLvl = rcm.getResearchTechLevel();
         }
        var buildingPrice = buildUpgradeData.getTechLevels()[buildingTechLvl].getUpgradePrice();
        
        buildUpgradeData.getTechLevels()[buildingTechLvl].increaseCurrentLvl();

        if (buildUpgradeData.getTechLevels()[buildingTechLvl].upgradedLevelState() && !isUpgradedTechLvl)
        {
        switch(bCode)
        {
            case "eamt":
                if (am.buy(bUpgradeRezerve, buildingPrice))
                {
                    var buildings = new EnemyTagMgr().locateEnemyObjectsWithTag(enemyTag.getEnemyTag(), buildTag);

                    for (int bIndex = 0; bIndex < buildings.Length; bIndex++)
                    {
                        if (buildings[bIndex].GetComponent<EAMBuildingCode>().getBuildingCode() == bCode)
                        {
                            var buildingHealth = buildings[bIndex].GetComponent<HealthEnemyAI>();
                            var spawnedTurretDome = FindGameObjectInChildWithTag (buildings[bIndex], buildRangeTag);
                            var spinGun = spawnedTurretDome.GetComponent<Turret>(); 
                            var laserBeam = spinGun.getSpinGun().GetComponent<TurretFireMotion>();

                            buildingHealth.setHealth(
                                    buildingHealth.getHealth() + buildUpgradeData.getTechLevels()[buildingTechLvl].getHealthUpgradeOffset()
                            );
                            laserBeam.setMaxDmgPoints(
                                    laserBeam.getMaxDmgPoints() + buildUpgradeData.getTechLevels()[buildingTechLvl].getDamagePointsTurretOffset()
                            );
                            
                            isUpgradedTechLvl = true;
                            Debug.Log("Upgraded turrets!");
                            return;
                        }
                    }
                }
                else
                {
                    Debug.Log("ERROR: Do not have enough credits for building upgrades!");
                    return;
                }
            break;

            case "eamd":
                if (am.buy(bUpgradeRezerve, buildingPrice))
                {
                    var buildings = new EnemyTagMgr().locateEnemyObjectsWithTag(enemyTag.getEnemyTag(), buildTag);

                    for (int bIndex = 0; bIndex < buildings.Length; bIndex++)
                    {
                        if (buildings[bIndex].GetComponent<EAMBuildingCode>().getBuildingCode() == bCode)
                        {
                            var buildingHealth = buildings[bIndex].GetComponent<HealthEnemyAI>();
                            
                            buildingHealth.setHealth(
                                    buildingHealth.getHealth() + buildUpgradeData.getTechLevels()[buildingTechLvl].getWallUpgradeHealth()
                            );
                            Debug.Log("Upgraded walls!");
                            isUpgradedTechLvl = true;
                            return;
                        }
                    }
                }
                else
                {
                    Debug.Log("ERROR: Do not have enough credits for building upgrades!");
                    return;
                }
            break;

               case "eabs":
                if (am.buy(bUpgradeRezerve, buildingPrice))
                {
                    var buildings = new EnemyTagMgr().locateEnemyObjectsWithTag(enemyTag.getEnemyTag(), buildTag);

                    for (int bIndex = 0; bIndex < buildings.Length; bIndex++)
                    {
                        if (buildings[bIndex].GetComponent<EAMBuildingCode>().getBuildingCode() == bCode)
                        {
                            var buildingHealth = buildings[bIndex].GetComponent<HealthEnemyAI>();
                            
                            buildingHealth.setHealth(
                                    buildingHealth.getHealth() + buildUpgradeData.getTechLevels()[buildingTechLvl].getHealthUpgradeOffset()
                            );
                            Debug.Log("Upgraded walls!");
                            isUpgradedTechLvl = true;
                            return;
                        }
                    }
                }
                else
                {
                    Debug.Log("ERROR: Do not have enough credits for building upgrades!");
                    return;
                }
            break;
        }
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

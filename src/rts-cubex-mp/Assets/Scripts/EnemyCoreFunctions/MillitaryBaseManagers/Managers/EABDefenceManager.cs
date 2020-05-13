using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EABDefenceManager : MonoBehaviour
{
    [Header("Main defence manager conf. parameters")]
    [SerializeField] string turretBCode = "empty";
    [SerializeField] string wallBCode = "empty";
    [SerializeField] string crRTurretName = "empty";
    [SerializeField] string crRWallName = "empty";
    [SerializeField] string spawnSiteName = "EMSite";
    [SerializeField] string[] dTypeCode;
    [SerializeField] bool isSpawningDefence = false;
    [SerializeField] int requiredTurretAmount = 1;
    [SerializeField] int requiredWallAmount = 2;
    [SerializeField] List<Transform> defenceSpawnPoints;

    private EnemyAIBase eb;
    private AIResourcesManagement am;
    private WorkerAIManager wam;
    private EnemyBaseTag eBaseTag;
    private RandomNumberGenerator randomNumber;
   
    void Start()
    {
       eBaseTag = GetComponent<EnemyBaseTag>();
       eb = GetComponent<EnemyAIBase>(); 
       am = GetComponent<AIResourcesManagement>();
       wam = GetComponent<WorkerAIManager>();
    
       randomNumber = new RandomNumberGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        if (AmountEnough(requiredTurretAmount, dTypeCode[0]) && AmountEnough(requiredWallAmount, dTypeCode[1]))
        {
            return;
        }
         if (!AmountEnough(requiredTurretAmount, dTypeCode[0]))
         {
            buildDefenceStructure(dTypeCode[0]);
            //Debug.Log("Spawning a turret..");
         }  
         if (!AmountEnough(requiredWallAmount, dTypeCode[1]))
         {
            buildDefenceStructure(dTypeCode[1]);
            //Debug.Log("Spawning a walls..");
         } 

       
    }
    private void buildDefenceStructure(string dType)
    {

             var checkIfTurret = (dType == "tr");
             var checkIfWall = (dType == "dwall");

             var dBuilding = checkIfTurret ? eb.getBaseStructureList().getBuilding(turretBCode) : 
                               checkIfWall ? eb.getBaseStructureList().getBuilding(wallBCode) : null;
             var dPrice = checkIfTurret ? eb.getBaseStructureList().getBuildingPrice(turretBCode) :
                            checkIfWall ? eb.getBaseStructureList().getBuildingPrice(wallBCode) : 0; 
              var dPriceInEnergon = checkIfTurret ? eb.getBaseStructureList().getEnergonPrice(turretBCode) :
                            checkIfWall ? eb.getBaseStructureList().getEnergonPrice(wallBCode) : 0;  

             var creditRezerveName = checkIfTurret ? crRTurretName : checkIfWall ? crRWallName : null;

             var canBuild = (dBuilding != null) && (dPrice > 0 && creditRezerveName != null);
             //Debug.Log("Building Dst state: " + canBuild);      
             if (wam.areThereAnySpawnedFreeWorkers())
             {
                if (am.buy(creditRezerveName, dPrice, dPriceInEnergon) && canBuild)
                {
                    Debug.Log("------------>Defence structure " + creditRezerveName + " bought successfully!<-----------");
                    wam.assignExistingWorkerToTask(
                        true, 
                            spawnSiteName, 
                                defenceSpawnPoints[randomNumber.generateRandomNumber(0, defenceSpawnPoints.Count)].position, 
                                    dBuilding
                                ); 
                     Debug.Log("Assigned a task to an exisiting worker..");
                   // isSpawningDefence = false;
                    return;
                }
                else
                {
                  //  Debug.Log("Insuficient credits! ERORR...");
                    return;
                }
             }
             else
             {
                  //If worker count has reached max, then return and do nothing else..
                  if (wam.workerCountReachedMax()) return;
                  
               if (am.buy(creditRezerveName, dPrice)  && canBuild)
                {
                 Debug.Log("------------>Defence structure bought successfully!<-----------");
                    wam.spawnWorker(
                        true, 
                            spawnSiteName, 
                                 defenceSpawnPoints[randomNumber.generateRandomNumber(0, defenceSpawnPoints.Count)].position, 
                                    dBuilding
                                ); 
                   // isSpawningDefence = false;
                    return;
                 }
                 else
                 {
                     Debug.Log("Insuficient credits! ERORR...");
                     return;
                 }
            }
            // Add more code here...
    }        
    
    private bool AmountEnough(int rAmount, string bCode)
    {
        var enemyMgr = new EnemyTagMgr();
        if (enemyMgr.enemyObjectsExist("EMDef"))
        {
            var existingBuildings = new EnemyTagMgr().locateEnemyObjectsWithTag(eBaseTag.getEnemyMBaseTag(), "EMDef");
        // var existingBuildings = FindObjectsOfType<EAMBuildingCode>();
            int amount = 0;
            for (int buildIndex = 0; buildIndex < existingBuildings.Length; buildIndex++)
            {   
                var building = existingBuildings[buildIndex].GetComponent<EAMBuildingCode>();
                if (building.getBuildingCode() == bCode)
                {
                amount++;
                }
            }

            return rAmount == amount;
        }
        return false;
    }

}

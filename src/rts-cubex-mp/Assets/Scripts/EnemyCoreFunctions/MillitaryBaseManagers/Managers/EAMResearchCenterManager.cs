using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAMResearchCenterManager : MonoBehaviour
{
    [SerializeField] List<string> rBCodes;
    [SerializeField] int creditRezerveIndex = 4;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] int rCIndex = 0;
    [Header("Energon/Credits reserve names")]
    [SerializeField] string crRMBaseRecName = "empty";
    [SerializeField] string crRMBUBaseRecName = "empty";
    [Tooltip("Current required research center amount")]
    [SerializeField] int rAmount = 1;
    private EnemyAIBase eb;
    private AIResourcesManagement arm;
    private WorkerAIManager wam;
    private EnemyBaseTag eBaseTag;
    private void Start () 
    {
        eBaseTag = GetComponent<EnemyBaseTag>();
        eb = GetComponent<EnemyAIBase> ();
        arm = GetComponent<AIResourcesManagement> ();
        wam = GetComponent<WorkerAIManager> ();
    }

    private void Update () 
    {
        //First check if the state for spawning workers is true..
        if (!enoughEMBResearchCentersAmount(rAmount))
            spawnResourceManager (rBCodes[rCIndex]);
    }

    private void spawnResourceManager (string rCBcodes) 
    {

        switch (rCBcodes) 
        {

            case "eamrc":
                //    Debug.Log(arm.getCreditReserves()[creditRezerveIndex].getCreditReserveCap());

              
                    //Debug.Log (!wam.areThereAnySpawnedFreeWorkers ());
                    if (arm.buy ("RMCenter", eb.getBaseStructureList().getBuildingPrice ("eamrc"), 
                            eb.getBaseStructureList().getEnergonPrice ("eamrc")) && !wam.areThereAnySpawnedFreeWorkers ())
                    {
                        //If worker count has reached max, then return and do nothing else..
                        if (wam.workerCountReachedMax()) return;

                        wam.spawnWorker (true, "RMCenter", spawnPoints[0].position, eb.getBaseStructureList ().getBuilding ("eamrc"));
                        return;
                    }
                    if (arm.buy ("RMCenter", eb.getBaseStructureList ().getBuildingPrice ("eamrc"),
                                 eb.getBaseStructureList().getEnergonPrice ("eamrc")) && wam.areThereAnySpawnedFreeWorkers ()) 
                    {
                        Debug.Log ("Enemy A.I. ml. reasearch center bought successfully");
                        //Spawn a m.b.worker...
                        wam.assignExistingWorkerToTask (true, "RMCenter", spawnPoints[0].position, eb.getBaseStructureList ().getBuilding ("eamrc"));
                        return;

                    } 
                    else 
                    {
                        Debug.Log ("ERROR! can't buy Enemy A.I. ml. reasearch center ");
                        return;
                    }      
               
                break;
                 case "eambrc":
                //    Debug.Log(arm.getCreditReserves()[creditRezerveIndex].getCreditReserveCap());

                
                    //Debug.Log (!wam.areThereAnySpawnedFreeWorkers ());
                    if (arm.buy ("RMCenter", eb.getBaseStructureList ().getBuildingPrice ("eambrc")) && !wam.areThereAnySpawnedFreeWorkers ())
                    {
                         //If worker count has reached max, then return and do nothing else..
                        if (wam.workerCountReachedMax()) return;

                        wam.spawnWorker (true, "RMCenter", spawnPoints[1].position, eb.getBaseStructureList ().getBuilding ("eambrc"));
                        return;
                    }
                    if (arm.buy ("RMCenter", eb.getBaseStructureList ().getBuildingPrice ("eambrc")) && wam.areThereAnySpawnedFreeWorkers ()) 
                    {
                        Debug.Log ("Enemy A.I. ml. reasearch center bought successfully");
                        //Spawn a m.b.worker...
                        wam.assignExistingWorkerToTask (true, "RMCenter", spawnPoints[1].position, eb.getBaseStructureList ().getBuilding ("eambrc"));
                        return;

                    } 
                    else 
                    {
                        Debug.Log ("ERROR! can't buy Enemy A.I. ml. reasearch center ");
                        return;
                    }                
               
                break;
        }
    }
    public bool enoughEMBResearchCentersAmount (int maxAmount)
    {
        //var eBMRCenter = FindObjectsOfType<ResearchCenterManager> ();
        var enemytagMgr = new EnemyTagMgr();
        if (!enemytagMgr.enemyObjectsExist("EBRCenter")) return false;

        var eBMRCenter = enemytagMgr.locateEnemyObjectsWithTag(eBaseTag.getEnemyMBaseTag(), "EBRCenter");
        return eBMRCenter.Length >= maxAmount;
    }
}
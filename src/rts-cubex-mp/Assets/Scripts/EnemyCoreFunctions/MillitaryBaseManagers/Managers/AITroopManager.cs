using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITroopManager : MonoBehaviour, AITroopManagerInterface
{
    //TODO add a logical system for purchasing buildings
    [Header("Barracks structure parameters")]
    [SerializeField] List<Transform> buildingPoints;
    [SerializeField] int barracksPrice = 20;
    [SerializeField] int barracksEnergonPrice = 5;
    [SerializeField] string barracksName = "Barracks";
    [SerializeField] string barracksBuildingCode = "eambb";
    // [SerializeField] GameObject troopBarracks;
    [SerializeField] float buildDelay = 5f;
    [SerializeField] string enemyTroopTag = "enemyTroop";
    
    [Header("Troop code generator parameters")]    
    [SerializeField] int requiredAmount = 1;
    [SerializeField] List<string> tMemCodes;

    [Header("Troop unit configuration parameters")]
    [SerializeField] bool waveOfAttackTroopsSpawned = false; 
    [SerializeField] bool receivedSignal = false;
    [SerializeField] int troopCodeOffset = 0;
    [SerializeField] int lightTroopUnitPrice = 5;
    [SerializeField] bool isNeedingSearchParty = false;
    [SerializeField] bool isNeedingPatrolUnits = false;
    [SerializeField] bool isNeedingForAttack = false;
    [SerializeField] int maxUnitsForPatroll = 3;
    [SerializeField] int maxUnitsForAttack = 5;
    [SerializeField] int maxUnitsForSearch = 1;
    [SerializeField] bool enoughTroopsForPatrol = false;
    [SerializeField] bool enoughTroopsForAttack = false;
    [SerializeField] bool enoughTroopsForSearch = false;
    [SerializeField] bool findPlayerBase = false;
    [SerializeField] bool isTeamLeaderFound = false;
    [SerializeField] bool troopsFollowingLeader = false;
    [SerializeField] List<string> troopCodes;
    [Header("Patrol wave for m. b. units")]
    [SerializeField] List<Vector3> patrolPoints;
    //Cached references
    AIResourcesManagement am;
    WorkerAIManager wm;
    EnemyAIBase eb;
    //Points, which will hold the constructed path to the player military base
    private List<Vector3> travelPointsToEnemyBase = new List<Vector3>();
    private EnemyBaseTag eBaseTag;
    [SerializeField] int structIndex;
    int prieviousRandom;
    bool isPurchasing;
    int randomStructureSpawnPosition;
    //Fix troop movment...
    void Start()
    {
       eb = GetComponent<EnemyAIBase>();
       am = GetComponent<AIResourcesManagement>();
       wm = GetComponent<WorkerAIManager>();
       eBaseTag = GetComponent<EnemyBaseTag>();
       randomStructureSpawnPosition = 0; 
       structIndex = 0;
       prieviousRandom = 0;
       isPurchasing = false;
        
          //Once appears this object in the game it tryes to add it's position to the troop patrol wave list..
       patrolPoints.Add(transform.position);
    }

    // Update is called once per frame
    void Update()
    {    
      
        if ((!requiredBarracksAmount(requiredAmount) && (eb.getDepositFoundState() || eb.areDepositsNotInArea()) ) && wm.spawAgain())
        {
            buildBarracks();
        }
        if (requiredBarracksAmount(requiredAmount))
        {
                //Troop managment logic...               
                if (receivedSignal && !waveOfAttackTroopsSpawned)
                {
                    isNeedingForAttack = true;
                    enoughTroopsForAttack = false;
                    Debug.Log("Siganl received to respawn troops for attack!");
                }
                if (eb.areDepositsNotInArea() && !enoughTroopsForSearch)
                {
                    isNeedingSearchParty = eb.areDepositsNotInArea();
                    if (hasEnoughTroopsFor(maxUnitsForSearch, false, false, isNeedingSearchParty))
                    {
                        enoughTroopsForSearch = true;
                    }
                }
                if ((eb.getDepositFoundState() && !eb.areDepositsNotInArea()) && !enoughTroopsForPatrol)
                {
                    //Prepares defending troops...
                    isNeedingPatrolUnits = eb.getDepositFoundState();
                    
                    if (hasEnoughTroopsFor(maxUnitsForPatroll, false, isNeedingPatrolUnits, false))
                    {
                        enoughTroopsForPatrol = true;
                        isNeedingForAttack = true;
                    }                   
                }
                if (((eb.getDepositFoundState() && !eb.areDepositsNotInArea()) && isNeedingForAttack) && !enoughTroopsForAttack)
                {
                    if (hasEnoughTroopsFor(maxUnitsForAttack, isNeedingForAttack, false, false))
                    {
                        enoughTroopsForAttack = true;
                        isNeedingForAttack = false;
                        //Configuring a strike team..
                        //var spawnedAttackTroops = GameObject.FindGameObjectsWithTag(enemyTroopTag);
                        var spawnedAttackTroops = new EnemyTagMgr().locateEnemyObjectsWithTag(eBaseTag.getEnemyMBaseTag(), "enemyTroop");
                       
                        var attackTroops = new List<GameObject>();
                       
                       // Debug.Log("Founded " + attackTeamMemCount + " attack team members");
                        if (attackTroops != null)
                        {
                            for (int sIndex = 0; sIndex < spawnedAttackTroops.Length; sIndex++)
                            {
                                
                                if (spawnedAttackTroops[sIndex].GetComponent<AITBaseSearch>().AssaultTeamMember())
                                {
                                    attackTroops.Add(spawnedAttackTroops[sIndex]);
                                }
                            }

                          // Debug.Log("Founded " + attackTroops.Count + " attack team members");
                           GameObject currentTeamLeader = null;
                            //Finding leader...
                            if (!isTeamLeaderFound && attackTroops.Count > 0)
                            {
                                currentTeamLeader = attackTroops[Random.Range(0, attackTroops.Count - 1)];
                            //    Debug.Log("Team leader index: " + Random.Range(0, attackTroops.Count - 1));
                                isTeamLeaderFound = (currentTeamLeader != null);                               
                                currentTeamLeader.GetComponent<AITBaseSearch>().setAsLeader(isTeamLeaderFound);  
                                currentTeamLeader.GetComponent<TroopTCode>().setTroopCode(tMemCodes[troopCodeOffset] + troopCodeOffset);
                              //  Debug.Log(isTeamLeaderFound ? "Founded a strike team leader" : "Error no team leader found!");                                                              
                            }
                            //Make others in team to follow leader configuration..
                            if ((isTeamLeaderFound && !troopsFollowingLeader) && attackTroops.Count > 0)
                            {
                                
                                for (int tIndex = 0; tIndex < spawnedAttackTroops.Length; tIndex++)
                                {
                                    var assaultTroop = spawnedAttackTroops[tIndex].GetComponent<AITBaseSearch>();
                                    if (assaultTroop.AssaultTeamMember() && !assaultTroop.amILeader())
                                    {
                                        spawnedAttackTroops[tIndex].
                                                GetComponent<FollowLeaderAction>().
                                                                FollowerOfTheLeader(true, currentTeamLeader.transform);
                                        
                                        spawnedAttackTroops[tIndex].
                                                GetComponent<TroopTCode>().
                                                                setTroopCode(
                                                                    tMemCodes[troopCodeOffset] +
                                                                                    troopCodeOffset 
                                                                    );
                                    }
                                }
                                troopsFollowingLeader = true; 
                                waveOfAttackTroopsSpawned = true;
                                if (troopCodeOffset >= tMemCodes.Count) troopCodeOffset = 0;

                                troopCodeOffset++;                               
                            }
                        }
                    }
                }
        }        
     }
    
   IEnumerator systemDelay(float delay)
   {
       Debug.Log("Waiting for signal...");
       yield return new WaitForSeconds(delay);
       Debug.Log("Building a barrack...");
   }

  public void buildBarracks()
  {
        var barracksCreditPrice = eb.getBaseStructureList().getBuildingPrice(barracksBuildingCode);
        var barracksBuilding = eb.getBaseStructureList().getBuilding(barracksBuildingCode);
        var barracksEnergonPrice = eb.getBaseStructureList().getEnergonPrice(barracksBuildingCode);

        if (structIndex >= buildingPoints.Count ) { return; }
        
        bool hasEnoughResources = false;

        
        hasEnoughResources = am.buy(barracksName, barracksCreditPrice, barracksEnergonPrice);

        if (!requiredBarracksAmount(requiredAmount) && hasEnoughResources)
        {       
             generateRandomNumber();
            if (wm.areThereAnySpawnedFreeWorkers())
            {
                 wm.assignExistingWorkerToTask(true, "barrack", buildingPoints[randomStructureSpawnPosition].position,  barracksBuilding); 
            }     
            else
            {
                //If worker count has reached max, then return and do nothing else..
                if (wm.workerCountReachedMax()) return;
                wm.spawnWorker(true, "barracks", buildingPoints[randomStructureSpawnPosition].position, barracksBuilding);   
                
                isPurchasing = false;
            }
            structIndex++;
        }
        else
        {
            Debug.Log("Error can't spawn barracks!");
        } 

    }
    public void receiveSignalForRespawn(bool state)
    {
        receivedSignal = state;  
        waveOfAttackTroopsSpawned = false;
        isTeamLeaderFound = false;
        troopsFollowingLeader = false;
        new EnemyTagMgr().locateEnemyObjectWithTag(eBaseTag.getEnemyMBaseTag(), "EBBarracks").GetComponent<SpawnTroopUnit>().resetAttackTroopMaxCount();     
    }
    public bool SearchingForDepo()
    {
        return isNeedingSearchParty;
    }
    public bool EnoughPatrolUnits()
    {
        return enoughTroopsForPatrol;
    }
    public bool EnoughAttackUnits()
    {
        return enoughTroopsForAttack;
    }
    public bool enoughForSearchParty()
    {
        return enoughTroopsForSearch;
    }
    public bool teamLeaderFound()
    {
        return isTeamLeaderFound;
    }
    //Combine into one<-------------------------
    private bool hasEnoughTroopsFor(int maxUnits, bool forAttack, bool forPatrol, bool forSearch)
    {
        var enemyTagMgr = new EnemyTagMgr();
        //if (!enemyTagMgr.enemyObjectExists(eBaseTag.getEnemyMBaseTag(), "EBBarracks")) return false;

        var _barracks = enemyTagMgr.locateEnemyObjectWithTag(eBaseTag.getEnemyMBaseTag(), "EBBarracks");
        var barracks = _barracks.GetComponent<SpawnTroopUnit>();
        /*var _barracks = enemyTagMgr.locateEnemyObjectWithTag(eBaseTag.getEnemyMBaseTag(), "EBBarracks");
        if (_barracks == null) return false;
        
        var barracks = _barracks.GetComponent<SpawnTroopUnit>();*/
        

        if (forAttack)
            return barracks.getAttackTroopCount() >= maxUnits;
        else if (forPatrol)
            return barracks.getPatrollUnitCount() >= maxUnits;
        else if (forSearch)
            return barracks.getTroopSearchCount() >= maxUnits;
        else
           return false;
    }
    // private bool enoughSearchDepoUnits(int maxUnits)
    // {
    //     return FindObjectOfType<SpawnTroopUnit>().getTroopSearchCount() >= maxUnits;
    // }
    // private bool enoughForAttack(int maxUnits)
    // {
    //     return FindObjectOfType<SpawnTroopUnit>().getAttackTroopCount() >= maxUnits;
    // }
    // private bool hasEnoughUnitsForPatrol(int maxUnits)
    // {
    //     return FindObjectOfType<SpawnTroopUnit>().getPatrollUnitCount() >= maxUnits;
    // }
   
    public void generateRandomNumber()
    {
       randomStructureSpawnPosition = Random.Range(0, buildingPoints.Count);
       while (randomStructureSpawnPosition == prieviousRandom)
       {
          randomStructureSpawnPosition = Random.Range(0, buildingPoints.Count);
       }
       prieviousRandom = randomStructureSpawnPosition;
    }

    public bool hasEnoughFunds()
    {
        bool enoughForBarrack = false, enoughForWorker = false;

        for (int rIndex = 0; rIndex < am.getCreditReserves().Count; rIndex++)
        {
            if (am.getCreditReserves()[rIndex].getCreditReserveCap() >= barracksPrice &&
             am.getCreditReserves()[rIndex].getReserveName() == barracksName)
             {
                 enoughForBarrack = true;
             }
        }
        
        for (int rIndex = 0; rIndex < am.getCreditReserves().Count; rIndex++)
        {
            if (am.getCreditReserves()[rIndex].getCreditReserveCap() >= wm.getWorkerUnitPrice()  &&
             am.getCreditReserves()[rIndex].getReserveName() == "Worker")
             {
                 enoughForWorker = true;
             }
        }

        return enoughForBarrack == enoughForWorker;
    }
    //Upgrade
    // public bool hasEnoughFunds()
    // {
    //      bool enoughForBarrack = false, enoughForWorker = false;

    // }


   //Function for checking if the ENemy AI has atleast one barracks
   public void setBasePatrollState(bool state)
   {
       isNeedingPatrolUnits = state;
   }
   public bool NeedingPatrolUnits()
   {
       return isNeedingPatrolUnits;
   }
   public bool NeedingForAttack()
   {
       return isNeedingForAttack;
   }
   public bool requiredBarracksAmount(int rAmount)
   {   
       var enemyTagMgr = new EnemyTagMgr();
       if (!enemyTagMgr.enemyObjectsExist("EBBarracks")) return false;
       var barracksSize = new EnemyTagMgr().locateEnemyObjectsWithTag(eBaseTag.getEnemyMBaseTag(), "EBBarracks").Length;
       return barracksSize >= rAmount;
   }
    
    public int getBarracksEnergonPrice()
    {
        return barracksEnergonPrice;
    }
    
    public bool Purchasing()
    {
        return isPurchasing;
    }
   public int getBarracksPrice()
   {
       return barracksPrice;
   }

   public int getLightTroopUnitPrice()
   {
       return lightTroopUnitPrice;
   }
   
   public void AddPatrolPoint(Vector3 point)
   {
       patrolPoints.Add(point);
   }
   public List<Vector3> getPatrolPoints()
   {
       return patrolPoints;
   }

   public List<Vector3> getTravelPointsToEnemtyBase()
   {
       return travelPointsToEnemyBase;
   }
   public void addTravelPointToBase(Vector3 point)
   {
       travelPointsToEnemyBase.Add(point);
   }
   public List<Vector3> getAttackPathPoints()
   {
       return travelPointsToEnemyBase;
   }
}

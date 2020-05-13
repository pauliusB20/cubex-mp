using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIWorker : MonoBehaviour
{    
   //Configuration parameters
   [Header("Configuration parameters")]
   [SerializeField] int scoreAmountForBuildings = 10;
   [SerializeField] int energon;
   [SerializeField] float collectorBuildDelay = 5f;
   [SerializeField] float barrackBuildDelay = 5f;
   [SerializeField] float researchCenterBuildDelay = 8f;
   [SerializeField] float defenceStructureBuild = 7f;
   [SerializeField] bool isAtTheBuildingSite = false;
   [SerializeField] string workingStatus = "";
   [SerializeField] Transform restingArea;
   // [SerializeField] bool isAssigned;
    //Cashed data
  //  private bool hasStartedWorking; 
    private Vector3 miningSTSpawnPosition, aiBasePosition; 
    UnityEngine.AI.NavMeshAgent nav;
   //For debug
   [SerializeField] private Vector3 destinationPoint;
   private string buildSiteTag;
   private bool isCollectorDestroyed;
   private bool spawnBarracks;
   private bool isAssigned = false;
   private GameObject buildingToSpawn;
   private GameSession gs;
   private GameObject enemyBase;
   private EAIWorkerTaskTimer workerTimer;
   private GameObject buildingSite;
  // private GameObject enemyBase; 
   private EnemyTag enemyTag;
    void Start()
    {
       
        gs = FindObjectOfType<GameSession>();
      //  isAssigned = false;
        energon = 0;
        isCollectorDestroyed = false;
        spawnBarracks = false;
     //   hasStartedWorking = true;
        enemyTag = GetComponent<EnemyTag>();
        
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        workerTimer = GetComponent<EAIWorkerTaskTimer>();

         enemyBase = new EnemyTagMgr().getCurrentEnemyBaseByTag(enemyTag.getEnemyTag(), "EnemyBase");
        nav.Warp(transform.position);
    }
    void Update()
    {
        if (enemyBase == null) return;
        if (isAtTheBuildingSite)
        {
            var restingArea = enemyBase.GetComponent<WorkerAIManager>().getRestingArea();
            switch(buildSiteTag)
            {
                case "Depo":                
                if (workerTimer.timerDone())
                {
                    isAssigned = true;
                    gs.addEnemyCollectorAmount(1);
                    gs.AddEnemyScorePoints(scoreAmountForBuildings);   
                    if (buildingSite == null) return;

                    GameObject collector = Instantiate(buildingToSpawn, buildingSite.transform.position, Quaternion.identity);
                    nav.Warp(transform.position);
                    isAtTheBuildingSite = false; 
                    workingStatus = buildSiteTag; 
                    buildSiteTag = "none"; 
                    collector.GetComponent<EnemyTag>().setEnemyTag(enemyTag.getEnemyTag());
                    Destroy(buildingSite);
                    nav.Warp(transform.position);
                    isAtTheBuildingSite = false; 
                    // enemyBase.GetComponent<AIResourcesManagement>().increaseUsedDepositCount();
                    enemyBase.GetComponent<AIResourcesManagement>().increaseCollectorAmount();
                    enemyBase.GetComponent<EnemyAIBase>().decreaseDepoLength();
                }
                break;

                case "barracks":                
                if (workerTimer.timerDone())
                {
                    isAssigned = false;
                    Debug.Log("Barracks built..");
                    gs.addEnemyBarrackAmount(1);
                    gs.AddEnemyScorePoints(scoreAmountForBuildings);   
                    GameObject barrack = Instantiate(buildingToSpawn, buildingSite.transform.position, Quaternion.identity);
                    barrack.GetComponent<EnemyTag>().setEnemyTag(enemyTag.getEnemyTag());
                    isAtTheBuildingSite = false;  
                    Destroy(buildingSite);  
                    nav.Warp(transform.position);
                    buildSiteTag = "none"; 
                    setDestinationPoint(restingArea.position);                
                }
                break;

                case "RMCenter":                
                if (workerTimer.timerDone())
                {
                    gs.addEnemyResearchAmount(1);
                    gs.AddEnemyScorePoints(scoreAmountForBuildings);                   
                    GameObject researchCenter = Instantiate(buildingToSpawn, buildingSite.transform.position, Quaternion.identity);
                    researchCenter.GetComponent<EnemyTag>().setEnemyTag(enemyTag.getEnemyTag());
                    isAtTheBuildingSite = false;
                    isAssigned = false;
                    Destroy(buildingSite);
                    nav.Warp(transform.position);
                    buildSiteTag = "none";
                    setDestinationPoint(restingArea.position); 
                }
                break;

                case "EMSite":                
                if (workerTimer.timerDone())
                {
                    gs.addEnemyTurretAmount(1);
                    gs.AddEnemyScorePoints(scoreAmountForBuildings);                   
                    GameObject defence = Instantiate(buildingToSpawn, buildingSite.transform.position, Quaternion.identity);
                    defence.GetComponent<EnemyTag>().setEnemyTag(enemyTag.getEnemyTag());
                    isAtTheBuildingSite = false;
                    isAssigned = false;
                    Destroy(buildingSite);
                    nav.Warp(transform.position);
                    buildSiteTag = "none";
                    setDestinationPoint(restingArea.position); 
                }
                break;
            }
        }
        move();

    }
    public void move()
    {    
        //Unit movement    
        if (destinationPoint != null && nav != null)
        {
            nav.SetDestination(destinationPoint); 
        }
       
    }
    public void setWorkerRestingArea(Transform t)
    {
        restingArea = t;
    }
    public void goToRestArea()
    {
        destinationPoint = restingArea.position;
    }
    public string getWorkingStatus()
    {
        return workingStatus;
    }
    public void giveABuildingToSpawn(GameObject building)
    {
        this.buildingToSpawn = building;
    }
    public void setDestinationPoint(Vector3 pos)
    {
        destinationPoint = pos;
    }
    public void setWorkingStatus(string status)
    {
        workingStatus = status;
    }
    private void OnTriggerEnter(Collider other)
    {
        //When worker find a deposit, spawns a collector
        var iCanBuild = (other.gameObject.tag == buildSiteTag && buildSiteTag != null);

        if (iCanBuild)
        {
            switch (buildSiteTag)
            {
               case "Depo":
                // gs.addEnemyCollectorAmount(1);
                // gs.AddEnemyScorePoints(scoreAmountForBuildings);
                workerTimer.startTimer(collectorBuildDelay);
                buildingSite = other.gameObject;
               // Destroy(other.gameObject);
                isAtTheBuildingSite = true;
                
                // Instantiate(buildingToSpawn, other.transform.position, Quaternion.identity);
               break;

               case "barracks":
                // gs.addEnemyBarrackAmount(1);
                // gs.AddEnemyScorePoints(scoreAmountForBuildings);
               // Destroy(other.gameObject);
                // Instantiate(buildingToSpawn, other.transform.position, Quaternion.identity);
                // Debug.Log("Barracks built..");
                // isAssigned = false;
                workerTimer.startTimer(barrackBuildDelay);
                isAtTheBuildingSite = true;
                buildingSite = other.gameObject;
               // buildSiteTag = "none";
               break;

               case "RMCenter":
                //  gs.addEnemyResearchAmount(1);
                //  gs.AddEnemyScorePoints(scoreAmountForBuildings);
                //Destroy(other.gameObject);
                //  Instantiate(buildingToSpawn, other.transform.position, Quaternion.identity);
                //  Debug.Log("MRCenter built..");
                //  isAssigned = false;
                workerTimer.startTimer(researchCenterBuildDelay);
                isAtTheBuildingSite = true;
                buildingSite = other.gameObject;
               // buildSiteTag = "none";
               break;

               case "EMSite":
                // gs.addEnemyTurretAmount(1);
                // gs.AddEnemyScorePoints(scoreAmountForBuildings);
                //Destroy(other.gameObject);
                // Instantiate(buildingToSpawn, other.transform.position, Quaternion.identity);
                // Debug.Log("Defence structure built..");
                // isAssigned = false;
                //nav.isStopped = true;
                workerTimer.startTimer(defenceStructureBuild);
                isAtTheBuildingSite = true;
                buildingSite = other.gameObject;
               // buildSiteTag = "none";
               break;

               case "EWRA":
                isAssigned = false;
                nav.isStopped = true;
               break;

            }
        }
    }
    //Method for assigning the worker to a specific task..
    public void AssignTaskState(bool state)
    {
        var nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (nav.isStopped) 
            nav.isStopped = false;

        this.isAssigned = state;
    }
    public bool AssignedToTask()
    {
        return isAssigned;
    }
    public Vector3 getDestinationPoint()
    {
        return destinationPoint;
    }
    public void setMiningPosition(Vector3 pos)
    {
        miningSTSpawnPosition = pos;
    }
    public Vector3 getMiningPosition()
    {
       return miningSTSpawnPosition;
    }
    public int getEnergon()
    {
        return energon;
    }
    public void takeSetEnergon(int e)
    {
        energon = e;
    }
    public void setCollectorDestructionState(bool state)
    {
        isCollectorDestroyed = state;
    }
    public bool CollectorDestroyed()
    {
        return isCollectorDestroyed;
    }
    public bool canSpawnBarracks()
    {
        return spawnBarracks;
    }
    public void setCanSpawnBarracks(bool state)
    {
        spawnBarracks = state;
    }
    public void setBuildSiteTag(string tag)
    {
        buildSiteTag = tag;
    }
    public string getBuildSiteTag()
    {
        return buildSiteTag;
    }
}

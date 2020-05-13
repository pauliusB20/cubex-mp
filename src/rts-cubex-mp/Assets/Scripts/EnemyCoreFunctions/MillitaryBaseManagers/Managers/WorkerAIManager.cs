using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAIManager : MonoBehaviour
{
   //Fix buy system AI
    //Inspector objects
    [Header("Worker configuration parmaeters")]
    [SerializeField] float startingWorkerSpawningTime = 2f;
    [SerializeField] GameObject aiWorker;
    [SerializeField] string aiWorkerName;
    [SerializeField] Transform[] workerSpawnPoint;
    [SerializeField] int destinationPointIndex;
    [SerializeField] float delayTillNextSpawn = 2f;
    [SerializeField] Transform restingArea;
    [SerializeField] int workerCount = 0;
    [SerializeField] int maxWorkerAmount = 3;
     //[SerializeField] float delayBeforeSpawn = 2f;
    [Header("Unit price")]
    [SerializeField] int workerUnitPrice = 20;
    AITroopManager atm;
    //Cached references
    GameSession gs;
    EnemyAIBase eb;
    AIResourcesManagement am;
    List<WorkerUnit> workers;
    int depositIndex;
    [SerializeField] bool canSpawnWorker = true;
    bool startCountdownTillNextSpawn = false;
    private EnemyBaseTag eBaseTag;

    [Header("Timer configuration for worker spawning")]
    [SerializeField] float timeStart = 0;
    [SerializeField] float startingTime = 0;  
    void Start()
    {  
        //Setting initial values
        //Getting components   
        gs = FindObjectOfType<GameSession>();
        eb = GetComponent<EnemyAIBase>();
        am = GetComponent<AIResourcesManagement>();
        atm = GetComponent<AITroopManager>();
        eBaseTag = GetComponent<EnemyBaseTag>();

        workers = new List<WorkerUnit>();
        destinationPointIndex = 0;
        depositIndex = 0;
        if (workerUnitPrice <= 0)
        {
            UnityEngine.Debug.LogError("Prices for units are not defined");
        }
        Debug.Log(gameObject.name + " : " + eBaseTag.getEnemyMBaseTag());
    }
    
    private void Update() {
        if ((!canSpawnWorker && startCountdownTillNextSpawn) && timeStart > 0)
        {
               //Main timer countdown processs
           
                //The system tryes to subract from current starting timer value...
                timeStart -= Time.deltaTime;
                //When it subracts, then the system rounds up the value when it will try to outputthe current time's values.

                //Checks if the timeStart value reached 0, when true then the timer button becomes interactable, a message is printed, which indicates 
                //that the time coundown is done and resets the current timeStart variable's value and text. As well, it returns nothing, so it could cancel
                //the system for continuing any further.. 
                if (timeStart <= 0)
                {
                    
                    //Sets the current coundown signal the false, which would say that the system can not run and that it can run once the user clicked on the button.
                    startCountdownTillNextSpawn = false;
                    canSpawnWorker = true;
                    timeStart = startingTime;
                    return;
                }
        
        }
    }
    public void spawnWorker(bool state, string taskName, Vector3 taskPos, GameObject building)
    {
        
        //delay between spawn...
        var randomNumber = new RandomNumberGenerator();
        bool enoughCredits = false;
       //Worker code goes here... 
      
       enoughCredits = am.buy(aiWorkerName, workerUnitPrice);
       if (enoughCredits)
       {
         GameObject spawnedWorker = Instantiate(aiWorker, workerSpawnPoint[0 + randomNumber.generateRandomNumber(0, workerSpawnPoint.Length)].position, Quaternion.identity) as GameObject;
         spawnedWorker.GetComponent<EnemyTag>().setEnemyTag(eBaseTag.getEnemyMBaseTag());
         var worker = spawnedWorker.GetComponent<EnemyAIWorker>();
                //For checking if the current unit price is 0 because units can not have price which is 0
         timeStart = startingTime;
         canSpawnWorker = false;
         startCountdownTillNextSpawn = true;
         gs.addEnemyWorkerAmount();
         if (spawnedWorker != null)   
         {         
            Debug.Log("Succesfully spawned a base worker...");
           //If worker is spawnned adding it to a list
            workers.Add(new WorkerUnit("MBase worker", spawnedWorker, "m", workerUnitPrice));
            // assignExistingWorkerToTask(state, taskName, taskPos, building);
            worker.AssignTaskState(state);
            worker.setBuildSiteTag(taskName);
            worker.giveABuildingToSpawn(building);
            worker.setDestinationPoint(taskPos);
            workerCount++;
         }
            
       }
       else 
       {
           Debug.Log("Can't spawn! insuficient credits!");
       }
        
    }
   
    public void assignExistingWorkerToTask(bool state, string taskName, Vector3 taskPos, GameObject building)
    {
      var enemyTagMgr = new EnemyTagMgr();
       
       if (!enemyTagMgr.enemyObjectsWithTagExist(eBaseTag.getEnemyMBaseTag(), "EBWorker"))  return;

       
       var workersSpawned = enemyTagMgr.locateEnemyObjectsWithTag(eBaseTag.getEnemyMBaseTag(), "EBWorker");
       if (workersSpawned != null)
       {
       // var workersSpawned = FindObjectsOfType<EnemyAIWorker>();

        // for (int workerIndex = 0; workerIndex < workersSpawned.Length; workerIndex++)
        // {
        //     if (!workersSpawned[workerIndex].AssignedToTask())
        //     {
        //         workersSpawned[workerIndex].AssignTaskState(state);
        //         workersSpawned[workerIndex].setBuildSiteTag(taskName);
        //         workersSpawned[workerIndex].giveABuildingToSpawn(building);
        //         workersSpawned[workerIndex].setDestinationPoint(taskPos);                
        //         break;
        //     }
        // }

         for (int workerIndex = 0; workerIndex < workersSpawned.Length; workerIndex++)
        {
            if (!workersSpawned[workerIndex].GetComponent<EnemyAIWorker>().AssignedToTask())
            {
                var worker = workersSpawned[workerIndex].GetComponent<EnemyAIWorker>();
                worker.AssignTaskState(state);
                worker.setBuildSiteTag(taskName);
                worker.giveABuildingToSpawn(building);
                worker.setDestinationPoint(taskPos);                
                break;
            }
        }
       }
       else
       {
           return;
       }
    }
    //Fix this!
     public bool areThereAnySpawnedFreeWorkers()
    {
        var enemyTagMgr = new EnemyTagMgr();
       // var workersSpawned = FindObjectsOfType<EnemyAIWorker>();
      if (!enemyTagMgr.enemyObjectsWithTagExist(eBaseTag.getEnemyMBaseTag(), "EBWorker")) return false;

        var workersSpawned = enemyTagMgr.locateEnemyObjectsWithTag(eBaseTag.getEnemyMBaseTag(), "EBWorker");
        if (workersSpawned != null)
        {
            Debug.Log(gameObject.name + " : worker size: " + workersSpawned.Length);
            int workerAmount = 0;
            for (int workerIndex = 0; workerIndex < workersSpawned.Length; workerIndex++)
            {
                if (workersSpawned[workerIndex].GetComponent<EnemyTag>().getEnemyTag() == eBaseTag.getEnemyMBaseTag())
                {
                    if (!workersSpawned[workerIndex].GetComponent<EnemyAIWorker>().AssignedToTask())
                    {
                        workerAmount++;
                    }
                }
            }
            return workerAmount > 0;
        }
        else
        {
            return false;
        }
       
    }
    public bool areThereAnySpawnedWorkers()
    {   
        var enemytagMgr = new EnemyTagMgr();
        if (!enemytagMgr.enemyObjectsExist("EBWorker")) return false;
        return enemytagMgr.locateEnemyObjectsWithTag(eBaseTag.getEnemyMBaseTag(), "EBWorker").Length > 0;
    }
   
   public bool spawAgain()
   {
       return canSpawnWorker;
   }
   public void setDepositIndex(int i)
   {
       depositIndex = i;
   }
   public int getWorkerUnitPrice()
   {
       return workerUnitPrice;
   }
   public List<WorkerUnit> getWorkerList()
   {
       return workers;
   }
   public void setRestingArea(Transform t)
   {
       restingArea = t;
   }
   public Transform getRestingArea()
   {
       return restingArea;
   }
   //Method for checking if worker reached max..
   public bool workerCountReachedMax()
   {
       return workerCount >= maxWorkerAmount;
   }
   public void setWorkerMaxCount(int max)
   {
       maxWorkerAmount = max;
   }
}

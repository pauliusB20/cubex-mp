using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAIBase : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Base configuration parameters")]
    [SerializeField] GameObject objectScanner, objScannerGun;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] bool areDepositsFound = false;
    [SerializeField] private bool depositsNotInArea;
    [SerializeField] float coolDownDelay = 2f;
    [SerializeField] bool startSpawningWorkers;
    [SerializeField] bool scannerStopped = false;
    [SerializeField] MBaseStructures eMBstructures;
    /*[Header("Worker configuration parmaeters")]
    [SerializeField] GameObject worker;
    [SerializeField] GameObject baseUnitSpawnPoint;*/

    [Header("Deposit point collection parameters")]
    [SerializeField] int depositLength = 0;
    [SerializeField] float maxDistanceToDeposit = 0f;
    //Cached references
    List<DepositPoint> depositPoints;

    private bool canNotSort;
    private bool sortTheList = true;
    private HealthEnemyAI enemyHealth;
    private AITroopManager troopManager;
    
    void Start()
    {
    
       troopManager = GetComponent<AITroopManager>();
       enemyHealth = GetComponent<HealthEnemyAI>();
       depositPoints = new List<DepositPoint>();
       canNotSort = false;
       startSpawningWorkers = false;
       depositsNotInArea = false;

    }
    void Update()
    {   
      //Handle Enemy death
       if (enemyHealth.getHealth() <= 0)
       {
          FindObjectOfType<GameSession>().increaseDestroyedEnemyBaseAmount();
          Destroy(gameObject);
       }
      
      //Checking distance of the deposits
      if((!depositsNotInArea && depositLength > 0) && !areDepositsFound)
      {
         //Sort deposit points
         sortDepositPointList();
         //Turn and try to add them into list   
         turnAndCountDeposits();  
         return;
      }  
      //Checking if the deposits are in the build area...
      if (depositLength > 0)
       {
         areDepositsFound = true;
         depositsNotInArea = false;
       }
       if ((depositLength > 0 && areDepositsFound) && !sortTheList)
       {
            sortDepositPointList();
            sortTheList = false;
       }
       
       //If deposits are not in the build area, then turns on the signal for the search process....  
       if (depositLength <= 0 && !areDepositsFound)
       { 
        depositsNotInArea = true;  
       }
      
      
    }
   
   
 
  public void turnAndCountDeposits()
   {
        //If first deposit is found, then do nothing
       if (areDepositsFound) { 
          Debug.Log("Success all deposits are found!");                   
           return;
       }
        objectScanner.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        scannForEnergonDeposits();
   }
 
   void scannForEnergonDeposits()
   {
     // depositPoints = new List<DepositPoint>(Random.Range(1, depositLength) + 2);

      if (depositPoints.Count >= depositLength && depositPoints.Count > 0)
      {
          areDepositsFound = true;   
          scannerStopped = true;         
          UnityEngine.Debug.Log("Deposit location list size: " + depositPoints.Count);
          return;
      }
        RaycastHit hit;
        //Ray scanner size:
        var buildAreaSizeHalf = FindObjectOfType<BuildAreaEnemyAI>().getBuildAreaSizeOnAxis().z / 2;
       // Debug.Log("Current ray size is: " + buildAreaSizeHalf);

        //If deposit is found, then it will be added to a list
        if (Physics.Raycast(objScannerGun.transform.position, objScannerGun.transform.forward, out hit, buildAreaSizeHalf, 1 << LayerMask.NameToLayer("Energon")))
        {      
          if ((hit.transform.gameObject.layer == LayerMask.NameToLayer("Energon")) )
           {         
              //Debug.DrawRay(objScannerGun.transform.position, objScannerGun.transform.forward, Color.green);        
              var depoPoint = new DepositPoint(Mathf.Round(hit.distance), hit.point,
               /*For debuging purposes -> */hit.transform.gameObject.name, EnemySystemConstants.TAKEN_POINT);
              depositPoints.Add(depoPoint);
           }
        }
   }
   public void decreaseDepoLength()
   {
      if (depositLength > 0)
         depositLength--;
      else
         return;
   }
   public int getDepositLength()
   {
      return depositLength;
   }
   public List<DepositPoint> getDepositPoints()
   {
      return depositPoints;
   }

   //Find the maximum distance to the near by energon deposit
   //------------------>Function WORKS<-----------------------
   public void sortDepositPointList()
    {
       
       if (areDepositsFound == false || canNotSort == true) { return; }
       StartCoroutine(countdownTilSort());
       startSpawningWorkers = true;
       canNotSort = true;
       
    }
    IEnumerator countdownTilSort()
    {
       Debug.Log("Sorting...");
       yield return new WaitForSeconds(coolDownDelay);       
       var sort = new ListSort();
      //FIX: using heap sort instead of quick sort because heap sorting is more stable in our game
       sort.heapSort(depositPoints); 
       Debug.Log("Sort finished!");  
       printDepositPositions();     
       startSpawningWorkers = true;
       
       
    }

    private void printDepositPositions()
    {
       for (int i = 0; i < depositPoints.Count; i++)
       {
          UnityEngine.Debug.Log(depositPoints[i].toString());
       }
    }
    public void setSpeedScanner(float speed)
    {
       this.rotationSpeed = speed;
    }
    public float getMaxDistance()
    {
       return  maxDistanceToDeposit;
    }
    public Vector3 getBasePosition()
    {
       return transform.position;
    }
    public void setDepoState(bool state)
    {
       areDepositsFound = state;
    }
   public bool getDepositFoundState()
   {
      return areDepositsFound;
   }
   public bool canStartSpawningWorkers()
   {
      return startSpawningWorkers;
   }
   public void addDepositCount(int point)
   {
      depositLength += point;
      //Debug.Log(depositLength);
   }
   public void addEnergonDepositToList(Vector3 pos, float distance)
   {
      if (pos == null || distance == 0)
         return;

      

      var depoPoint = new DepositPoint(distance, pos, "E:" + depositLength, EnemySystemConstants.TAKEN_POINT);
      depositPoints.Add(depoPoint);

      if (depositPoints.Count > 0)
      {
         Debug.Log("Added a new founded deposit point. Size list: " + depositPoints.Count);
         // areAllDepositsFound = true;
      }
      depositLength++;  
   }

   public bool areDepositsNotInArea()
   {
      return depositsNotInArea;
   }
   public void setMBuildStruc(MBaseStructures mb)
   {
      eMBstructures = mb;
   }
   public void setBaseScanner(GameObject scanner)
   {
      objectScanner = scanner;
   }
   public void setBaseScannerPos(GameObject scannerPos)
   {
      objScannerGun = scannerPos;
   }
   public MBaseStructures getBaseStructureList()
   {
      return eMBstructures;
   }
   
   
}

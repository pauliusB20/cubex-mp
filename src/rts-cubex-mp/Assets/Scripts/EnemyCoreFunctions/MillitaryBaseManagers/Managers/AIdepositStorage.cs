using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIdepositStorage : MonoBehaviour
{
     //Storage system:
     //It checks for any incoming worker units and how many energon resources did a worker unit bring
     EnemyAIBase eb;
     AIResourcesManagement am;
     WorkerAIManager wm;
     bool moveNextDepositPosition;
     [SerializeField] int depositIndex;
     [SerializeField] bool isCollectorDestroyed = false;
     [SerializeField] Transform restingArea;
    //  [SerializeField] List<_CreditRezerve> creditRezerves = new List<_CreditRezerve>();
    //  [SerializeField] List<EnergonRezerve> energonRezerves = new List<EnergonRezerve>();
     GameObject enemyBase;
     private void Start() {
       enemyBase = transform.parent.transform.gameObject;
       eb = enemyBase.GetComponent<EnemyAIBase>();
       am = enemyBase.GetComponent<AIResourcesManagement>();
        wm = enemyBase.GetComponent<WorkerAIManager>();
       moveNextDepositPosition = false;
       wm.setRestingArea(restingArea);
       depositIndex = 0;
     }
     
     private void Update() {
       if (moveNextDepositPosition)
       {
         // depositIndex++; 
        //  wm.setDepositIndex(depositIndex);          
          moveNextDepositPosition = false;
          return;
       }
       else
       {
           return;
       }
     }
    private void OnTriggerEnter(Collider otherObject)
    {
      var enteredWorker = otherObject.gameObject.GetComponent<EnemyAIWorker>();
      // Debug.Log("Worker status:" + enteredWorker.getWorkingStatus());
      // if ((otherObject.gameObject.tag == "EBWorker" && enteredWorker.getBuildSiteTag() == "none") 
      //       && (enteredWorker.getEnergon() <= 0 && enteredWorker.getWorkingStatus() == "none"))
      // {
      //    var workerNav = otherObject.gameObject.GetComponent<EnemyAIWorker>();        
      //    workerNav.setCollectorDestructionState(false);             
      //    workerNav.setDestinationPoint(transform.position);  
      //    workerNav.AssignTaskState(false);  
      //    am.decreaseCollectorAmount();     
      //    moveNextDepositPosition = true;
      //    workerNav.setWorkingStatus("none");
      //    workerNav.setDestinationPoint(restingArea.position);
      //    return;
      // }
      //System for checking if there is a destroyed collector      
    if ((( otherObject.gameObject.tag == "EBWorker" && enteredWorker.CollectorDestroyed() == false)  &&  enteredWorker.getBuildSiteTag() == "none") && enteredWorker.getWorkingStatus() == "Depo")
      {
          var workerNav = otherObject.gameObject.GetComponent<EnemyAIWorker>();
          am.addEnergonToBase(workerNav.getEnergon());
          //UnityEngine.Debug.Log("Stored energon: " + workerNav.getEnergon());
          workerNav.takeSetEnergon(0);

          workerNav.setDestinationPoint(workerNav.getMiningPosition());
      }
     
    }
   public void collectorDestroyedStatus(bool state)
   {
     isCollectorDestroyed = state;
   }
   public Vector3 getDepositStoragePosition()
   {
       return transform.position;
   }
  //  public void addReserveCreditToList(_CreditRezerve cres)
  //  {
  //    creditRezerves.Add(cres);
  //  }
  //  public void addReserveToList(EnergonRezerve eres)
  //  {
  //    energonRezerves.Add(eres);
  //  }
  //  public List<_CreditRezerve> getCreditRezerves()
  //  {
  //    return creditRezerves;
  //  }
  //  public List<EnergonRezerve> getEnergonRezerves()
  //  {
  //    return energonRezerves;
  //  }
}

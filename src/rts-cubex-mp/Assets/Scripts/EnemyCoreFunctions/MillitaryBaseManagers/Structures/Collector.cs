using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{

     [Header("Collector configuration parameters")]
     [SerializeField] int availableEnergon = 360;
     [SerializeField] int energonTakingAmount = 1;

     private GameObject enemyBaseBuilding;     
     EnemyAIBase enemyBase;
     HealthEnemyAI health;
     private EnemyTag enemyTag;

    private void Start() {
        FindObjectOfType<GameSession>().addEnemyCollectorAmount(1);
        enemyTag = transform.parent.transform.gameObject.GetComponent<EnemyTag>();
        enemyBaseBuilding = new EnemyTagMgr().getCurrentEnemyBaseByTag(enemyTag.getEnemyTag(), "EnemyBase");
        enemyBase = GetComponent<EnemyAIBase>();
        health = GetComponent<HealthEnemyAI>();
        
        //Once appears this object in the game it tryes to add it's position to the troop patrol wave list..
       enemyBaseBuilding.GetComponent<AITroopManager>().AddPatrolPoint(transform.position);
    }
    public Vector3 getCollectorPosition()
    {
        return transform.position;
    }
    private void Update() {
      if (health.getHealth() <= 0)
      {
        enemyBase.decreaseDepoLength();
        Destroy(gameObject);
      }
    }
 // when a worker unit arrives, this collector system checks how many energon resources it has and if it has enough, then it gives some to a worker
  private void OnTriggerEnter(Collider otherObject)
  {
      //Needs fixing
      //A system for checking if there is zero left energon. If the deposit has ran out, then it sends a status message to the enemy base, that it needs to
      //to another position 
      
      if (availableEnergon > 0 && otherObject.gameObject.tag == "EBWorker" &&
               otherObject.gameObject.GetComponent<EnemyAIWorker>().getWorkingStatus() == "Depo")
      {
             
          var workerNav = otherObject.gameObject.GetComponent<EnemyAIWorker>();  
        //   workerNav.setWorkState(true);                  
          availableEnergon -= energonTakingAmount;          
          workerNav.takeSetEnergon(energonTakingAmount);    
          workerNav.setMiningPosition(transform.position);
          //workerNav.setDestinationPoint(FindObjectOfType<AIdepositStorage>().getDepositStoragePosition());
          workerNav.setDestinationPoint(new EnemyTagMgr().getCurrentBasePositionAtTag(enemyTag.getEnemyTag(), "EnemyBase") );
      }
      else if ((availableEnergon <= 0 && otherObject.gameObject.tag == "EBWorker") &&
               otherObject.gameObject.GetComponent<EnemyAIWorker>().getWorkingStatus() == "Depo")
      {
        var workerNav = otherObject.gameObject.GetComponent<EnemyAIWorker>();  
        workerNav.setBuildSiteTag("none");
        workerNav.setWorkingStatus("none");
        workerNav.takeSetEnergon(0);
        workerNav.setCollectorDestructionState(true);  
        workerNav.setDestinationPoint(FindObjectOfType<AIdepositStorage>().getDepositStoragePosition());
        workerNav.setDestinationPoint(new EnemyTagMgr().getCurrentBasePositionAtTag(enemyTag.getEnemyTag(), "EnemyBase") );
      //  FindObjectOfType<EnemyAIBase>().decreaseDepoLength();
        new EnemyTagMgr().getCurrentEnemyBaseByTag(enemyTag.getEnemyTag(), "EnemyBase").GetComponent<EnemyAIBase>().decreaseDepoLength();
        new EnemyTagMgr().getCurrentEnemyBaseByTag(enemyTag.getEnemyTag(), "EnemyBase").GetComponent<AIResourcesManagement>().decreaseCollectorAmount();
        workerNav.goToRestArea();
        workerNav.AssignTaskState(false);
        Destroy(gameObject);
        return;
      }
      
  }

 
  
}

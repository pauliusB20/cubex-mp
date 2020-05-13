using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopPatrollAction : MonoBehaviour
{
    [Header("Troop unit configuartion parameters")]
    [SerializeField] float stoppingDistance = 0.8f;
    // [SerializeField] bool isSearchingForDeposit = false;
    [SerializeField] bool patrolMode = false;
    //EnemyAIBase eb;
    // AIwanderer aiWand;
    UnityEngine.AI.NavMeshAgent troopNav;
    private Vector3 currentDestination;   
    private AIwanderer wan;
    private AITBaseSearch baseAttacker;
    private AITroopManager mg;
    private EnemyAIBase eb; 
   // private EnemyAIBase eb;
    int desIndex = 0;
    private EnemyTagMgr tagMgr;
    private EnemyTag enemyTag;
    private GameObject enemyBase;
    void Start()
    {
        // aiWand = GetComponent<AIwanderer>();
        //eb = FindObjectOfType<EnemyAIBase>();
        tagMgr = new EnemyTagMgr();
        troopNav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyTag = GetComponent<EnemyTag>();
        baseAttacker = GetComponent<AITBaseSearch>();
        enemyBase = tagMgr.getCurrentEnemyBaseByTag(enemyTag.getEnemyTag(), "EnemyBase");
        mg = enemyBase.GetComponent<AITroopManager>();
        eb =  enemyBase.GetComponent<EnemyAIBase>();
        wan = GetComponent<AIwanderer>();
        desIndex = 0;
        GoToNextPoint();
    }

    // Update is called once per frame 
    private void Update() {

        if (mg.getPatrolPoints().Count <= 1 && patrolMode) 
        {
            patrolMode = false;
            wan.orderToLookForDepo(true);
        }
        if (patrolMode) {
        
         if (NearPosition())
            move();
        }

        // else
        // {
        //   //Search behaviour code goes here...
        //   //baseAttacker.setAssaultTeamMemberStatus(true);
        // }
    } 

    private void move()
    {
        //TODO: add more positions and when it goes to a last position, goes to the first position...
        if (NearPosition())
         GoToNextPoint();
       
    }
    
    // public void orderToSearchForDeposit(bool state)
    // {
    //     isSearchingForDeposit = state;
    // }
    void GoToNextPoint()
    {
        if (getPatrolSize() <= 0) return;
         
       troopNav.destination = getPatrolPosition(desIndex);
        desIndex = (desIndex + 1) % getPatrolSize(); 
    }
    public void setDestination(Vector3 pos)
    {
        currentDestination = pos;
    }
    //DOES NOT WORK
    //For switching destinations, usse ontriggerenter.....
   /*  bool pathComplete()
     {
         if ( Vector3.Distance(troopNav.destination, troopNav.transform.position) <= troopNav.stoppingDistance)
         {
             if (!troopNav.hasPath || troopNav.velocity.sqrMagnitude <= 1f)
             {
                 return true;
             }
         }
 
         return false;
     }*/
     private bool NearPosition()
     {
        return !troopNav.pathPending && troopNav.remainingDistance <= stoppingDistance;
     }
     public void setPatrolModeState(bool state)
     {
         this.patrolMode = state;
     }
     public bool patrolModeState()
     {
         return patrolMode;
     }
     private Vector3 getPatrolPosition(int pIndex)
     {
        return mg.getPatrolPoints()[pIndex];
     }
     private int getPatrolSize()
     {
        return mg.getPatrolPoints().Count;
     }
}
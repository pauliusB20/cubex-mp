using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIwanderer : MonoBehaviour
{
    //Main configuration parameters
    [Header("Main configuration parameters")]
    [SerializeField] float wanderRadius = 1f;
    [SerializeField] float wanderTimer = 1f;
    [SerializeField] float rayVisibilityTime = 5f;
    [SerializeField] float patrolAreaRadius = 2f;
    [SerializeField] bool goToBase = false;
    [SerializeField] bool isLookingForDeposit = false;
    [SerializeField] int locIndexInChildList = 1;
    [SerializeField] bool patrollingNearTheBase = false;
    //Cached references
    private Transform target;
    private UnityEngine.AI.NavMeshAgent agent;
    private float timer;
    private bool hadArrived;
    private bool isBackInBase = false;
    private Vector3 foundedEnergonPosition;
    private GameObject scanner;
    private ScanArea scanArea;
    private EnemyAIBase eBase;
    private bool addDepoPointToList = false;
    //For storing acurate path points
    [SerializeField] private List <Vector3> pathPoints;

    TroopPatrollAction patrolAction;
    private EnemyTag enemyTag;
    private EnemyTagMgr eTagMgr;
    // Use this for initialization
    void Start () {
        //Initializing variables...
        eTagMgr = new EnemyTagMgr();
        enemyTag = GetComponent<EnemyTag>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
        scanner = gameObject.transform.GetChild(transform.childCount - locIndexInChildList).gameObject;
        scanArea = scanner.GetComponent<ScanArea>();
        eBase = eTagMgr.getCurrentEnemyBaseByTag(enemyTag.getEnemyTag(), "EnemyBase").GetComponent<EnemyAIBase>();
        patrolAction = GetComponent<TroopPatrollAction>();
        hadArrived = false;


        timer = wanderTimer;

        pathPoints.Add(transform.position);
        
    }
 
    // Update is called once per frame
    void Update ()
    {
        //Handle deposit discovery...\
        if (isLookingForDeposit)
        {
            if (!scanArea.DepoFound())
                movement();
            else
             goToDeposit();
        }
        // else if (patrollingNearTheBase)
        // {
           
        //     patrolAction.orderToSearchForDeposit(true);
        // }
        else
        {
            return;
        }
    }
    
    //Set the state for deposit search....
    public void needTolookForDeposit(bool state)
    {
        isLookingForDeposit = state;
    }

    public bool lookingForDeposit()
    {
        return isLookingForDeposit;
    }

    private bool NearPosition()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;       
    }
    private void goToDeposit()
    {
       //Once the system detects a near by deposit, it starts going to it
      if (scanArea.DepoFound())
      {
          
          if (NearPosition() && !addDepoPointToList)
          {              
            pathPoints.Add(scanArea.getDepoPosition());
            foundedEnergonPosition = scanArea.getDepoPosition();
            //If mbase structure exists, send a message to it about this discovered deposit point... 
            if (eBase != null)
            {
                
                    float dis = Vector3.Distance(eBase.getBasePosition(), foundedEnergonPosition);
                    Debug.Log(gameObject.name + ": distance from base to depo: " + dis);
                    eBase.addEnergonDepositToList(foundedEnergonPosition, dis);
                    isLookingForDeposit = false;
                    patrolAction.setPatrolModeState(true);
                    addDepoPointToList = true;
               
            }
            else
            {
               Debug.LogWarning(gameObject.name + ": can't add founded depo position!");
            }   
            //agent.isStopped = true;
            hadArrived = true;
          }          
          agent.SetDestination(scanArea.getDepoPosition());
      }
      else 
      {          
           //Send command to base and from the base starts patrolling..
         //  goToBase = true;
           // handlePatrolArea();
            returnBackToBase();
       
      }

    }
    public void orderToLookForDepo(bool state)
    {
        isLookingForDeposit = state;
        scanArea.orderToSearchNewDepo(true);
    }
    private void handlePatrolArea()
    {
        if (goToBase)
            returnBackToBase();
        else
            patrolTheArea(patrolAreaRadius);
    }
    //Patrol the energon deposit till the base worker arrives
    private void patrolTheArea(float areaRadius)
    {
        agent.isStopped = false;
        wanderRadius = areaRadius;
        movement();
    }
    //Method for returning to the base
    private void returnBackToBase()
    {
        if ((hadArrived && pathPoints.Count > 0) && !isBackInBase)
        {
            agent.SetDestination(pathPoints[0]);

            if (NearPosition())
            {

                isBackInBase = true;
                patrolAction.setPatrolModeState(true);
            }
        }
        else
        {
            // agent.isStopped = true;
            isLookingForDeposit = false;
            patrolAction.setPatrolModeState(true);
        }
    }
    
    private void movement()
    {
        timer += Time.deltaTime;
 
        if (timer >= wanderTimer) {
            
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius);
            //For debugging
                Debug.DrawRay(newPos, Vector3.up, Color.blue, rayVisibilityTime);

            //If it is close to the target
            if (NearPosition())
            {
                newPos = RandomNavSphere(transform.position, wanderRadius);
            }           

            agent.SetDestination(newPos);
            timer = 0;
        }
    }
    //Generating random position on the navmesh
    public Vector3 RandomNavSphere(Vector3 origin, float dist) {
        //Notes:
        //Random can give an invalid point
        //Radius must change once appraching goal
        //Once troop arrives near energon depo, starts patrolling near the position
        //remember path to the base
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        UnityEngine.AI.NavMeshHit navHit;
 
        UnityEngine.AI.NavMesh.SamplePosition (randDirection, out navHit, dist, UnityEngine.AI.NavMesh.AllAreas);

        return navHit.position;
    }
    
}

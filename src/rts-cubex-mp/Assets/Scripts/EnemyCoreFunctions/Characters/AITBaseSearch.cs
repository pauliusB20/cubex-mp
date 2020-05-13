using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITBaseSearch : MonoBehaviour
{

    //Create:
    //Leader system, which is driven by random choice..
    //Path finding algorithm...
    [SerializeField] string enemyTag;
    [SerializeField] bool isLeader = false;
    [SerializeField] bool findingPlayerBaseMode = false;
    [SerializeField] bool isAssaultTeamMember = false;
    [SerializeField] float wanderRadius = 1f;
    [SerializeField] float wanderTimer = 1f;
    [SerializeField] float rayVisibilityTime = 5f;
    [SerializeField] float patrolAreaRadius = 2f;
    [SerializeField] bool isSearchForPlayerBase = false;
    //Points for remembering the path to the player's base
    // [SerializeField] bool isFollowingLeader = false;
    private UnityEngine.AI.NavMeshAgent aNav;
    private Vector3 currentDestination;
    private GameObject militaryBase;
    private AITroopManager tmanager;
    //private AITroopManager troopManager;
    private float timer;
    private EnemyTag enemySystemTag;

    private void Start() {
        enemySystemTag = GetComponent<EnemyTag>();
        militaryBase = new EnemyTagMgr().getCurrentEnemyBaseByTag(enemySystemTag.getEnemyTag(), "EnemyBase");
        tmanager = militaryBase.GetComponent<AITroopManager>();        
        aNav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        timer = wanderTimer;
    }

    private void Update()
    {
        //Fix it! + add follow script individual
        
        // Debug.Log("IS assault troop: " )
        // Debug.Log(isLeader && isAssaultTeamMember);
        // if (isFollowingLeader && !isLeader)  
        // {
        //     aNav.SetDestination(currentDestination);
        // }  
        if (isLeader && isAssaultTeamMember)
        {
            movement();        
            isSearchForPlayerBase = true;   
        }   
         
        else
        {
            return;
        }
    }
    public bool searchingForPlayerBase()
    {
        return isSearchForPlayerBase;
    }
    public void setAssaultTeamMemberStatus(bool state)
    {
        isAssaultTeamMember = state;
    }
    public bool AssaultTeamMember()
    {
        return isAssaultTeamMember;
    }
    private bool NearPosition()
    {
        return !aNav.pathPending && aNav.remainingDistance <= aNav.stoppingDistance;       
    }
    public void enableFindingPlayerBase(bool mode)
    {
        findingPlayerBaseMode = mode;
    }
    public void goToDestination(Vector3 pos)
    {
        this.currentDestination = pos;
    }
    public void setAsLeader(bool leaderState)
    {
        this.isLeader = leaderState;
    }
    public bool amILeader()
    {
        return isLeader;
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

            aNav.SetDestination(newPos);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopTeamCheckSystem : MonoBehaviour
{
    [SerializeField] int currentTeamSize = 0;
    [SerializeField] int maxTeamMembers = 0;
    [SerializeField] string enemyTroopTag = "enemyTroop";
    [SerializeField] int minRequiredTroopsForSignal = 1;

    private AITBaseSearch teamLeader;
    private FollowLeaderAction teamFollower;
    private TroopTCode troopCode;
    private GameObject[] enemyTroops;
    private HealthEnemyAI hp;
    private bool isTeamFull = false;
    private bool isTeamEmpty = false;
    private bool saveMaxTeamMembersValue = false;
    // Update is called once per frame
    private GameObject enemyBase;
    private EnemyTag enemytag;
    private void Start() {
        enemytag = GetComponent<EnemyTag>();
        enemyBase = new EnemyTagMgr().getCurrentEnemyBaseByTag(enemytag.getEnemyTag(), "EnemyBase");
        teamLeader = GetComponent<AITBaseSearch>();
        teamFollower = GetComponent<FollowLeaderAction>();
        troopCode = GetComponent<TroopTCode>();
        hp = GetComponent<HealthEnemyAI>();
    }
    void Update()
    {
        if (isTeamEmpty)
        {
            return;
        }
        if ((( maxTeamMembers > currentTeamSize) && currentTeamSize <= minRequiredTroopsForSignal) && !isTeamEmpty)
        {
            isTeamEmpty = true;
            enemyBase.GetComponent<AITroopManager>().receiveSignalForRespawn(true);
            return;
        }
        if (teamLeader.amILeader() || teamFollower.FollowerOfTheLeader())
        {
            enemyTroops = GameObject.FindGameObjectsWithTag(enemyTroopTag);
            if (enemyTroops != null && troopCode != null) 
            {
                 
               
                //Send info to class troopmanager  to spawn new gameobjects
                var currentTroopCode = gameObject.GetComponent<TroopTCode>().getTroopCode();

                currentTeamSize = countTroopsByCode(enemyTroops, currentTroopCode);
                Debug.Log("Current team size: " + currentTeamSize);

                if (currentTeamSize >= maxTeamMembers)
                {
                     maxTeamMembers = currentTeamSize;
                     isTeamFull = true;
                     return;
                }  
                

            }
        }
    }
    private int countTroopsByCode(GameObject[] objects, string code)
    {
        int tAmount = 0;
        if (objects.Length <= tAmount)
        {
            Debug.Log("Team full and ready for battle!");
            tAmount = objects.Length - 1;
            return tAmount;
        }
        for (int tCIndex = 0; tCIndex < objects.Length; tCIndex++)
        {   
             if (objects[tCIndex].GetComponent<TroopTCode>().getTroopCode() == code)
             {
                tAmount++;
             }
        }
        if (!saveMaxTeamMembersValue)
            maxTeamMembers = tAmount;

        saveMaxTeamMembersValue = true;
        return tAmount;
    }
}

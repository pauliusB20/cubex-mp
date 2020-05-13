using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLeaderAction : MonoBehaviour
{
    //Script for making the troop for following the leader...
    [SerializeField] bool isFollowerOfTheLeader = false;
    private UnityEngine.AI.NavMeshAgent aTroop;
    private Transform leader;
    private bool isSearchingForPlayerBase = false;
    void Start()
    {
        aTroop = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowerOfTheLeader && leader != null)
        {
            isSearchingForPlayerBase = true;
            aTroop.SetDestination(leader.position);
        }
    }
    public void FollowerOfTheLeader(bool state, Transform whatleader)
    {
        isFollowerOfTheLeader = state;
        this.leader = whatleader; 
    }
    public bool FollowerOfTheLeader()
    {
        return isFollowerOfTheLeader;
    }
    public bool SearchingForBase()
    {
        return isSearchingForPlayerBase;
    }
}

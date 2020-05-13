using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBaseTroopAction : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool isAttackingPlayerBase;

    private UnityEngine.AI.NavMeshAgent aNav;
    private AITroopManager aTroopManager;
    private GameObject enemyBase;
    private EnemyTag enemyTag;
    void Start()
    {
        enemyTag = GetComponent<EnemyTag>();
        enemyBase = new EnemyTagMgr().getCurrentEnemyBaseByTag(enemyTag.getEnemyTag(), "EnemyBase");
        aNav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        aTroopManager = enemyBase.GetComponent<AITroopManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!NearPosition() && isAttackingPlayerBase)
        {
            aNav.isStopped = true;
            return;
        }

        if (isAttackingPlayerBase && NearPosition())
        {
            aNav.SetDestination(aTroopManager.getAttackPathPoints()[aTroopManager.getAttackPathPoints().Count - 1]);
        }
    }
    public void makeAttacker(bool state)
    {
        isAttackingPlayerBase = state;
    }
     private bool NearPosition()
    {
        return !aNav.pathPending && aNav.remainingDistance <= aNav.stoppingDistance;       
    }
}

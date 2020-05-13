using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MEnemyAIGameSessionConf", menuName = "EnemyAI_Test/MEnemyAIGameSessionConf", order = 0)]
public class EnemyAIGameSession :  ScriptableObject {
    
    [SerializeField]
    List <EnemyAITimeAction> enemyAIGameActionList;

    public List <EnemyAITimeAction> getEAGameActionList()
    {
        return enemyAIGameActionList;
    }
   
}

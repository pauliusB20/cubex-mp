using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTag : MonoBehaviour
{
    [SerializeField] string enemyTag = "A";
    [SerializeField] string enemyName = "empty";

    public void setEnemyTag(string tag)
    {
        enemyTag = tag;
    }
    public string getEnemyTag()
    {
        return enemyTag;
    }
    public string getEnemyObjectName()
    {
        return enemyName;
    }
}

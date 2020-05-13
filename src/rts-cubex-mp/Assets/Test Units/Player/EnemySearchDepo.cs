using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EnemySearchDepo 
    {
         [UnityTest]
        public IEnumerator EnemySearchDepoWithEnumeratorPasses()
        {
            var enemyTestingEnvironment = MonoBehaviour.Instantiate(Resources.Load("Prefabs/EnemyTestSD", typeof(GameObject))) as GameObject;
            Assert.True(enemyTestingEnvironment != null, "Loaded enemy testing prefab");
            Debug.Log(enemyTestingEnvironment != null ? "Loaded enemy testing prefab" : "Error prefab in resources not found!");

            var spawnedEnemyBase = enemyTestingEnvironment.transform.Find("EnemyAIBaseScanner").gameObject;            
            Debug.Log(spawnedEnemyBase != null ? "Enemy base structure is found!" : "Error enemy struture not found!");

            var enemyBaseManager = spawnedEnemyBase.GetComponent<EnemyAIBase>();

            Debug.Log(enemyBaseManager != null ? "Enemy base structure script is found!" : "Error enemy struture script not found!");           

            yield return new WaitForSeconds(30);

            Debug.Log(enemyBaseManager.getDepositFoundState() ? "All deposits are found!" : "Deposits are not found! Begining search mode..");

        }
    }
}

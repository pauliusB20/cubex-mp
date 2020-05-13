using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBaseSpawner : MonoBehaviour
{
    //Enemy Military Base spawner
    //The way it works, the game can set a specific amount of enemy bases to be spawned in the area, which will can be spawned in random positions
    // and if the amount reached the maximum amount of positions then no military bases are spawned...
    [Header("Primary military base energon paramters")]
    [SerializeField] int minEnergon = 20;
    [SerializeField] int maxEnergon = 50;
    [Header("Primary military base cube credits paramters")]
    [SerializeField] int minCredits = 250;
    [SerializeField] int maxCredits = 500;
    [Header("Primary military base collector paramters")]
    [SerializeField] int minColCount = 1;
    [SerializeField] int maxColCount = 4;
    [Header("Primary configuration parameters for the military base workers")]
    [SerializeField] int minMBaseWorkerCountMax = 3;
    [SerializeField] int maxMBaseWorkerCountMax = 6;

    [Header("Main military base configuration parameters")]
    [SerializeField] GameObject enemyMainBase;
    [SerializeField] int amountOfEnemyBases = 1;
   // [SerializeField] List<GameObject> cloneEnemyBases = new List<GameObject>();
    [SerializeField] List<Transform> enemyBasePositions;
    [SerializeField] string codeStartingLetter = "A";
    private RandomNumberGenerator rand = new RandomNumberGenerator();
    private void Start()
    {

       amountOfEnemyBases = SceneManager.GetActiveScene().buildIndex;
       //For xalculating random max worker count for the enemy base...
       maxMBaseWorkerCountMax = amountOfEnemyBases > 1 ? minMBaseWorkerCountMax * amountOfEnemyBases :  minMBaseWorkerCountMax * amountOfEnemyBases + amountOfEnemyBases + 1;

       int spawnPosition = 0;
       if (amountOfEnemyBases > enemyBasePositions.Count) 
       {
           Debug.LogError("Error! Max enemy spawn positions reached!");
           return;
       }

       for (int bIndex = 0; bIndex < amountOfEnemyBases; bIndex++ )
       {
           //Instantiating the military base in the specified position...
           GameObject enemyBase = Instantiate(enemyMainBase, enemyBasePositions[spawnPosition].position, Quaternion.identity);
           //Seting the military bases group code..
           enemyBase.GetComponent<EnemyBaseTag>().setEnemyMBaseTag(codeStartingLetter + spawnPosition);   
           //Setting the default starting energon amount..
           enemyBase.GetComponent<AIResourcesManagement>().setEnergon(Random.Range(minEnergon, maxEnergon));
           //Setting the default starting cube credits amount..
           enemyBase.GetComponent<AIResourcesManagement>().setAICubeCredits(Random.Range(minCredits, maxCredits)); 
           //Setting the default max count for the resource collectors..
        //    enemyBase.GetComponent<AIResourcesManagement>().setMaxRequiredCollectors(Random.Range(minColCount, maxColCount)); 
           //Setting the default starting worker amount..
           enemyBase.GetComponent<WorkerAIManager>().setWorkerMaxCount(Random.Range(minMBaseWorkerCountMax, maxMBaseWorkerCountMax));
           spawnPosition++;
       }
    }
    
    public void setAmountOfBases(int amount)
    {
        amountOfEnemyBases = amount;
    }
}

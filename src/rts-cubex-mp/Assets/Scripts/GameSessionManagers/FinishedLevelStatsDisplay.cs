using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishedLevelStatsDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text lvlStateText;
    [SerializeField] Text currentRank;
    [SerializeField] Text playerStats;
    [SerializeField] Text enemyStats;
    private GameSession gs;
    // void Start()
    // {
        
    // }
    private void Update() {
        gs = FindObjectOfType<GameSession>();
        if (gs != null)
        {
            lvlStateText.text = gs.getSessionText();
            currentRank.text = "Rank: " + gs.getRank();
            playerStats.text =  "Score: " + gs.getScorePlayerPoints() + ",\n" 
                            + "Spawned:\nWorkers: " + gs.getPlayerWorkersAmount()  + ",\n" 
                            + "Troop amount: " + gs.getPlayerTroopAmount()  + ",\n" 
                            + "Collector amount: " + gs.getPlayerCollectorAmount() + ",\n" 
                            + "Turret amount: " + gs.getPlayerTurretAmount() + ",\n" 
                            + "Walls amount:" + gs.getPlayerWallsAmount() + ",\n"
                            + "Barrack amount: " + gs.getPlayerBarrackAmount() + ",\n" 
                            + "Research center amount: " + gs.getPlayerResearchAmount() + ",\n" 
                            + "Enemy troop kill count: " +  gs.getPlayerEnemyKillCount() + ",\n" 
                            + "Enemy bases destroyed: " + gs.getPlayerEnemyBasesDescCount();
            
            enemyStats.text =  "Score: " + gs.getScoreEnemyPoints() + ",\n"
                            + "Spawned:\nWorkers: " + gs.getEnemyWorkerAmount()  + ",\n" 
                            + "Troop amount: " + gs.getEnemyTroopAmount()  + ",\n" 
                            + "Collector amount: " + gs.getEnemyCollectorAmount() + ",\n" 
                            + "Turret amount: " + gs.getEnemyTurretAmount() + ",\n" 
                            + "Walls amount: " + gs.getEnemyWallsAmount() + ",\n" 
                            + "Barrack amount: " + gs.getEnemyBarrackAmount() + ",\n" 
                            + "Research center amount: " + gs.getEnemyResearchAmount() + ",\n" 
                            + "Enemy troop kill count: " +  gs.getEnemyPlayerKillCount() + ",\n"  
                            + "Enemy bases destroyed: " + gs.getEnemyPlayerBasesDescCount();
        }
    }
   
}

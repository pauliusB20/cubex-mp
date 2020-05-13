using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    //Game session manager DEMO version <----------------------
    [Header("Main game session configuration parameters")]
    
    [SerializeField] RankConfiguration rankConfSystem;
    [Header("Player game session configuration parameters")]
    // - score points for the rank system....
    [SerializeField] int scorePlayerPoints = 0;
    [SerializeField] int xpPlayerPoints = 0;
    [SerializeField] int playerWorkerAmount = 0;
    [SerializeField] int playerTroopAmount = 0;
    [SerializeField] int playerCollectorAmount = 0;
    [SerializeField] int playerTurretAmount = 0;
    [SerializeField] int playerBarrackAmount = 0;
    [SerializeField] int playerResearchAmount = 0;
    [SerializeField] int playerWallsAmount = 0;
    [SerializeField] int pDestroyedEnemyBaseAmount = 0;
    [SerializeField] int pDestroyedEnemyUnitAmount = 0;
    [Header("Enemy A.I. game session configuration parameters")]
    [SerializeField] int scoreEnemyPoints = 0;
    [SerializeField] int xpEnemyPoints = 0;
    [SerializeField] int enemyWorkerAmount = 0;
    [SerializeField] int enemyTroopAmount = 0;
    [SerializeField] int enemyTurretAmount = 0;
    [SerializeField] int enemyWallsAmount = 0;
    [SerializeField] int enemyCollectorAmount = 0;
    [SerializeField] int enemyBarrackAmount = 0;
    [SerializeField] int enemyResearchAmount = 0;
    [SerializeField] int eDestroyedPlayerBaseAmount = 0;
    [SerializeField] int eDestroyedPlayerUnitAmount = 0;
    [SerializeField] bool isEnemyDestroyed = false; 
    [Header("Status game session configuration parameters")]
    //Text for holding game state - you win - you loose
    [SerializeField] string sessionStatusText;
    //If you get let's say 100 points, you become great leader...
    [SerializeField] string sessionRankText;

  
    void Awake()
    {
        //Singleton pattern - Doesn't destroy this game object when you load a next scene
        //This pattern will usefull for display score and other data in the next scene....
        int lengthGameSession = FindObjectsOfType<GameSession>().Length;

        if (lengthGameSession > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (isEnemyDestroyed)
        {
            sessionStatusText = "Victory";
        }
        else
        {
            sessionStatusText = "Defeat";
        }

        if (sessionStatusText != null || sessionStatusText != "")
        {
           sessionRankText = rankConfSystem.getRank(scorePlayerPoints);
        }


    }
    
    //Method for reseting the system.
    //For instance if you don't want data about the level to be showed in the main menu scene but you only wnat to see in the score 
    //scene, then this method. It will be usefull because in order to stop the data stored on this object to go to another scene, 
    //you must delete this gameobject
    //by using this method.
    public void enemyDestroyedState(bool state)
    {
        isEnemyDestroyed = state;
    }
    public void ResetGame()
    {
        //Reset conf. file...
        Destroy(gameObject);
    }
    public void increaseDestroyedEnemyBaseAmount()
    {
        pDestroyedEnemyBaseAmount++;
    }
    public void increasePEnemyUnitKillAmount()
    {
        pDestroyedEnemyUnitAmount++;
    }
    public void addPlayerTurretAmount(int g)
    {
        playerTurretAmount += g;
    }
    public int getPlayerTurretAmount()
    {
       return playerTurretAmount;
    }
    //Player score adding methods
    public void AddPlayerScorePoints(int points)
    {
        scorePlayerPoints += points;
    }
    
    public void addPlayerWorkersAmount(int w)
    {
        playerWorkerAmount += w;
    }
    public void addTroopAmount(int t)
    {
        playerTroopAmount += t;
    }
     public void addPlayerCollectorAmount(int c)
    {
        playerCollectorAmount += c;
    }
    public void addPlayerBarrackAmount(int b)
    {
        playerBarrackAmount += b;
    }
    public void addPlayerResearchAmount(int r)
    {
        playerResearchAmount += r;
    }
    public void addPlayerWallsAmount(int o)
    {
        playerWallsAmount += o;
    }

    //---------------->Enemy A.I. section<--------------------
     public void increaseDestroyedPlayerBaseAmount()
    {
        eDestroyedPlayerBaseAmount++;
    }
    public void increaseEPlayerUnitKillAmount()
    {
        eDestroyedPlayerUnitAmount++;
    }
    public void addEnemyTurretAmount(int g)
    {
        enemyTurretAmount += g;
    }
    public int getEnemyTurretAmount()
    {
        return enemyTurretAmount;
    }
    //Enemy score adding methods
    public void AddEnemyScorePoints(int points)
    {
        scoreEnemyPoints += points;
    }
    
     public void AddEnemyXPPoints(int points)
    {
        xpEnemyPoints += points;
    }

     public void addEnemyWorkerAmount()
    {
        enemyWorkerAmount++;
    }
    public void addEnemyTroopAmount(int t)
    {
        enemyTroopAmount += t;
    }
     public void addEnemyCollectorAmount(int c)
    {
        enemyCollectorAmount += c;
    }
    public void addEnemyBarrackAmount(int b)
    {
        enemyBarrackAmount += b;
    }
    public void addEnemyResearchAmount(int r)
    {
        enemyResearchAmount += r;
    }
    public void addEnemyWallsAmount(int q)
    {
        enemyWallsAmount += q;
    }
    //For getting the score and certain base unit amounts
    //Player parameter getter methods
    public int getScorePlayerPoints()
    {
        return scorePlayerPoints;
    }
    public int getXpPlayerPoints()
    {
        return xpPlayerPoints;
    }
    
    public int getPlayerWorkersAmount()
    {
        return playerWorkerAmount;
    }
    public int getPlayerTroopAmount()
    {
        return playerTroopAmount;
    }
    public int getPlayerCollectorAmount()
    {
        return playerCollectorAmount;
    }
    public int getPlayerBarrackAmount()
    {
        return playerBarrackAmount;
    }
    public int getPlayerResearchAmount()
    {
        return playerResearchAmount;
    }
   
    public int getPlayerEnemyKillCount()
    {
        return pDestroyedEnemyUnitAmount;
    }
    public int getPlayerEnemyBasesDescCount()
    {
        return pDestroyedEnemyBaseAmount;
    }
    public int getPlayerWallsAmount()
    {
        return playerWallsAmount;
    }
   //Enemy parameter getter methods
    public int getScoreEnemyPoints()
    {
        return scorePlayerPoints;
    }
    public int getXpEnemyPoints()
    {
        return xpPlayerPoints;
    }
    public int getEnemyWorkerAmount()
    {
        return enemyWorkerAmount;
    }
    public int getEnemyTroopAmount()
    {
        return enemyTroopAmount;
    }
    public int getEnemyCollectorAmount()
    {
        return enemyCollectorAmount;
    }
    public int getEnemyBarrackAmount()
    {
        return enemyBarrackAmount;
    }
    public int getEnemyResearchAmount()
    {
        return enemyResearchAmount;
    }
    public int getEnemyWallsAmount()
    {
        return enemyWallsAmount;
    }
    public int getEnemyPlayerKillCount()
    {
        return eDestroyedPlayerUnitAmount;
    }
    public int getEnemyPlayerBasesDescCount()
    {
        return eDestroyedPlayerBaseAmount;
    }
    //Session text...
    public void setSessionText(string state)
    {
        sessionStatusText = state;
    }
    public string getSessionText()
    {
        return sessionStatusText;
    }
    public void setRank(string rk)
    {
        sessionRankText = rk;
    }
    public string getRank()
    {
        return sessionRankText;
    }
}

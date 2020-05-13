using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchCenterManager : MonoBehaviour
{
   [Header("Main configuration parameters")]
   [SerializeField] int researchTechLevel = 0;
   [SerializeField] int maxResearchTechLevel = 3;
   [SerializeField] int upgradePrice = 50;
   [SerializeField] int upgradePriceOffset = 30;
   [SerializeField] bool isReachingMaxCap = false;
   [SerializeField] bool needToUpgrade = true; //CommandForUpgrade
   [SerializeField] bool isUpgraded = false;
   [SerializeField] string pTLvlCode = "UpMLvl";

   private GameObject enemyBase;
   private AIResourcesManagement arm;
   private EnemyAIBase eb;
   private EnemyTag enemyTag;
    //For checking unit availability and level upgrade capacities, add scriptable object file...
   private void Start() {
       //Once appears this object in the game it tryes to add it's position to the troop patrol wave list..
       enemyTag = GetComponent<EnemyTag>();
       enemyBase = new EnemyTagMgr().getCurrentEnemyBaseByTag(enemyTag.getEnemyTag(), "EnemyBase");
       arm = enemyBase.GetComponent<AIResourcesManagement>();
       eb = enemyBase.GetComponent<EnemyAIBase>();
       new EnemyTagMgr().getCurrentEnemyBaseByTag(enemyTag.getEnemyTag(), "EnemyBase").GetComponent<AITroopManager>().AddPatrolPoint(transform.position);
   }
   private void Update() {
       
      upgradeResearchLvl();
   }
   public void upgradeResearchLvl()
   {
       if ((eb.canStartSpawningWorkers() && arm.buy(pTLvlCode, upgradePrice)) && needToUpgrade)
       {
            Debug.Log("Tech level upgraded! State: successs..");   
            increaseResearchLvl();
            increasePrice();         
            needToUpgrade = false;
            isUpgraded = true;
            return;
       }
   }
   public bool Upgraded()
   {
       return isUpgraded;
   }
   
   public void setResearchTechLevel(int lvl)
   {
       this.researchTechLevel = lvl;
   }

   public int getResearchTechLevel()
   {
       return researchTechLevel;
   }
   //Method for increasing the researchLevel...
    public void increaseResearchLvl()
    {
        if (researchTechLevel >= maxResearchTechLevel)
        {
            researchTechLevel = maxResearchTechLevel;
            isReachingMaxCap = true;
        }
        if (!isReachingMaxCap)
        {
            researchTechLevel++;
        } 
    }
    public void increasePrice()
    {
        this.upgradePrice += upgradePriceOffset;
    }
    public int currentPrice()
    {
        return upgradePrice;
    }
    public bool reachedMaxCap()
    {
        return isReachingMaxCap;
    }
}

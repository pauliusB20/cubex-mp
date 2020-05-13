using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EnemyAI barracks code for spawning troops...
public class SpawnTroopUnit : MonoBehaviour
{
    [Header("Troop Unit configuration parameters")]
    [SerializeField] MTroopUnits troopUnitConfObj;
    [SerializeField] int currentTroopIndexInList = 0;
    [SerializeField] GameObject troopUnit;
    [SerializeField] Transform assaultTroopRestingArea;
    [SerializeField] float timeBeforeSpawn = 2f;
    [SerializeField] Transform spawnPosition;    
    [SerializeField] int minPatrollUnitsCount = 2;
    [SerializeField] int lightTroopUnitPrice = 5;
    [SerializeField] int heavyTroopUnitPrice = 5;
    [SerializeField] bool isSearchingForDeposit = false;
    [SerializeField] string troopLightUnitName = "LightTroopUnit";
    [SerializeField] string troopHeavyUnitName = "HeavyTroopUnit";
    [SerializeField] List<string> troopCodes;
    [SerializeField] bool spawnTroopPatroll = false;
    [SerializeField] int patrollUnitCount = 0;
    [SerializeField] bool spawnTroopAttack = false;
    [SerializeField] int attackTroopCount = 0;
    [SerializeField] bool searchParty = false;
    [SerializeField] int troopSearchCount = 0;
    
    private GameObject enemyBase;
    AIResourcesManagement arm;
    AITroopManager atm;
    EnemyAIBase eb;   
    GameSession gs; 
    private EnemyTag enemyTag;
    List<TroopUnit> troops;
     private void Start() {
          //Once appears this object in the game it tryes to add it's position to the troop patrol wave list..
        gs = FindObjectOfType<GameSession>();
        gs.addEnemyBarrackAmount(1);
        
        enemyTag = GetComponent<EnemyTag>();
        enemyBase = new EnemyTagMgr().getCurrentEnemyBaseByTag(enemyTag.getEnemyTag(), "EnemyBase");
        enemyBase.GetComponent<AITroopManager>().AddPatrolPoint(transform.position);
        arm = enemyBase.GetComponent<AIResourcesManagement>();
        atm = enemyBase.GetComponent<AITroopManager>();
        eb = enemyBase.GetComponent<EnemyAIBase>();
        troops = new List<TroopUnit>();
    }
    void Update()
    {
      if (troopCodes != null)
      {
        spawnTroopPatroll = atm.NeedingPatrolUnits();
        spawnTroopAttack = atm.NeedingForAttack();
        searchParty = eb.areDepositsNotInArea();

        if (searchParty && !atm.enoughForSearchParty())
        {
          var researchCenterExists = new EnemyTagMgr().enemyObjectExists(enemyTag.getEnemyTag(), "EBRCenter");
          if (researchCenterExists)
          {
            var researchCenterBuilding = new EnemyTagMgr().locateEnemyObjectWithTag(enemyTag.getEnemyTag(), "EBRCenter");
            var researchCenter = researchCenterBuilding.GetComponent<TroopUnitUpgradeManager>();
            if (researchCenterBuilding != null  && researchCenter != null)
              if(researchCenter.getCurrentTroopTechLevel() > 0)
                spawnTroopUnit(troopCodes[Random.Range(0, troopCodes.Count)], false, false, searchParty);  
              else
                spawnTroopUnit(troopCodes[currentTroopIndexInList], false, false, searchParty);  
          }
         
             spawnTroopUnit(troopCodes[currentTroopIndexInList], false, false, searchParty);         
        }

        if (spawnTroopPatroll && !atm.EnoughPatrolUnits())
        {
          var researchCenterExists = new EnemyTagMgr().enemyObjectExists(enemyTag.getEnemyTag(), "EBRCenter");
          if (researchCenterExists)
          {
            var researchCenterBuilding = new EnemyTagMgr().locateEnemyObjectWithTag(enemyTag.getEnemyTag(), "EBRCenter");
            var researchCenter = researchCenterBuilding.GetComponent<TroopUnitUpgradeManager>();

           if (researchCenterBuilding != null  || researchCenter != null)
            if(researchCenter.getCurrentTroopTechLevel() > 0)
              spawnTroopUnit(troopCodes[Random.Range(0, troopCodes.Count)], spawnTroopPatroll, false, false); 
            else
              spawnTroopUnit(troopCodes[currentTroopIndexInList], spawnTroopPatroll, false, false); 
          }
             spawnTroopUnit(troopCodes[currentTroopIndexInList], spawnTroopPatroll, false, false);           
        }
    
        if (spawnTroopAttack && !atm.EnoughAttackUnits())
        {
          var researchCenterExists = new EnemyTagMgr().enemyObjectExists(enemyTag.getEnemyTag(), "EBRCenter");
          if (researchCenterExists)
          {
            var researchCenterBuilding = new EnemyTagMgr().locateEnemyObjectWithTag(enemyTag.getEnemyTag(), "EBRCenter");
            var researchCenter = researchCenterBuilding.GetComponent<TroopUnitUpgradeManager>();
            if (researchCenterBuilding != null  && researchCenter != null)
              if(researchCenter.getCurrentTroopTechLevel() > 0)
                spawnTroopUnit(troopCodes[Random.Range(0, troopCodes.Count)], false, spawnTroopAttack, false);   
              else
                spawnTroopUnit(troopCodes[currentTroopIndexInList], false, spawnTroopAttack, false); 
          }
               spawnTroopUnit(troopCodes[currentTroopIndexInList], false, spawnTroopAttack, false);        
        }
        else
         return;

      }
       else 
        return;
    }
    private void spawnTroopUnit (string troopCode, bool stateTroopPatroll, bool spawnTroopAttack, bool searchParty)
    {
        //Research center tech level checking...
        //Universal spawning method code...
        bool canSpawn = false;      
        string tempCode = troopCode;
        bool troopState = false;
        string troopName = "n";
        int tAmount = 1;
        //AddPurchase sequence...
       
        switch(tempCode)
        {
          case "tlu":
           troopName = troopLightUnitName;
           break;
          case "thu":
            troopName = troopHeavyUnitName;
            tAmount++;
            break;
        }       
        // if (enemyBase.GetComponent<EnemyBaseTag>().getEnemyMBaseTag() != enemyTag.getEnemyTag()) return;
        canSpawn = arm.buy(troopName, troopUnitConfObj.getUnitPriceInCredits(tempCode));
        if (canSpawn)
        {
              //Delay till next spawn...
             StartCoroutine(countDownTillNextSpawn());
             GameObject spawnedTrooper = Instantiate(
                                          troopUnitConfObj.getTroopUnit(tempCode), 
                                                                spawnPosition.position,
                                                                          Quaternion.identity
                                          ) as GameObject;             
            troops.Add(
                new TroopUnit(troopName, spawnedTrooper, tempCode, troopUnitConfObj.getUnitPriceInCredits(tempCode))
            );    
                                     
           if (searchParty)
            {
              Debug.Log("Searching: " + searchParty);
              // spawnedTrooper.GetComponent<TroopPatrollAction>().setPatrolModeState(false);
              spawnedTrooper.GetComponent<AIwanderer>().needTolookForDeposit(true);
              spawnedTrooper.GetComponent<EnemyTag>().setEnemyTag(enemyTag.getEnemyTag());
              troopSearchCount++;
             return;
            }
            if (stateTroopPatroll/* && !searchParty*/)
            {
              spawnedTrooper.GetComponent<TroopPatrollAction>().setPatrolModeState(stateTroopPatroll);
              spawnedTrooper.GetComponent<EnemyTag>().setEnemyTag(enemyTag.getEnemyTag());
              patrollUnitCount++;
              return;
            } 
            if (/*(!stateTroopPatroll && !searchParty) && */spawnTroopAttack) 
            {  
              spawnedTrooper.GetComponent<AITBaseSearch>().setAssaultTeamMemberStatus(true); 
              spawnedTrooper.GetComponent<EnemyTag>().setEnemyTag(enemyTag.getEnemyTag());             
              attackTroopCount++;
              return;
            }
        }
        else
        {
          return;
        }
    }
    public void resetAttackTroopMaxCount()
    {
      attackTroopCount = 0;
    }
    IEnumerator countDownTillNextSpawn()
    {
       yield return new WaitForSeconds(5f);
    }
    public int getTroopSearchCount()
    {
      return troopSearchCount;
    }
    
    public int getPatrollUnitCount()
    {
      return patrollUnitCount;
    }
    public int getAttackTroopCount()
    {
      return attackTroopCount;
    }
    
   private void printAttentionMsg(string msg)
   {
     Debug.Log(msg);
   }
}

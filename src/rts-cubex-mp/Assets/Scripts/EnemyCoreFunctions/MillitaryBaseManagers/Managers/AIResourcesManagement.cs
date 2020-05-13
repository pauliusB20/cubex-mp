using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIResourcesManagement : MonoBehaviour
{
   [Header("Resource management configuratino parameters")]
   [SerializeField] string collectorName = "Collector";
   [SerializeField] string energonRCode = "cre";
   [SerializeField] int requiredEnergonAmountRate = 40;
   [SerializeField] string collectorCode = "eambc";
  // [SerializeField] int energonReserveSwitchIndex = 0;
   [SerializeField] bool noMoreDepositPoints = false;
   [SerializeField] int maxRequiredCollectors = 1;
   [SerializeField] int spawnedCollectorAmount = 0;
   [SerializeField] int AIcubeCredits = 40;
   [SerializeField] int reserveStorageOffset = 5;
   [SerializeField] int reserveEnergonStorageOffset = 1;
   [SerializeField] int AIenergon = 20;
   [SerializeField] const float creditConstant = 0.25f;
   [SerializeField] int conversionAmount = 10;
   [SerializeField] int maxRezerveCount = 4;
   [SerializeField] bool canConvert = false;
   [Header("Energon reserve indexer")]
   [SerializeField] int eRIndex = 0;
   //Enemy A.I. game element list
   [SerializeField] List<string> itemNames;
   [SerializeField] List<string> itemERNames;
   //[SerializeField] List<string> energonRNames;
   [Header("Starter pack configuration parameters")]
   [SerializeField] int workerAmount = 2;
   [SerializeField] int barracksAmount = 1;
   [SerializeField] int troopUnitAmount = 2;
   [SerializeField] int collectorAmount = 4;
   //New credit reserve list
   [SerializeField] List<_CreditRezerve> creditRezerves = new List<_CreditRezerve>();
   [SerializeField] List<EnergonRezerve> energonRezerves = new List<EnergonRezerve>();
   [Header("Resource collection point")]
   [SerializeField] GameObject collectionPoint;
  
    WorkerAIManager wam;
    AITroopManager atm;
    EnemyAIBase eb;
    int rezerveIndex = 0, depositIndex = 0;
    private bool isPackGenerated = false;
    private EnemyBaseTag enemyBaseTag;
    private EnemyTagMgr enemytagMgr; 
    void Start()
    {
        //Initializaing variables...
        enemytagMgr = new EnemyTagMgr();
        eb = GetComponent<EnemyAIBase>();
        wam = GetComponent<WorkerAIManager>();
        atm = GetComponent<AITroopManager>();
        enemyBaseTag = GetComponent<EnemyBaseTag>();

        if (itemNames.Count > 0 || itemNames != null)
        {
            for (int itemIndex = 0; itemIndex < itemNames.Count; itemIndex++)
            {
                creditRezerves.Add(new _CreditRezerve(itemNames[itemIndex], enemyBaseTag.getEnemyMBaseTag()));
            }
            //Adds default energon rezerve list
            for (int itemEIndex = 0; itemEIndex < itemERNames.Count; itemEIndex++)
            {
                energonRezerves.Add(new EnergonRezerve(itemERNames[itemEIndex], enemyBaseTag.getEnemyMBaseTag()));
            }
          

            generateStarterReservePack();
            generateEnergonStarterPack();
            Debug.Log(isPackGenerated ? "Success! Starter credit pack is here!" : "Error something went wrong!");
           
        }
        else
        {
            Debug.LogError("Item list is not initialized or size is 0!");
        }
    
    }
    void Update()
    {
       //Check if can convert energon to cube credits..
        if (CreditCenversionStatus())
        {
            checkAndConvertToCubeCredits();
        }
       
      //Logic for checking if a resource gathering collector is required in the military base..
        if (wam.spawAgain())
        {
            if (eb.getDepositFoundState())
            {
                if (!requiredCollectors(maxRequiredCollectors))
                {
                    Debug.Log("I will try to build: " + collectorName);
                    buildCollectorStructure();
                   // spawnedCollectorAmount++;
                }
                else
                {
                   
                    return;
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
              
    }
    public Vector3 getResourceCollectionPoint()
    {
        return collectionPoint.transform.position;
    }
    //For checking if there is one deposit in the base area...
    // private int WorkersDepositSize()
    // {
    //     int size = 0;
    //     var workers = new EnemyTagMgr().locateEnemyObjectsWithTag("A1", "EBWorker");

    //     for (int iWorker = 0; iWorker < workers.Length; iWorker++)
    //     {
    //         if (workers[iWorker].getBuildSiteTag() == "Depo")
    //          size++;
    //     }

    //     return size > 0 && size <= 1 ? size : 0;
    // }
    //Add data from file...
    private void  generateStarterReservePack()
    {
        var collectorCreditPrice = eb.getBaseStructureList().getBuildingPrice(collectorCode);
        var barracksCreditPrice = eb.getBaseStructureList().getBuildingPrice("eambb");

        if (AIcubeCredits >= workerAmount * wam.getWorkerUnitPrice())
        {
            creditRezerves[0].setRezerveCap(workerAmount * wam.getWorkerUnitPrice());
            AIcubeCredits -= (workerAmount * wam.getWorkerUnitPrice());
        }
        if (AIcubeCredits >= collectorAmount * collectorCreditPrice)
        {
            creditRezerves[1].setRezerveCap(collectorAmount * collectorCreditPrice);
            AIcubeCredits -= (collectorAmount * collectorCreditPrice);
        }
        if (AIcubeCredits >= barracksAmount * atm.getBarracksPrice())
        {
            creditRezerves[2].setRezerveCap(barracksAmount * barracksCreditPrice);
            AIcubeCredits -= barracksAmount * atm.getBarracksPrice();
        }
        if (AIcubeCredits >= troopUnitAmount * atm.getLightTroopUnitPrice())
        {
            creditRezerves[3].setRezerveCap(troopUnitAmount * atm.getLightTroopUnitPrice());
            AIcubeCredits -= troopUnitAmount * atm.getLightTroopUnitPrice();
        }
       
       
    }
    private void generateEnergonStarterPack()
    {
        var collectorEnergonPrice = eb.getBaseStructureList().getEnergonPrice(collectorCode);
        if (AIenergon >= collectorAmount * collectorEnergonPrice)
        {
            energonRezerves[1].setRezerveCap(collectorAmount * collectorEnergonPrice);
            AIenergon-= (collectorAmount * collectorEnergonPrice);
        }
        if (AIenergon >= barracksAmount * atm.getBarracksEnergonPrice())
        {
            energonRezerves[2].setRezerveCap(barracksAmount * atm.getBarracksEnergonPrice());
            AIenergon -= barracksAmount * atm.getBarracksEnergonPrice();
        }
        
        isPackGenerated = true;
        printRezerves();
        printEnergonRezerves();
    }
     private void buildCollectorStructure()
     {
          var collectorCreditPrice = eb.getBaseStructureList().getBuildingPrice(collectorCode);
          var collectorEnergonPrice = eb.getBaseStructureList().getEnergonPrice(collectorCode);
          var collectorBuilding = eb.getBaseStructureList().getBuilding(collectorCode);    
//Fix this
         if (eb.getDepositPoints().Count <=  depositIndex)
         {
             depositIndex = 0;
             noMoreDepositPoints = true;
             return;
         }
         bool hasEnoughCreditsForCollector = false;
         var depositPoints = eb.getDepositPoints();
        
       if(requiredCollectors(maxRequiredCollectors)) return;

      
        Debug.Log("Current deposit index on which the collector is going to be built:" + depositIndex);
        if (wam.areThereAnySpawnedFreeWorkers())
        { 
        //   var workers = enemytagMgr.locateEnemyObjectsWithTag(enemyBaseTag.getEnemyMBaseTag(), "EBWorker");
         
          if (eb.getDepositPoints()[depositIndex].getPointStatus() == EnemySystemConstants.DESTROYED_POINT) 
          {
              if (eb.getDepositPoints().Count >= depositIndex)
                   depositIndex = 0;
               return;
          }
          hasEnoughCreditsForCollector = buy(collectorName,  collectorCreditPrice, collectorEnergonPrice);
          

            wam.assignExistingWorkerToTask(true, "Depo", eb.getDepositPoints()[depositIndex].getPosition(), collectorBuilding);  
            eb.getDepositPoints()[depositIndex].setPointStatus(EnemySystemConstants.DESTROYED_POINT);    
          
       }
        else 
        {
            //If worker count has reached max, then return and do nothing else..
           if (wam.workerCountReachedMax()) return;
           
           hasEnoughCreditsForCollector =  buy(collectorName,  collectorCreditPrice, collectorEnergonPrice);
          if (hasEnoughCreditsForCollector)  
          {            
            wam.spawnWorker(true, "Depo", eb.getDepositPoints()[depositIndex].getPosition(), collectorBuilding);    
          }
          else 
          {
           // Debug.Log("Can't spawn! insuficient credits!");
            return;
          }
          
        }
        depositIndex++;
        //Debug.Log("Group: " + enemyBaseTag.getEnemyMBaseTag() + " resources:");
       
        printRezerves();
        printEnergonRezerves();
        Debug.Log("Building at index: " + depositIndex);
        
     }

    

   public bool requiredCollectors(int maxRequiredCollectors)
    {
       //Method for checking the if there are enough collectors in the base area...
       var enemyTagMgr = new EnemyTagMgr();
       if (!enemyTagMgr.enemyObjectsExist("EBCollector")) return false;
       var collectors = enemyTagMgr.locateEnemyObjectsWithTag(enemyBaseTag.getEnemyMBaseTag(), "EBCollector");
       var collectorSize = collectors != null ? collectors.Length : 0;
       return collectorSize >= maxRequiredCollectors;
   } 
   public void setAICubeCredits(int credit)
   {
       AIcubeCredits = credit;
   }
   //Method for purchasing
   public void buy(int price)
   {
       AIcubeCredits -= price;
   }

    public int getAICubeCredits()
   {
       return AIcubeCredits;
   }
   public void setEnergon(int energon)
   {
       AIenergon = energon;
   }
   public int getAIenergon()
   {
       return AIenergon;
   }
   public void addEnergonToBase(int energon)
   {
       //First check if there are enough energon in the energon reserve cell, for credit conversion...
       
       if (eRIndex >= energonRezerves.Count) eRIndex = 0;

       AIenergon += energon;
      //If all energon rezerves ae full, then return and do not fill them again..
      if(RezervesEnergonFull()) 
      {
           if (AIenergon >= reserveStorageOffset)
            AIenergon -= reserveStorageOffset;
          
          return;
      }

       if (AIenergon >= reserveStorageOffset)
       {
           if (!energonRezerves[eRIndex].Full())
           {
                energonRezerves[eRIndex].addEnergon(AIenergon);
                AIenergon -= reserveStorageOffset;
                eRIndex++;
           }
           else
           {

               eRIndex++;
               if (eRIndex >= energonRezerves.Count) eRIndex = 0;

               energonRezerves[eRIndex].addEnergon(AIenergon);
               AIenergon -= reserveStorageOffset;
               eRIndex++;
           }
        
       }

   }
   private bool CreditCenversionStatus()
   {
        if (energonRezerves == null) return false;

        EnergonRezerve creditEReserve = null;

        for (int eIndex = 0; eIndex < energonRezerves.Count; eIndex++)
        {
            if (energonRezerves[eIndex].getReserveName() == energonRCode)
            {
                creditEReserve = energonRezerves[eIndex];
                break;
            }
        }
        if (creditEReserve == null) return false;
        //If enough energon in the energon reserve cell, then begin the credit conversion calculation algorithm..
       
        return creditEReserve.getEnergonReserveCap() - conversionAmount >= 0;
   }
   private bool RezervesEnergonFull()
   {
       int size = 0;
        foreach (var ceRezerve in energonRezerves)
       {
           if (ceRezerve.Full())
           {
               size++;

           }
       }
       return size > 0;
   }
   //Method for automatic purchasing..
  public bool buy(string itemName, int price)
  {
          
      for (int rIndex = 0; rIndex < creditRezerves.Count; rIndex++)
      {
          if ((creditRezerves[rIndex].getCreditReserveCap() >= price && creditRezerves[rIndex].getReserveName() == itemName)
           && creditRezerves[rIndex].getBelogingGroup() == enemyBaseTag.getEnemyMBaseTag())
          {
              creditRezerves[rIndex].setRezerveCap(creditRezerves[rIndex].getCreditReserveCap() - price);
              Debug.Log("Purchasing item: " + itemName + ", status: success!");
              printRezerves();
              return true;
          }
      }
        return false;
  }
  public bool buy(string itemName, int price, int energonPrice)
  {     
      for (int rIndex = 0; rIndex < creditRezerves.Count; rIndex++)
      {

        for (int eIndex = 0; eIndex < energonRezerves.Count; eIndex++)
        {
          if ((creditRezerves[rIndex].getReserveName() == itemName && energonRezerves[eIndex].getReserveName() == itemName)
               && (creditRezerves[rIndex].getCreditReserveCap() >= price && energonRezerves[eIndex].getEnergonReserveCap() >= energonPrice)
                && (creditRezerves[rIndex].getBelogingGroup() == enemyBaseTag.getEnemyMBaseTag() && 
                    energonRezerves[eIndex].getBelongingCode() == enemyBaseTag.getEnemyMBaseTag()))
          {
              creditRezerves[rIndex].setRezerveCap(creditRezerves[rIndex].getCreditReserveCap() - price);
              energonRezerves[eIndex].setRezerveCap(energonRezerves[eIndex].getEnergonReserveCap() - energonPrice);
              Debug.Log("Purchasing item: " + itemName + ", status: success!");
              printRezerves();
              printEnergonRezerves();
              return true;
          }
        }
      }
    //    printRezerves();
    //    printEnergonRezerves();
        return false;
  }
  //Credit conversion method
  public void checkAndConvertToCubeCredits()
  {
     if (energonRezerves == null) return;

     EnergonRezerve creditEReserve = null;

     for (int eIndex = 0; eIndex < energonRezerves.Count; eIndex++)
     {
        if (energonRezerves[eIndex].getReserveName() == energonRCode)
        {
            creditEReserve = energonRezerves[eIndex];
            break;
        }
     }

     if (creditEReserve == null) return;
      //If enough energon in the energon reserve cell, then begin the credit conversion calculation algorithm..
      if (creditEReserve.getEnergonReserveCap() - conversionAmount >= 0)
      {
         creditEReserve.setRezerveCap(creditEReserve.getEnergonReserveCap() - conversionAmount);
         AIcubeCredits += ((int)(Mathf.Round((float) conversionAmount / creditConstant)));
         
         if (creditRezerves.Count != null &&  AIcubeCredits > 0)
         {
             bool isProcessComplete = false;
             int rIndex = 0, gIndex = 0;
           
           while (AIcubeCredits > 0)
           {
               //Storing credits sums into seperate cells
               if (rIndex >= creditRezerves.Count) rIndex = 0;

               if (!creditRezerves[rIndex].Full())
               {
                   //Adds reserveStorageOffset from AIcubeCredits...
                   creditRezerves[rIndex].addCredits(reserveStorageOffset);  
                   AIcubeCredits -= reserveStorageOffset; 
                   rIndex++;
               } 
              
               gIndex++;
           }
        //   Debug.Log("rIndex: " + rIndex);
         //  Debug.Log("gIndex: " + gIndex);
        
           // Debug.Log("Temp sum: " + AIcubeCredits);
            if (AIcubeCredits <= 0)
            {
                Debug.Log("Credits left: " + AIcubeCredits);  
                Debug.Log("Stored credits:<------------------------");
                printRezerves();
                AIcubeCredits = 0;
                isProcessComplete = true;
            }
            Debug.Log(isProcessComplete ? "Credit storage process complete!" : "Failed storing credits in storage");

         }
         else
         {
             Debug.LogError("Error! credit reserve list is not initialized!");
         }

      }
  }
 
   public void printRezerves()
   {
       Debug.Log("----------->CUBE CREDIT RESERVES<-------------");
       for (int rIndex = 0; rIndex < creditRezerves.Count; rIndex++)
       {
           //Debug.Log("--------------------->Group creditRezerve:" + enemyBaseTag.getEnemyMBaseTag()+ "<---------------------");
           Debug.Log(creditRezerves[rIndex].toString());
       }
   }
   public void printEnergonRezerves()
   {
       Debug.Log("----------->ENERGON RESERVES<-------------");
       for (int rIndex = 0; rIndex < energonRezerves.Count; rIndex++)
       {
          //  Debug.Log("--------------------->Group energonRezerve:" + enemyBaseTag.getEnemyMBaseTag()+ "<---------------------");
           Debug.Log(enemyBaseTag.getEnemyMBaseTag() + ": " + energonRezerves[rIndex].toString());
       }
   }
   public List<_CreditRezerve> getCreditReserves()
   {
       return creditRezerves;
   }
   public List<EnergonRezerve> getEnergonReserves()
   {
       return energonRezerves;
   }
   public void decreaseCollectorAmount()
   {
       if (spawnedCollectorAmount > 0)
         spawnedCollectorAmount--;
       else 
          return;
   }
   public int getUsedDepositCount()
   {
      return depositIndex;
   }
   public void increaseUsedDepositCount()
   {
       if (eb.getDepositPoints().Count <= depositIndex)
       {
           depositIndex = 0;
            return;
       }

       depositIndex++;

   }
   public int getCollectorAmount()
   {
       return spawnedCollectorAmount;
   }
   public void increaseCollectorAmount()
   {
       spawnedCollectorAmount++;
   }
   public void setMaxRequiredCollectors(int mrc)
   {
       maxRequiredCollectors = mrc;
   }
}

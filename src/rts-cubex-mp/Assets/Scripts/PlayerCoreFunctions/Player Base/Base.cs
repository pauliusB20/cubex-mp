using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Base : MonoBehaviour
{
    [Header("market Configuration")]
    [SerializeField] GameStorageConf gamecnf;
    [Header("Main Base tool display parameters")]
    [SerializeField] GameObject Screen; // this GameObject variable for saving main base panel
    [SerializeField] GameObject ResourceAmountScreen;// this is the screen which will apear when player will not have enough resources to build game structure; 
    [SerializeField] GameObject ResourcesAmountScreenForUpgrades; // this Error screen for upgrades when player does not have enough resources
    [SerializeField] GameObject ErrorPanelForUpgradingResearchCenter;
    [SerializeField] GameObject ErrorPanelForUpgradingPlayerBase;
    [SerializeField] GameObject ErrorPanelForBuildingResearchCenter;
    [SerializeField] GameObject ErrorPanelForBuildingCollector;
    [SerializeField] GameObject BuildingArea; // building area object
    [SerializeField] Text addCredits;
    [SerializeField] Text addEnergon;
    [SerializeField] Text creditsLeft;
    [SerializeField] Text energonLeft;
    [SerializeField] Text additionalWorkerText;
    [SerializeField] int credits;
    [SerializeField] int energon;
    [SerializeField] int maxPlayerTroopsAmount;
    [SerializeField] GameObject worker; // GameObject variable which holds worker object
    [SerializeField] Button createBuilding;
    [SerializeField] Button addcreditstobase;
    [SerializeField] Text currentPlayerTroopsAmount;
    bool resourceAmountScreenState;// screen state variable
    bool resourceAmountScreenStateForUpgrade;
    bool errorStateForResearchCenter;
    bool errorStateForPlayerBase;
    bool errorStateToBuildStructure;
    bool errorStateForPlayerCollector;
    bool buildingArea;
    bool createWorkerAmountState;
    [SerializeField] Button createEnergonCollector; // Button type variables needed to save buttons
    [SerializeField] Button createBarrackBuilding;
    [SerializeField] Button createTurret;
    [SerializeField] Button createResearchCentre;
    [SerializeField] Button createTroopsResearchCenter;
    [SerializeField] Button createPlayerWalls;
    int researchCentreUnitAmount; // variable for building research center amount 
    int troopsResearchCenterAmount; // varibale for troops research amount
    int workersAmountOriginal; // variable which holds only free workers amount in the player's baze
    [SerializeField] int conversionAmount; // needed min amount of enrgon to exchange it to credits
    [SerializeField] int existingWorkerAmount; // variable which holds amount of all workers which are spawned on the player base (free and not free)
    [SerializeField] Text existingAndMaxWorkersAmount; // text fieldas for workers
    [SerializeField] int maxWorkerAmountInLevel;
    [SerializeField] Button createworker;
    [SerializeField] int fixedPriceOfOneAdditionalWorker;
    [SerializeField] int minCreditsAmountNeededForUpgrading; // this variables holds player's base upgrading price in credits
    [SerializeField] int minEnergonAmounNeededForUpgrading; //this variables holds player's base upgrading price in energon
    [SerializeField] int baseUpgradeHPAmount;
    [SerializeField] Button upgradeBase;
    [SerializeField] Text upgradeBaseButtonText;
    private HealthOfRegBuilding healthOfTheBase;
    private ResearchLevel researchCenterLevel;
    [SerializeField] int playerBaselevel; // variable, which holds player's base level.
    [SerializeField] int maxPlayerBaseLevel;
    [SerializeField] Text baseLevel; // player's base level text field
    [SerializeField] Text baseHealth;
    [SerializeField] Text creditsLeft1;
    [SerializeField] Text energonLeft1;
    [SerializeField] Text creditsLeft2;
    [SerializeField] Text energonLeft2;
    [SerializeField] Text creditsLeft3;
    [SerializeField] Text energonLeft3;
    [SerializeField] Text addCreditsToBase;
    Vector3 pozitionOfWorker;
    [SerializeField] int playerScoreEarned;
    [SerializeField] int powerNumber;
    [SerializeField] int playerTroopsAmount;
    [Header("Tutorial manager parameters")]
    [SerializeField] bool isTutorialChecked = false;
    private int index = 0; // parameter needed for worker indexing
    string workerIndex;  // string value which holds unique index of each spawned worker
    
    // Start is called before the first frame update
    void Start()
    {
       if (gamecnf.isUserLoggedIn())
       {
          var resourcesInLocalAccount = gamecnf.getGRRecord(gamecnf.getLocalAccountData().userID);
          energon = resourcesInLocalAccount.Energon;
          credits = resourcesInLocalAccount.Credits;

          gamecnf.updateOrCreateGRRecord
            (
               gamecnf.getLocalAccountData().userID,
               100,
               100
            );

       }
       workersAmountOriginal = 0;  // variable which holds only free workers amount in the player's baze
       researchCentreUnitAmount = 0;
       troopsResearchCenterAmount = 0;
       existingWorkerAmount = 0;
       resourceAmountScreenState = false; // initialize boolean variables
       resourceAmountScreenStateForUpgrade = false;
       errorStateForPlayerBase = false;
       errorStateForResearchCenter = false;
       errorStateToBuildStructure = false;
       errorStateForPlayerCollector = false; 
       buildingArea = false; // make the object of biulding area be inactive
       createWorkerAmountState = false;
       createBarrackBuilding.interactable = false; // make button be interactable
       createEnergonCollector.interactable = false;
       createTurret.interactable = false; 
       createResearchCentre.interactable = false;
       createTroopsResearchCenter.interactable = false;
       createPlayerWalls.interactable = false;
       healthOfTheBase = GetComponent<HealthOfRegBuilding>();
       researchCenterLevel = FindObjectOfType<ResearchLevel>();
      // BuildingArea.SetActive(false);
       additionalWorkerText.text = "Additional \n Worker (" + fixedPriceOfOneAdditionalWorker + " credits)";
       addCreditsToBase.text = "Add Credits\n" + "(" + conversionAmount + " energon)";
    }
    // Update is called once per frame
    void Update()
    {
       if (healthOfTheBase.getHealth() <= 0)
       {
         FindObjectOfType<GameSession>().increaseDestroyedPlayerBaseAmount();
         Destroy(gameObject);

       }
        addCredits.text = "Credits left : " + credits; // main baze panel
        addEnergon.text = "Energon left : " + energon; // main baze panel
        creditsLeft.text = "Credits left : " + credits; // defensive structure build
        energonLeft.text = "Energon left : " + energon; // defensive structure build
        creditsLeft1.text = "Credits left : " + credits; // baze upgrade panel
        energonLeft1.text = "Energon left : " + energon; // baze upgrade panel
        creditsLeft2.text = "Credits left : " + credits; // regular structure build
        energonLeft2.text = "Energon left : " + energon; // regular structure build
        creditsLeft3.text = "Credits left : " + credits; // research center build panel
        energonLeft3.text = "Energon left : " + energon; // reserach center build panel
        existingAndMaxWorkersAmount.text = " Workers: " + workersAmountOriginal +"/"+ maxWorkerAmountInLevel; 
        currentPlayerTroopsAmount.text = "Troops: " + playerTroopsAmount + "/" + maxPlayerTroopsAmount;

        if(getWorkersAmountState())
        {
          createworker.interactable = true;
          createWorkerAmountState = false;
        }
        // conditions needed to change specific information in the game
        if(getResourceAmountScreenState()) 
        {
        ResourceAmountScreen.SetActive(true);
        }
        else
        {
        ResourceAmountScreen.SetActive(false); 
        }

        if(getResourceAmountScreenStateForUpgrade())
        {
        ResourcesAmountScreenForUpgrades.SetActive(true);
        }
        else
        {
        ResourcesAmountScreenForUpgrades.SetActive(false);
        }
        
        if(getErrorStateForResearchCenter())
        {
        ErrorPanelForUpgradingResearchCenter.SetActive(true);
        }
        else
        {
        ErrorPanelForUpgradingResearchCenter.SetActive(false);
        }
        
        if(getErrorStateForPlayerBase())
        {
        ErrorPanelForUpgradingPlayerBase.SetActive(true);
        }
        else
        {
        ErrorPanelForUpgradingPlayerBase.SetActive(false);
        }

        if(getErrorStateToBuildStructure())
        {
        ErrorPanelForBuildingResearchCenter.SetActive(true);
        }
        else
        {
        ErrorPanelForBuildingResearchCenter.SetActive(false);
        }

        if(getErrorStateForPlayerCollector())
        {
        ErrorPanelForBuildingCollector.SetActive(true);
        }
        else
        {
        ErrorPanelForBuildingCollector.SetActive(false);
        }
        // condition needed to change the change building area state
        if(getBuildingArea())
        {
        BuildingArea.SetActive(true);
        }
        else
        {
        BuildingArea.SetActive(false);
        }

        if( workersAmountOriginal <=0 ) // condition which check free workers amount in player's baze and makes particular changes in button's behaviour;
        {
        createBarrackBuilding.interactable = false;
        createEnergonCollector.interactable = false;
        createTurret.interactable = false;
        createPlayerWalls.interactable = false;
        }
        else
        {
        createEnergonCollector.interactable = true;
        createBarrackBuilding.interactable = true;
        createTurret.interactable = true;
        createPlayerWalls.interactable = true;
        }
        // condition for biulding research center
        if(workersAmountOriginal <= 0 || researchCentreUnitAmount == 1)
        {
          createResearchCentre.interactable = false;
        }
        else
        {
          createResearchCentre.interactable = true;
        }
        // condition for troops research center
        if(workersAmountOriginal <= 0 || troopsResearchCenterAmount == 1)
        {
        createTroopsResearchCenter.interactable = false;
        }
        else
        {
        createTroopsResearchCenter.interactable = true;
        }
        // condiion to ckech if the player has needed zmount of resources to convert them
        if(energon < conversionAmount)
        {
         addcreditstobase.interactable = false;
        }
        // several changes in text fields of buttons//
        upgradeBaseButtonText.text = "Upgrade Base to level " + (playerBaselevel + 1) + " (" + minCreditsAmountNeededForUpgrading + " credits & " + minEnergonAmounNeededForUpgrading + " energon)";
        baseLevel.text = "Base level : " + playerBaselevel;
        baseHealth.text = "Health : " + healthOfTheBase.getHealth() + " / " + healthOfTheBase.getHealthOfStructureOriginal();
          if(playerBaselevel == maxPlayerBaseLevel) // then the player reaches max level then the buttons text changes
           {
             upgradeBaseButtonText.text = "You have reached max level";
           }
    }
    void OnMouseDown()
    {
        Screen.SetActive(true);
        addCredits.text = "Credits left : " + credits;  
        addEnergon.text = "Energon left : " + energon;  
    }
    //workers spawning method
    public void Spawning()
    {      
    GameObject spawnedWorker = Instantiate(worker,pozitionOfWorker,Quaternion.identity); 
    workersAmountOriginal++; 
    existingWorkerAmount++; 
    workerIndex = "W" + index;  
    //Debug.Log(workerIndex);
    spawnedWorker.GetComponent<Worker>().setWorkerIndex(workerIndex);
    //Debug.Log(spawnedWorker.GetComponent<Worker>().getWorkerIndex());
    index ++;
    var playerScorePoints = FindObjectOfType<GameSession>();
        if(playerScorePoints != null)
        {
          playerScorePoints.addPlayerWorkersAmount(1);
        }

        if(existingWorkerAmount >= maxWorkerAmountInLevel)
        {
          createworker.interactable = false;
        }
    }
    
    public void addAdditionalWorker() 
    {
      if(credits < fixedPriceOfOneAdditionalWorker)
      {
      resourceAmountScreenState = true;
      return;   
      }
      credits -= fixedPriceOfOneAdditionalWorker;
      maxWorkerAmountInLevel++;
       
      if(existingWorkerAmount < maxWorkerAmountInLevel)
      {
        createworker.interactable = true;
      }
     
      } 

    public void addcreditsAmountToBase() 
    {
      if(energon < conversionAmount)
      {
        return;
      }
      credits+=Random.Range(30,40);
      energon-=conversionAmount; 
    } 

    public void upgradeBaseMethod()
    {
      if(playerBaselevel == maxPlayerBaseLevel) // kai zaidejas pasiekia max leveli tai mygtukas pakeicia teksta ir po paspaudimo escapina
      {
        return;
      }
      if(credits < minCreditsAmountNeededForUpgrading || energon < minEnergonAmounNeededForUpgrading) // per update tikrinsiu ar pakanka resursu tobulinti baze. Kai neuztenks resursu tai uzdisabliname
      {
        setResourceAMountScreenStateForUpgrade(true);  
        return;
      }
      playerBaselevel++;
      energon -= minEnergonAmounNeededForUpgrading; // numinusuojame resursus uz viena updata.
      credits -= minCreditsAmountNeededForUpgrading; // numinusuojame resursus uz viena update.
      minCreditsAmountNeededForUpgrading += 10; // kas kita leveli upgradinant reikes vis daugiau resursu.
      minEnergonAmounNeededForUpgrading += 20; // kas kita leveli upgradinant reikes vis daugiau resursu.
      healthOfTheBase.setHealthOfStructureOriginal(healthOfTheBase.getHealthOfStructureOriginal() + baseUpgradeHPAmount);
      maxPlayerTroopsAmount += 10;
      baseUpgradeHPAmount += (int)Mathf.Pow(2,powerNumber); // uzsetiname nauja reiksme HP, siaip cia galima parasyti koki desni pagal kuri dides tas skaicius, pavyzdziui prideti skaiciu kuris bus padaugintas is bazes levelio
      powerNumber++;
      var playerScorePoints = FindObjectOfType<GameSession>();
      if(playerScorePoints != null)
      {
        playerScorePoints.AddPlayerScorePoints(playerScoreEarned);
        playerScoreEarned += 5;
      }
      
    }
    // UI geteris/seteris workeriams // kai vienas zusta kai buvo max skaicius mygtukas vel turi bui ijungtas su galimybe vel spawninti.
    public bool getWorkersAmountState()
    {
      return createWorkerAmountState;
    }
    public void setWorkersAmountState(bool createWorkerStatus)
    {
      createWorkerAmountState = createWorkerStatus;
    }
    // UI screeno geteris/seteris jei neuztenka resusrsu nupirkti game objekto
    public bool getResourceAmountScreenState()
    {
      return resourceAmountScreenState;
    }
    public void setResourceAMountScreenState(bool screenStatus)
    {
      resourceAmountScreenState = screenStatus;
    }
    // UI geteris/seteris jei neuztenka resursu uzupgardinti game objekto
    public bool getResourceAmountScreenStateForUpgrade()
    {
      return resourceAmountScreenStateForUpgrade;
    }
    public void setResourceAMountScreenStateForUpgrade(bool screenStatus)
    {
      resourceAmountScreenStateForUpgrade = screenStatus;
    }
    // UI geteris/seteris jei reikia uztobulinti research centra
    public bool getErrorStateForResearchCenter()
    {
      return errorStateForResearchCenter;
    }
    public void setErrorStateForResearchCenter(bool screenStatus)
    {
      errorStateForResearchCenter = screenStatus;
    }
    // UI geteris/seteris jei reikia uztobulinti zaidiejo baze
    public bool getErrorStateForPlayerBase()
    {
      return errorStateForPlayerBase;
    }
    public void setErrorStateForPlayerBase(bool screenStatus)
    {
      errorStateForPlayerBase = screenStatus;
    }
    // UI geteris/seteris norint pastatyti resursu rinkeja
    public bool getErrorStateForPlayerCollector()
    {
      return errorStateForPlayerCollector;
    }
    public void setErrorStateForPlayerCollector(bool screenStatus)
    {
      errorStateForPlayerCollector = screenStatus;
    }
    // UI geteris/seteris sukurti tam ikra game strucuture
    public bool getErrorStateToBuildStructure()
    {
      return errorStateToBuildStructure;
    }
    public void setErrorStateToBuildStructure(bool screenStatus)
    {
      errorStateToBuildStructure = screenStatus;
    }
    // geteris/seteris building area nustatytis
    public bool getBuildingArea()
    {
      return buildingArea;
    }
    public void setBuildingArea(bool status)
    {
      buildingArea = status;
    }
    // metodas skirtas uzdaryti atsidarusius langus
    public void closeWindow()
    {
      resourceAmountScreenState = false;
      resourceAmountScreenStateForUpgrade = false;
      errorStateForResearchCenter = false;
      errorStateForPlayerBase = false;
      errorStateToBuildStructure = false;
      errorStateForPlayerCollector = false;
    }
    public int getCreditsAmount()
    {
      return credits;
    }
    public void setCreditsAmount(int creditsAmount)
    {
      credits = creditsAmount;
    }
    public int getEnergonAmount()
    {
      return energon;
    }
    public void setEnergonAmount(int energonAmount)
    {
      energon = energonAmount;
    }
    // geteriai nustatyti laisvu workeriu skaiciu zaidejo bazeje
    public int getworkersAmount()
    {
      return workersAmountOriginal;
    }
    public void setworkersAmount(int workersAmount)
    {
      workersAmountOriginal = workersAmount;
    }
    // kiek yra pasatatyta research center zaidejo bazeje
    public int getResearchCentreUnitAmount()
    {
      return researchCentreUnitAmount;
    }
    public void setResearchCentreUnitAmount(int newUnitAmount)
    {
      researchCentreUnitAmount = newUnitAmount;
    }
    // kiek yra pastatyta troopsu research centru
    public int getTroopsResearchCentreUnitAmount()
    {
      return troopsResearchCenterAmount;
    }
    public void setTroopsResearchCentreUnitAmount(int newUnitAmount)
    {
      troopsResearchCenterAmount = newUnitAmount;
    }
    // zaidejo bazes lygis
    public int getPlayerBaseLevel()
    {
      return playerBaselevel;
    }
    // kiek zaidejas siuo metu turi workeriu bazeje, nepriklausomai ar jie laisvi ar ne
    public int getExistingworkersAmount()
    {
      return existingWorkerAmount;
    }
    public void setExistingworkersAmount(int workersAmount2)
    {
      existingWorkerAmount = workersAmount2;
    }
    public int getPlayerMaxTroopsAmount() // max troops amount in current playerbase
    {
      return maxPlayerTroopsAmount;
    }
    public void setPlayerMaxTroopsAmount(int troopsAmount)
    {
      maxPlayerTroopsAmount = troopsAmount;
    }
    public void setPosition(Vector3 poz)
    {
        pozitionOfWorker = poz;
    }
    public int getMaxWorkerAmountInLevel()
    {
      return maxWorkerAmountInLevel;
    }
    public void addPlayerTroopsAmount(int amount)
    {
      playerTroopsAmount += amount;
    }
    public int getPlayerTroopsAmount()
    {
      return playerTroopsAmount;
    } 
}
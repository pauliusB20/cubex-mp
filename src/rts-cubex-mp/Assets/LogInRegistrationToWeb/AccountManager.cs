using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class AccountManager : MonoBehaviour
{
    //NOTE: Fix button bug 
    //NOTE: Fix login tracker + item button creation bug
    //NOTE: Addd item data retrieval
    [Header("Net cnf")]
    [SerializeField] CMRequestUrl cmurl;
    [Header("UI Configuration parameters")]
    [SerializeField] string marketURL;
    [SerializeField] GameObject registerBtn;
    [SerializeField] GameObject accountBtn;
    [SerializeField] string btnUIInvTag;
    [SerializeField] float logOutDelay = 5f;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject inventoryScreen;
    [SerializeField] GameObject MainMarketScreen;
    [Header("Configuration parameters")]  
    [SerializeField] GameStorageConf gamecnf;
    [SerializeField] string resUrl = "http://193.219.91.103:2096/resData";
    [SerializeField] Text userDetails;
    [SerializeField] Text userResourcesText;
    [SerializeField] Text nemAccountDataText;
    [SerializeField] string defaultText = "Not loaded 124";
    [SerializeField] int currentEnergonAmount;
    [SerializeField] int currentCreditsAmount;
    [SerializeField] NetworkUserLogInManager uNetLogInStatus;
    [SerializeField] List<string> resTypes = new List<string>();
    [Header("NeM configuration parameters")]
    [SerializeField] string tokenResURL;
    [SerializeField] List<string> userTokiens = new List<string>();
    [SerializeField] bool areTokiensReceived = false;
    [SerializeField] bool userTokenFound = false;
    [SerializeField] bool dataDisplayed = false;
    [SerializeField] NetworkUserLogInManager userNet;
    bool isUserLoggedOut = false;
    bool areResourcesFound = false;
  
    public bool areResourcesReceived {get; set;} 
    private List<string> receiveData = new List<string>(); 
   
    enum ResourcesTable {ResID = 0, EAmountPos = 1, CAmountPos = 2};
    enum ResourcesType {energon, credits};
    void Start()
    {
        marketURL = cmurl.MarketPageURL;
        resUrl = cmurl.ResourcesDataURL;
        tokenResURL = cmurl.TokenDataURL;        
    }   
    void Update()
    {
        
            accountBtn.SetActive(gamecnf.isUserLoggedIn());//Updating view accoutn btn every state
            registerBtn.SetActive(!gamecnf.isUserLoggedIn());
    } 

    public void logOut()
    {
        //Setting the login form text boxes interactivity
        
       // var inventorySystem = FindObjectOfType<AccInventoryManager>();
        // if (inventorySystem.InventoryBtns.Length > 0)
        // {
        //     Debug.Log("Account: Cleaning up inventory!");
        //     foreach (var item in inventorySystem.InventoryBtns)
        //     {
        //         item.GetComponent<ItemDetailsInfoBtn>().destroyBtn();
        //         var items = new List (inventorySystem.InventoryBtns);
        //         items.InventoryBtns.RemoveAt(items.IndexOf(item));
                
        //     } 
        // }
        //Settign logout status
        StartCoroutine(logoutSession());
        
       
    }
    IEnumerator logoutSession()
    {
        userNet.AccountDataLoaded = false;
        gamecnf.markUserStatus(false);
        loadingScreen.SetActive(true);  
        inventoryScreen.SetActive(false);      
        userNet.UserNameTBox.interactable  = true;
        userNet.PasswordTBox.interactable  = true;
        var accInventory = FindObjectOfType<AccInventoryManager>();
        accInventory.resetInvButtons();  
        //Cleaning the inventory   
        var inventoryBtns = accInventory.InventoryBtns;
        if (inventoryBtns.Length > 0)
        {
            foreach (var item in inventoryBtns)
            {
                Destroy(item);
            }
            Debug.Log("Inventory cleaned: " + (inventoryBtns.Length <= 0));
        }
        else
        {
            Debug.Log("No inventory buttons found! Skipping this process!");
        }
        // Recording the logout time
        uNetLogInStatus.updateLogHistory(1);
        // Once generated the logout time, sending history data
        uNetLogInStatus.sendDataHistoryToWeb();
        yield return new WaitForSeconds(logOutDelay);        
        isUserLoggedOut = !isUserLoggedOut;
        uNetLogInStatus.updateLogInStatus();
        uNetLogInStatus.setLogOutStatus();
        clearDetails();
    }
    public void clearDetails()
    {   
        MainMarketScreen.SetActive(true);
        loadingScreen.SetActive(false);
        userDetails.text = "Username:" + defaultText + "\nPassword:" + defaultText;
        
        accountBtn.SetActive(false);
        FindObjectOfType<AccInventoryManager>().InvAccessBtnFromLogIn.SetActive(false);
        // userNet.getUserNameField().text = "";
        // userNet.getUserPasswordField().text = "";
    }

    public void gatherResourcesInformation()
    {
        if (gamecnf.getLocalAccountData().energon == 0 && gamecnf.getLocalAccountData().credits == 0)
            StartCoroutine(retreiveResourcesData());

    }

    IEnumerator retreiveResourcesData()
    {
        Debug.Log("Starting to connect to the server..");
        using (UnityWebRequest www = UnityWebRequest.Get(resUrl))
        {
            yield return www.SendWebRequest();

            string[] pages = resUrl.Split('/');
            int page = pages.Length - 1;

            if (www.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + www.error);
            }
            else
            {
                //Debug.Log(pages[page] + ":\nReceived: " + www.downloadHandler.text);
                string tempReceivedData = www.downloadHandler.text;
                //Debug.Log("Data without split:" + tempReceivedData);
                string[] tempDataArray = Regex.Split(tempReceivedData, "<br/>");
                foreach (var r in tempDataArray)
                {
                    Debug.Log(r);
                    receiveData.Add(r);
                   
                }
                 areResourcesReceived = true; //For Debugging
                 if (receiveData.Count > 0)
                {
                    foreach (var item in receiveData)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var temp = item.Split(' ');               
                            if (int.Parse(temp[(int)ResourcesTable.ResID]) == userNet.getCurrentCurrentUserID())
                            {                    
                                currentEnergonAmount = int.Parse(temp[(int)ResourcesTable.EAmountPos]);
                                currentCreditsAmount = int.Parse(temp[(int)ResourcesTable.CAmountPos]);
                            
                                
                                // currentEnergonAmount = int.Parse(temp[(int)ResType.energonAmount]);
                                // currentCreditsAmount = int.Parse(temp[(int)ResType.creditsAmount]);
                                //Assigning local resources to an account

                                gamecnf.assignGameResourcesToLocAccount(currentEnergonAmount, currentCreditsAmount);
                                //Add NEM data to configuration file
                            
                                areResourcesFound = currentEnergonAmount > 0 && currentCreditsAmount > 0;     
                                
                                // FindObjectOfType<PurchaseManager>().setStartingResourcesAmounts();   
                                if (areResourcesFound) {
                                    Debug.Log("Game resources aquired!");
                                    //uNetLogInStatus.TTracer.addCompletionValue(uNetLogInStatus.TaskCompletionValueOffset);   
                                    displayUserDataInInventory(); //Displaying data in user account inventory
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public void displayUserDataInInventory()
    {
        if (gamecnf.isUserLoggedIn())
        {
                userDetails.text = "Username: " + gamecnf.getLocalAccountData().nickname + 
                "\nStatus:" + (gamecnf.isUserLoggedIn() ? "online" : "offline");
               
                //Gathering NEM data regarding current tokien balance
            //  collectLoggedInUserBalance();
            
            
             //Collecting game resources and NEM currencies
               
                 userResourcesText.text = "Resources on web account:\nCurrent energon amount: " + gamecnf.getLocalAccountData().energon
                       + "\nCredits amount: " + gamecnf.getLocalAccountData().credits;
              
               if (gamecnf.GameLocalAccRes.Count > 0)
               { 
                   foreach (var item in gamecnf.GameLocalAccRes)
                   {
                        if (item.Userid == gamecnf.getLocalAccountData().userID)
                        {
                            userResourcesText.text += ("\nResources in game acount:\nEnergon:" + item.Energon
                             + "\nCredits: " + item.Credits); 
                             break;
                        }
                   }
               } 
               else
               {
                    userResourcesText.text += ("\nResources in game account:\nEnergon: 0\nCredits: 0\n");
               }   
               if (gamecnf.Nemdata.IsDataSaved) //Check if data is saved in game nem data record
                {
                    nemAccountDataText.text = "NEM wallet data:\nAddress: " + gamecnf.Nemdata.WalletAddress +
                                            "\nPrivateKey: " + gamecnf.Nemdata.PrivateKey + 
                                            "\nPublicKey: " + gamecnf.Nemdata.PublicKey;
                    if (gamecnf.Nemdata.TokienBalance > 0)
                    {
                        nemAccountDataText.text += "\nBalance: " + gamecnf.Nemdata.TokienBalance + "CubeCoin";
                    }
                    
                }
                 
          }
    }
    public void collectLoggedInUserBalance()
    {
        if (!areTokiensReceived)
        {
             areTokiensReceived = true;
             StartCoroutine(collectAllNEMTokienData());
        }
        else
        {
            if (userTokiens.Count > 0 && !userTokenFound)
            {
                foreach(var tokien in userTokiens)
                {
                    if (!string.IsNullOrEmpty(tokien))
                    {
                        var temp = tokien.Split(' ');
                        int tempcheck;
                        if (int.TryParse(temp[0], out tempcheck))//Later fix bug with balance
                        {
                            if (int.Parse(temp[0]) == gamecnf.getLocalAccountData().userID)
                            {
                                gamecnf.assignUserBalance(int.Parse(temp[1]));
                                userTokenFound = true;
                              //  uNetLogInStatus.TTracer.addCompletionValue(uNetLogInStatus.TaskCompletionValueOffset);
                                return;
                            }
                        }
                    }
                   
                    
                }
            }
        }
    }
    IEnumerator collectAllNEMTokienData()
    {
         Debug.Log("Starting to connect to the server for getting balance..");
        using (UnityWebRequest www = UnityWebRequest.Get(tokenResURL))
        {
            yield return www.SendWebRequest();

            string[] pages = resUrl.Split('/');
            int page = pages.Length - 1;

            if (www.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + www.error);
            }
            else
            {
                //Debug.Log(pages[page] + ":\nReceived: " + www.downloadHandler.text);
                string tempReceivedData = www.downloadHandler.text;
                //Debug.Log("Data without split:" + tempReceivedData);
                string[] tempDataArray = Regex.Split(tempReceivedData, "<br/>");
                foreach (var r in tempDataArray)
                {
                    Debug.Log(r);
                    userTokiens.Add(r);
                  
                }
            }
        }
    }
    public void openTheMarketInTheDefaultBrowser()
    {
        if (marketURL == "") return;
        
        Application.OpenURL(marketURL);
    }
    public void setEnergonAmount(int amount)
    {
        currentEnergonAmount = amount;
    }
    public int getCurrentUsersEnergonAmount()
    {
        return currentEnergonAmount;
    }
    public void setCreditsAmount(int amount)
    {
        currentCreditsAmount = amount;
    }
    public int getCurrentUsersCreditsAmount()
    {
        return currentCreditsAmount;
    }
    
}

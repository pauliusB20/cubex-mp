using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using UnityEngine.Networking;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using CryptSharp;
public class NetworkUserLogInManager : MonoBehaviour
{
    [Header("Configuration parameters")]
    [SerializeField] CMRequestUrl cmurl;
    [SerializeField] string userDatabaseReceiveUrl = "https://example.com";
    [SerializeField] string statusUpdateUrl = "https://example.com";
    [SerializeField] string updateHistoryUrl = "https://example.com";
    [SerializeField] string currentLogedInUserName;
    [SerializeField] string currentLogedInUserPassword;
    [SerializeField] int CURRENT_USER_ID;
    [SerializeField] InputField userNameTBox;
    [SerializeField] InputField passwordTBox;
    [SerializeField] bool isUserLogedIn;
    [SerializeField] bool isUserLogedOut;
    [SerializeField] GameObject succesPanel;
    [SerializeField] GameObject failedLogInWindow;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject logInPanel;
    [SerializeField] GameObject AccountManager;
    [SerializeField] float inventoryRefreshDelay = 15f;
    [SerializeField] string logInTime;
    [SerializeField] string logOutTime;
    [SerializeField] string logIp;
    [SerializeField] bool isHistoryAltered = false;
    [SerializeField] bool dataHistorySent = false;
    [SerializeField] bool accountDataLoaded = false;
    [SerializeField] float loadingDelay = 5f;
    [Header("UI configurations")]
    [SerializeField] string itemTag = "itemBtnInv";
    //Inventory manager
    [SerializeField] AccInventoryManager inventoryManager;
    [SerializeField] GameStorageConf logItemMgr; //Scriptable object asset file, which holds game asset info in game
    [Header("NEM Block chain wallet data")]
    [SerializeField] string wallet_address;
    [SerializeField] string private_key;
    [SerializeField] string public_key;
    [Header("Task Loading Conf.")]
    [SerializeField] GameObject loadingAccountText;   

    public InputField UserNameTBox { get{return userNameTBox;} }
    public InputField PasswordTBox { get{return passwordTBox;} }
    // public TaskTracer TTracer { get {return ttracer; }} //Tracer for other backend tasks
    // public float TaskCompletionValueOffset { get { return taskCompletionValueOffset; } set { taskCompletionValueOffset = value; }}
    public string Wallet_address { get {return wallet_address;} set {wallet_address = value;}}
    public string Private_key { get { return private_key; } set{ private_key = value; }}
    public string Public_key { get{ return public_key; } set{ public_key = value; }}
    public bool AccountDataLoaded {get {return accountDataLoaded;} set{accountDataLoaded = value;}}
    bool areNemdataAquired = false;
    private string tempReceivedData;
    private List<string> receiveData = new List<string>(); 
    private int hIndex = 0;
    void Start()
    {
        userDatabaseReceiveUrl = cmurl.UserDBDataURL;
        statusUpdateUrl = cmurl.StatusUpdateURL;
        updateHistoryUrl = cmurl.HistoryUpdateURL;        
        Debug.Log(updateHistoryUrl);
        isUserLogedIn = false;
       // createLogInSession();
       
    }
    private void Update()
    {
    //    if (!accountDataLoaded && logItemMgr.isUserLoggedIn()) //Loading inventory and ather data
    //    {
    //        StartCoroutine(createLocalUserInventory());
    //        accountDataLoaded = true;
    //    }
        //History sending code
        // if (!logItemMgr.isUserLoggedIn() && (!isHistoryAltered && !dataHistorySent ))
        // {
        //     Debug.Log("Sending history data..");
        //     StartCoroutine(delay(1000));
        //     sendDataHistoryToWeb();
        //     dataHistorySent = true;
        // }
    }
    IEnumerator delay(int delay)
    {
        yield return new WaitForSeconds(delay);
    }
    public void checkIfReadyToLogIn() //Checking if user nickname and password is inputted
    {
        var areAllFieldFull = (userNameTBox.text != null && passwordTBox.text != null);
        if (!areAllFieldFull)
        {
            loadingAccountText.SetActive(false);
            logInPanel.SetActive(false);
            failedLogInWindow.SetActive(true);
            Debug.Log("Please Input your Nickname or email and password");
            return;
        }
        else
        {
           
           StartCoroutine(checkForUserInDB());
        }
    }
    IEnumerator checkForUserInDB()
    {
       loadingAccountText.SetActive(true);
       logInPanel.SetActive(false);
       yield return new WaitForSeconds(loadingDelay);
       startCheckingUserTable();
    }
    public void startCheckingUserTable() //Add progress bars later
    {
       
        //Check if list is empty or not...
        if (receiveData == null) return;
      //  ttracer.startTaskTracking();
         //Setting interactivity to false
       // loadingAccountText.SetActive(true);
        userNameTBox.interactable  = false;
        passwordTBox.interactable  = false;
        foreach(var r in receiveData) //Optimize login
        {
                if (!string.IsNullOrEmpty(r))
                {
                   
                    //Checking if user in the user's table is an admin or not           
                    if ((r.Contains(userNameTBox.text) && !r.Contains("admin")) && (userNameTBox.text != "" && r.Contains("offline")))
                    {
                        //Checking the password if username is found
                        var passswordForchecking = r.Split('-')[2];
                        var isPasswordValid = Crypter.CheckPassword(passwordTBox.text, passswordForchecking); //Checks if password exists in the current user hash
                        if (!isPasswordValid) break;

                       
                        AccountManager.SetActive(true);
                        var userData = r.Split('-');
                        //Change indexes later
                        currentLogedInUserName = userNameTBox.text;
                        currentLogedInUserPassword = passwordTBox.text;
                        CURRENT_USER_ID = int.Parse(userData[0]);  
                        isUserLogedIn = true; 
                        logInPanel.SetActive(!isUserLogedIn); //Closing login window
                        logItemMgr.markUserStatus(isUserLogedIn);
                        Debug.Log(isUserLogedIn ? "Success user is logged in into the in game market account." : "Login failed! ERROR..");
                       
                        
                        isHistoryAltered = false;
                        dataHistorySent = false; //For controlling history sending state..
                        hIndex = 0;//for history sending..
                        //Send data to login history table...
                        updateLogHistory(0);
                        //Local accoutn creaton
                        logItemMgr.createLocalUserAccount(
                                                        CURRENT_USER_ID,
                                                        userData[1], 
                                                        userData[5], 
                                                        currentLogedInUserPassword
                                                        );
                         updateLogInStatus(); //Updating account activity status
                    // ttracer.addCompletionValue(logItemMgr.getLocalAccountData().userID > 0 ? taskCompletionValueOffset : 0f);
                       
                    
                    
                        //------------Gathering NEM data-------------------
                        Wallet_address = userData[6];
                        Private_key = userData[7];
                        Public_key = userData[8];
                        logItemMgr.assignNemData(Wallet_address, Private_key, Public_key); 
                        Debug.Log("NEM data: " + (logItemMgr.Nemdata.IsDataSaved ? "aquired" : "not aquired"));
                       
                    //    ttracer.addCompletionValue(logItemMgr.Nemdata.IsDataSaved ? taskCompletionValueOffset : 0f);
                        //--------------------------------
                        StartCoroutine(createLocalUserInventory()); //Refreshink inventory
                        
                        
                        return;
                    }                   
                }
        }
        logInPanel.SetActive(false);
        failedLogInWindow.SetActive(true);
        Debug.Log("ERROR user does not exists on the server!");
    }
    IEnumerator createLocalUserInventory()
    {
         loadingAccountText.SetActive(true);
         var accmgr = AccountManager.GetComponent<AccountManager>();
         accmgr.collectLoggedInUserBalance();
         accmgr.gatherResourcesInformation();
         accmgr.displayUserDataInInventory();
         //Refesh inventory
         inventoryManager.collectItemDataAndUpdateConfigurationFile();
         //inventoryManager.RefreshInventory();
         inventoryManager.assignCurrentInvButtons();
         //Refesh resources in game inventory
         Debug.Log(accmgr.areResourcesReceived ? "Success game resources are received!" : "Failed! Receiving user game resources");
         yield return new WaitForSeconds(inventoryRefreshDelay);
         loadingAccountText.SetActive(false);
         succesPanel.SetActive(true); //When inventory is refreshed, display the success message
    }
    public void updateLogHistory(int type)
    { 
    
    //    bool isRecorded = false;
      
      if (type <= 0)
      {
        try
        {
            // Setting the login time
            logInTime = generateDate();
            logIp = LocalIPAddress();
            logItemMgr.getLocalAccountData().LoginTime = generateDate();
            logItemMgr.getLocalAccountData().IP =  LocalIPAddress();
            // isRecorded = !isRecorded;
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
      }
      else
      {
        try
        {
            // Setting the logout time
            logOutTime = generateDate();
            logItemMgr.getLocalAccountData().LogoutTime = generateDate();
            // logIp = LocalIPAddress();
            // isRecorded = !isRecorded;
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
      }
    }
    public void sendDataHistoryToWeb()
    {
        StartCoroutine(sendDataToHistoryTable());
    }
    IEnumerator sendDataToHistoryTable()
    {       
        bool isDataSent = false;
        WWWForm form = new WWWForm();
        // Debug.Log(CURRENT_USER_ID);
        form.AddField("user_id", logItemMgr.getLocalAccountData().userID);
        form.AddField("login_time", logItemMgr.getLocalAccountData().LoginTime);
        form.AddField("logout_time", logItemMgr.getLocalAccountData().LogoutTime);
		form.AddField("ip", logItemMgr.getLocalAccountData().IP);	
         using (UnityWebRequest www = UnityWebRequest.Post(updateHistoryUrl, form))
        {
            //www.SetRequestHeader ("cookie", "csrftoken=" + csrfCookie);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {                                  
                isDataSent = !isDataSent;                    
                isHistoryAltered = isDataSent;
            }
             Debug.Log(
                 isDataSent ? 
                    "User history data updated successfully" : 
                    "ERROR! can't update history :(."
            );  
        }
    }
    public void updateLogInStatus()
    {        
        StartCoroutine(refreshUserStatus());
    }
    public void setLogOutStatus()
    {        
        this.isUserLogedOut = true;    
        logItemMgr.markUserStatus(false);     
        // After history setting, destroying then the local account
        logItemMgr.destroyLocalAccount();
        //Destroing existing buttons, for loggint out. 
        
       
    }
    // Game status in web update
    IEnumerator refreshUserStatus()
    {
        bool isDataAltered = false;
        WWWForm form = new WWWForm();
        // Debug.Log(CURRENT_USER_ID);
        Debug.Log("Status userid:" + logItemMgr.getLocalAccountData().userID +" in game: " + (logItemMgr.isUserLoggedIn() ? "online" : "offline"));
        form.AddField("id", logItemMgr.getLocalAccountData().userID);
		form.AddField("status_in_game", logItemMgr.isUserLoggedIn() ? "online" : "offline");		
        
        using (UnityWebRequest www = UnityWebRequest.Post(statusUpdateUrl, form))
        {
            //www.SetRequestHeader ("cookie", "csrftoken=" + csrfCookie);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {                                  
                isDataAltered = !isDataAltered;                    
               
            }
             Debug.Log(
                 isDataAltered ? 
                    "User status data updated successfully" : 
                    "ERROR! can't update 789."
            );  
        }
    }
    public void createLogInSession()
    {
        if (!logItemMgr.isUserLoggedIn())
        {
            loadingAccountText.SetActive(true);
            StartCoroutine(startLogInSession());
        }      
    }
    IEnumerator startLogInSession()
    {
        Debug.Log("Starting to connect to the server..");
        using (UnityWebRequest www = UnityWebRequest.Get(userDatabaseReceiveUrl))
        {
            yield return www.SendWebRequest();

            string[] pages = userDatabaseReceiveUrl.Split('/');
            int page = pages.Length - 1;

            if (www.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + www.error);
                failedLogInWindow.SetActive(true);
                loadingAccountText.SetActive(false);
            }
            else
            {
                //Debug.Log(pages[page] + ":\nReceived: " + www.downloadHandler.text);
                tempReceivedData = www.downloadHandler.text;
                //Debug.Log("Data without split:" + tempReceivedData);
                string[] tempDataArray = Regex.Split(tempReceivedData, "<br/>");
                foreach (var r in tempDataArray)
                {
                    Debug.Log(r);
                    receiveData.Add(r);
                }
                //Opens login form
                loadingAccountText.SetActive(false);
                logInPanel.SetActive(true);
            }
        }
    }
    public string generateDate()
    {
      return DateTime.Now.ToString("g");
    }
    public string LocalIPAddress()
    {
             IPHostEntry host;
             string localIP = "0.0.0.0";
             host = Dns.GetHostEntry(Dns.GetHostName());
             foreach (IPAddress ip in host.AddressList)
             {
                 if (ip.AddressFamily == AddressFamily.InterNetwork)
                 {
                     localIP = ip.ToString();
                     break;
                 }
             }
             return localIP;
      }
   
    public string getUserName()
    {
        return currentLogedInUserName;
    }
    public string getPassword()
    {
        return currentLogedInUserPassword;
    }
    public bool UserLoggedIn()
    {
        return isUserLogedIn;
    }
    public InputField getUserNameField()
    {
        return userNameTBox;
    }
     public InputField getUserPasswordField()
    {
        return passwordTBox;
    }
    public int getCurrentCurrentUserID()
    {
        return CURRENT_USER_ID;
    }
}

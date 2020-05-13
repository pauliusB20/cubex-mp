using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class AccInventoryManager : MonoBehaviour
{
    [Header("Net cnf.")]
    [SerializeField] CMRequestUrl cmurl;
    [SerializeField] string urlToReceiveAllUserItems = "http://example.com";
    [SerializeField] string urlToReceiveAllGameItemCh = "http://example.com";
    [SerializeField] int u_id = 1;
    [SerializeField] bool areItemsStored = false;
    [SerializeField] string itemsFolderUrl = "Assets/AssetData/";
    [SerializeField] Text itemContentText;
    [SerializeField] bool allItemsArePresented = false;
    [SerializeField] bool tempStorageCreated = false;
    [SerializeField] bool itemsSent = false;
    [SerializeField] bool failedSendingFiles = false;
    [SerializeField] List<string> filePaths = new List<string>();
    [SerializeField] List<GameObject> storedAssetItems = new List<GameObject>();
    [SerializeField] NetworkUserLogInManager logInMg;
    [SerializeField] GameStorageConf gamecnf;
    //User data configuration parameters
    [SerializeField] List<string> userItemsInDB = new List<string>(); //Holds all user data about items
    [SerializeField] List<string> userItemChInDB = new List<string>();
    [SerializeField] List<string> currentUserItemsDetails = new List<string>(); //Holds all logged in user data about items
    [SerializeField] List<string> templateCodes = new List<string>();
    // UI configuration parameters
    [SerializeField] string btnUIInvTag;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject[] inventoryBtns;
    [SerializeField] int btnAmount = 0;
    [SerializeField] bool saveInvBtns = false;
    [SerializeField] GameObject invAccessBtnFromLogIn;
    [SerializeField] GameObject userItemButton;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject menuCanvas; //Panel, which hold all the buttons
    [SerializeField] ItemDetails iteminfo;
    [SerializeField] int buttonIndex = 0; //For counting
    [Header("UI button position managing")]
    [SerializeField] int loggedInUserItemCount = 0;
    [SerializeField] int newbtnPositionX = 0;
    [SerializeField] int btnPosOffsetX = 50;
    [SerializeField] int btnMaxPositionX = 330;

    [SerializeField] int newbtnPositionY = 0;
    [SerializeField] int btnPosOffsetY = 50;
    [SerializeField] int btnMaxPositionY = 330;
    [SerializeField] float refreshInventoryDelay = 5f;
    //Cached variables
    private string oldItemCode = "";
    //Access inventory when loged in button UI
    public int ButtonInvIndex {get {return buttonIndex;} set {buttonIndex = value;}} 
    public int LoggedInUserItemCount {get {return loggedInUserItemCount; } set {loggedInUserItemCount = value;}}
    public GameObject InvAccessBtnFromLogIn {get {return invAccessBtnFromLogIn;} }
    public GameObject[] InventoryBtns { get { return inventoryBtns;} set {inventoryBtns = value;}}
    private void Start() {
        // Assigning url to routes from cnf. file
      urlToReceiveAllUserItems = cmurl.ReceivingGameItemsURL;
      urlToReceiveAllGameItemCh = cmurl.ReceivingGameCharURL;
    }
    private void Update()
    {
        //Setting the currently createed buttons
        assignCurrentInvButtons();
        // if ((allItemsArePresented && !itemsSent) && !failedSendingFiles)
        //     SendDataToWeb();
        // if (!allItemsArePresented)
        //     RefreshInventory();
        if (userItemsInDB.Count > 0 || (userItemsInDB.Count > 0 && userItemChInDB.Count > 0))
        {
           invAccessBtnFromLogIn.SetActive(true); 
        }
    }

    public void RefreshInventory()
    {   
        // var inventoryButtons = GameObject.FindGameObjectsWithTag(btnUIInvTag);
        // if (inventoryButtons.Length > 0 &&  areButtonsCreated())
        // {
        //     foreach(var item in inventoryButtons)
        //         Destroy(item);
        // }
        //Display items in the inventory...    
        Debug.Log("Starting inventory refresh status: " + (gamecnf.isUserLoggedIn() && gamecnf.getGameItemList().Count > 0));
        if (gamecnf.isUserLoggedIn() && gamecnf.getGameItemList().Count > 0) 
        {
            loggedInUserItemCount = gamecnf.getUserItemCount(gamecnf.getLocalAccountData().userID);
            var currentItemList = gamecnf.getGameItemList();
            Debug.Log("Collecting data from the file..");
           
                foreach (var item in currentItemList)
                {
                //   foreach(var code in iteminfo.getItemCodes())
                //   {
                    //   if ((item.itemCode == code && code != oldItemCode) && item.UserID == gamecnf.getLocalAccountData().userID)
                    //   {
                      if (item.UserID == gamecnf.getLocalAccountData().userID)
                      { 
                            if (loggedInUserItemCount <= buttonIndex) return;
                            //Create item UI button
                           // oldItemCode = code;
                            GameObject spawnedBtn = Instantiate(userItemButton, Vector2.zero, userItemButton.transform.rotation);
                            spawnedBtn.SetActive(true);
                            GameObject marketMenu = GetChildWithName(menuCanvas, "CubeMarketUI_menus");
                            GameObject userInventory = GetChildWithName(marketMenu, "UsersInventory");
                            GameObject itemPanel = GetChildWithName(userInventory, "ItemPanel");
                            spawnedBtn.transform.SetParent(itemPanel.transform, false);
                            
                            //Checking y position
                            if (newbtnPositionX >= btnMaxPositionX)
                            {
                                newbtnPositionX = 0;
                                var btnCurrentYPos = spawnedBtn.transform.position.x;
                                newbtnPositionY += btnPosOffsetY;
                                btnCurrentYPos += newbtnPositionY;
                                spawnedBtn.transform.position = new Vector2(spawnedBtn.transform.position.x, btnCurrentYPos);
                            }
                            //Postion transformation on x pos
                            var btnCurrentXPos = spawnedBtn.transform.position.x;
                            btnCurrentXPos += newbtnPositionX;
                            spawnedBtn.transform.position = new Vector2(btnCurrentXPos, spawnedBtn.transform.position.y);
                            newbtnPositionX += btnPosOffsetX;
                            //Passing all item details into the button
                            var itemInfoOnButton = spawnedBtn.GetComponent<ItemDetailsInfoBtn>();
                            itemInfoOnButton.setBtnText(GetChildWithName(spawnedBtn, "Text").GetComponent<Text>());
                            itemInfoOnButton.setItemName(item.gameItemName); //Passing item full name
                            itemInfoOnButton.setLevel(item.itemLevel); //Passing the received level
                            itemInfoOnButton.setClassification_name(item.className); //Passing the received class
                            itemInfoOnButton.setItem_type_name(item.gameItemType); //Passing the received type
                            itemInfoOnButton.setItemCharDetails(item.itemChardetails);
                            itemInfoOnButton.setItem_status(item.itemStatus);
                            itemInfoOnButton.setItemCode(item.itemCode);
                            itemInfoOnButton.user_id = item.UserID;
                            buttonIndex++;
                            Debug.Log("Item: " + item.gameItemName + " info button created!");
                            //return;
                      }
                 // }
                } 
                Debug.Log("DONE! Loading inventory"); 
                //Adding completion value to task tracer
               // logInMg.TTracer.addCompletionValue(buttonIndex > 0 ? logInMg.TaskCompletionValueOffset : 0f);   
         }
         else
         {
            return;
         }      
    }
    public void deleteInvButtons()
    {
         var inventoryButtons = GameObject.FindGameObjectsWithTag(btnUIInvTag);
        if (inventoryButtons.Length > 0 &&  areButtonsCreated())
        {
            foreach(var item in inventoryButtons)
                Destroy(item);
        }
    }
    public bool areButtonsCreated()
    {
        return ButtonInvIndex >= gamecnf.getGameItemList().Count;
    }
    public void refreshInvByBtn() //Refresh inventory by button click
    {
       if (gamecnf.isUserLoggedIn())
        {
             if (InventoryBtns.Length > 0)
             {
                StartCoroutine(refreshInventory(InventoryBtns));             
             }
                   
        }
    }
    IEnumerator refreshInventory(GameObject[] InventoryBtns)
    {   
         loadingScreen.SetActive(true);
         inventoryPanel.SetActive(false);
         foreach (var item in InventoryBtns){
                     Destroy(item);
         }
         collectItemDataAndUpdateConfigurationFile();
         yield return new WaitForSeconds(refreshInventoryDelay);
         loadingScreen.SetActive(false);
         inventoryPanel.SetActive(true);
         resetInvButtons();
         RefreshInventory();
    }
    public void assignCurrentInvButtons() //Caching  all created buttons for cleaning later
    {
        if (gamecnf.isUserLoggedIn())
        {
                InventoryBtns = GameObject.FindGameObjectsWithTag(btnUIInvTag);                     
        }
       
    }
    public void updateConfigurationFile()
    {     
        collectItemDataAndUpdateConfigurationFile();      
    }
    // Method for getting a child gameobject
    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        } 
        else
        {
            return null;
        }
    }
   

 
    // public void LoadFromTempStoarage()
    // {
    //     if (storedAssetItems.Count > 0)
    //     {
    //         foreach (var item in storedAssetItems)
    //         {
    //             LoadItemFromAssetsTempData(item);
    //         }
    //     }
    //     else
    //     {
    //         return;
    //     }
    // }
    public void collectItemDataAndUpdateConfigurationFile()
    {
         StartCoroutine(retrieveItemsData());         
    }
    IEnumerator retrieveItemChData()
    {
        Debug.Log("Starting to connect to the server..");
        using (UnityWebRequest www = UnityWebRequest.Get(urlToReceiveAllGameItemCh))
        {
            yield return www.SendWebRequest();

            string[] pages = urlToReceiveAllUserItems.Split('/');
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
                userItemChInDB = new List<string>();
                foreach (var r in tempDataArray)
                {
                    Debug.Log("ITEMS CH Data RECEIVED: "+r);
                    userItemChInDB.Add(r);
                }
                if (userItemChInDB.Count > 0)
                {
                        if (gamecnf.isUserLoggedIn() && (userItemsInDB.Count > 0 && userItemChInDB.Count > 0))
                        {           
                            gamecnf.collectItemsFromDataString(userItemsInDB, userItemChInDB);
                            //Delete not required transactions
                             
                            RefreshInventory(); //After the item collection process, refresh inventory
                        }
                }
            }
        }
    }
    
    IEnumerator retrieveItemsData()
    {
        Debug.Log("Starting to connect to the server..");
        using (UnityWebRequest www = UnityWebRequest.Get(urlToReceiveAllUserItems))
        {
            yield return www.SendWebRequest();

            string[] pages = urlToReceiveAllUserItems.Split('/');
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
                userItemsInDB = new List<string>();
                foreach (var r in tempDataArray)
                {
                    Debug.Log("ITEMS RECEIVED: "+r);
                    userItemsInDB.Add(r);
                }
                if (userItemsInDB.Count > 0)
                {
                    StartCoroutine(retrieveItemChData());//Collecting item chracteristics        
                }
            }
        }
    }
    public void resetInvButtons()
    {
        //Reseting counter for inventory buttons
        ButtonInvIndex = 0;
        LoggedInUserItemCount = 0;
        //Reseting positions
        newbtnPositionX = 0;
        btnPosOffsetX = 100;
        btnMaxPositionX = 330;
        newbtnPositionY = 0;
        btnPosOffsetY = -10;
        btnMaxPositionY = 330;
    }
    
    
}
    


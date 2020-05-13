using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameLevelAccManager : MonoBehaviour
{
   /*
   Veikimas
   1) Patikrinti failo duomenis, jeigu useris prisijunges, rodyti lygyje, jog paskyra yra prijungta
   2) Jeigu paskyra prisijungta paimti duomenis apie zaidimo item'u informacija ir isvesti zaidime
  
  */
    [SerializeField] GameStorageConf gamecnf;
    [Header("Main UI configuration parameters")]
    [SerializeField] GameObject infoAccountPanel;
    [SerializeField] Text accountInfoText;
    [SerializeField] GameObject unitBtn;
    [SerializeField] GameObject menuAccCanvas;
    [SerializeField] int itemOldSize = 0;
    [Header("For building game assets in game configuration var")]
    [SerializeField] GameObject BuildingArea; //Later find alternatives for serializefield
    [SerializeField] Transform demoMarketItemPoint;
    [Header("UI Button configuration")]
    [SerializeField] float newBtnPosX = 0f;
    [SerializeField] float btnOffsetX = 160f;
    [SerializeField] bool userLoggenIn;
     public bool isUserLoggedIn {get {return userLoggenIn;} set{userLoggenIn = value;}}
    void Start()
    {
        //Displaying account information
        if (gamecnf.isUserLoggedIn())
        {   
            isUserLoggedIn = gamecnf.isUserLoggedIn();
            infoAccountPanel.SetActive(true);
            accountInfoText.text = "Cube market\nUser: " + gamecnf.getLocalAccountData().nickname + "\n" + 
                                   "Status: " + (gamecnf.getLocalAccountData().userID > 0 ? "online\n" : "offline\n"); 
        }
    }

    public void updateInventory()
    {        
        if (gamecnf.isUserLoggedIn())
        {            
            //Test out the code more.. 
            var gameItems = gamecnf.getGameItemList();
            if (itemOldSize < gameItems.Count) { //For not adding duplicate buttons
                itemOldSize = gameItems.Count;
                foreach (var item in gameItems)
                {
                    if (item.UserID == gamecnf.getLocalAccountData().userID)
                    {
                        // Code not done yet, needs some fixing...
                        GameObject btnClone = Instantiate(unitBtn, unitBtn.transform.position, unitBtn.transform.rotation);
                        btnClone.SetActive(true);
                        var currentBtnX = btnClone.transform.position.x;
                        currentBtnX += newBtnPosX;
                        btnClone.transform.position = new Vector2(currentBtnX, btnClone.transform.position.y);
                        newBtnPosX += btnOffsetX;
                        //Position transformation...
                        //Goes here..
                        GameObject inventoryPanel = GetChildWithName(menuAccCanvas, "InvPanel");
                        GameObject inventorySubPanel = GetChildWithName(inventoryPanel, "InvPanelSub");            
                        btnClone.transform.parent = inventorySubPanel.transform;
                        //Sending information to spawned item button
                        var btnInfo = btnClone.GetComponent<ItemDetailsInfoBtn>(); 
                        btnInfo.ItemId = item.itemid; //Setting item id  
                        btnInfo.setItemName(item.gameItemName);
                        btnInfo.setLevel(item.itemLevel);
                        btnInfo.setClassification_name(item.className);
                        btnInfo.setItem_type_name(item.gameItemType);
                        btnInfo.setItemCharDetails(item.itemChardetails);
                    }
                }
             }
            else { 
              return; 
            }
        }
    }
    public void createObjectInGame(GameObject obj, string assetName)
    {
        if (obj != null)
        {           
            var existingItemsInInventory = gamecnf.getGameItemList();
            if (existingItemsInInventory.Count > 0)
            {
                GameItem tempObj = null;

                BuildingArea.SetActive(true);
                var buildingAreaConf = BuildingArea.GetComponent<BuildArea>();
                buildingAreaConf.CanBuild = true; //Setting the option for building to true
                //Finding required object information
                foreach (var item in existingItemsInInventory)
                {
                    if (item.gameItemName == assetName)
                    {
                        tempObj = item;
                        break;
                    }
                }
                if (tempObj != null)
                {
                    //Sending object to building area...
                    buildingAreaConf.MarketBuild = true;
                    //Setting item information
                    var itemInfo = obj.GetComponent<ItemDescription>();
                    itemInfo.setAssetName(tempObj.gameItemName);
                    itemInfo.setAssetType(tempObj.gameItemType);  
                    itemInfo.setClass(tempObj.className);
                    //Characteristics later.. :)
                    //DEMO
                    //NOTE: Test out the system and fix bugs!!! First version works :) 
                    GameObject spawnedUnit = Instantiate(obj, demoMarketItemPoint.position, demoMarketItemPoint.rotation);
                    gamecnf.deleteGameItemInfoByName(tempObj.gameItemName);
                    //Cleaning inventory in game
                    var itemBtn = GameObject.FindGameObjectWithTag("iteminvb");
                    if (itemBtn != null)
                    {
                        Destroy(itemBtn);
                    }
                    Debug.Log("Spawning: "+spawnedUnit.name);
                   // buildingAreaConf.receivedMarketObject = obj;
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

        //Kai use mygtukas paspaustas, uždarome itemo info langą

        //Pakeiciam esama mygtuko teksta place item on building area ir padarom building area active
        //uzrakinam kitus mygtukus ir neleidzia useriui kitka statyti
        //paspaudzia ant building area ir sukuria userio itemas
    }
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
    // Update is called once per frame
    void Update()
    {
        //Do nothing...
    }
}

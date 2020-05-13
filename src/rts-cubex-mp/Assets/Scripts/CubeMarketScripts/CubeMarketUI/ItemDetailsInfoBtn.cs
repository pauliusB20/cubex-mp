using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using UnityEngine.UI;

class ItemCharacteristics
{
    // private string characteristicName;
    // private int characteristicsValue, user_id, item_id;
    //Class properties
    public string characteristicName{
        get;
        set;
    }
     public int characteristicsValue{
        get;
        set;
    }  
      public int item_id{
        get;
        set;
    }
    public string printValues()
    {
        return characteristicName + ": " + characteristicsValue;
    }

} 
public class ItemDetailsInfoBtn : MonoBehaviour
{
    [Header("Net cnf.")]
    [SerializeField] CMRequestUrl cmurl;
    [SerializeField] string itemTransactionUrlDel;
    [SerializeField] GameStorageConf gamecnf;
   //UI configuration  
    [SerializeField] Text detailText;
    [SerializeField] GameObject itemInfoUIPanel;
    //Item info
    [SerializeField] string itemName;
    [SerializeField] int level;
    [SerializeField] string classification_name;
    [SerializeField] string item_type_name;   
    [SerializeField] string itemCode;
    [SerializeField] string item_status;
    [SerializeField] int mUser_id;
    [SerializeField] string urlSendItemData;
    [Header("Prefab loading configuration")]
    [SerializeField] string itemsFolderUrl;
    [SerializeField] InGameLevelAccManager mgr;
    [SerializeField] string templateDirectoryPath = "CMarketItemTemplates/Player";
    public int user_id { get{ return mUser_id; } set { mUser_id = value; }}
    public int ItemId {get; set;}
    private List<ItemCharacteristics> itemChars = new List<ItemCharacteristics>(); //List that contains all the user item characteristics
    //Cached data
    [SerializeField] Text btnItemText;
    private string oldBtnText = "";
    private bool isDataSentToDB = false;
    // Update is called once per frame
    void Start()
    {
       itemTransactionUrlDel = cmurl.DeletingItemURL;
       if (itemName != null && btnItemText != null)
       {
            btnItemText.text = itemName; 
            oldBtnText = btnItemText.text;
       }
    }
    void Update()
    {
        // if (itemName != null && btnItemText != null)
        //     btnItemText.text = itemName;
    }
    public void setItemCharDetails(List<string> userItems)
    {
        
        if ((userItems == null || userItems.Count == 0)) return;
        foreach(var c in userItems)
        {
            var itemCh = new ItemCharacteristics();
            if (c.Split(' ')[5] != " ")
            {
                itemCh.characteristicName = c.Split(' ')[5];
                itemChars.Add(itemCh);
            }
        }
        if (itemChars.Count > 0)
        {
            int p = 0;
            foreach (var itemch in itemChars)
            {
                
                int temp_check_num; 
                var line = userItems[p].Split(' '); 
                if (int.TryParse(line[6], out temp_check_num) &&
                    int.TryParse(line[0], out temp_check_num) &&
                    int.TryParse(line[10], out temp_check_num))
                {
                    itemch.characteristicsValue = int.Parse(line[6]);
                    itemch.item_id = int.Parse(line[10]);
                }
                p++;
            }
        }
    }
    public void setItemCharDetails(List<SItemCharacteristics> itemCharDetails)
    {
        if (itemCharDetails == null || itemCharDetails.Count == 0) return;
        foreach (var item in itemCharDetails)
        {
              var itemCh = new ItemCharacteristics();
              itemCh.characteristicName = item.charname;
              itemCh.characteristicsValue = item.charval;
              itemChars.Add(itemCh);
        }
    }
    public void displayItemInfo()
    {
        //NOTE: Display item characteristics/ display their values
        //NOTE: Fix window paostion
        //NOTE: test with multiple items
        detailText.text = "Item name: " + itemName + 
                          "\nLevel: " + level + 
                          "\nClass: " + classification_name + 
                          "\nType: " + item_type_name;
        
        detailText.text += "\n";
        if (itemChars.Count > 0)
        {   
             detailText.text += "\nItem characreristics:\n";    
            foreach (var v in itemChars)
            {
               detailText.text += v.printValues() + "\n"; 
            }
        }
        detailText.text += "Item status: " + ((item_status == "game") ? "In game" : ""); 
    }
    public void sendDTtoItemTable()
    {       
          Debug.Log(itemName == null || 
            item_status != "game" ||
            gamecnf.getLocalAccountData().userID != user_id ?
            "Can't send item to DB" : "Sending item..");
        
         Debug.Log(itemName == null ? "Item name invalid!" : "");
        Debug.Log(item_status != "game" ? "Item status invalid!" : "");
        Debug.Log(gamecnf.getLocalAccountData().userID != user_id ? "Item user invalid!" : "");
       
        if (itemName == null || 
            item_status != "game" ||
            gamecnf.getLocalAccountData().userID != user_id) 
            return;
      
       
        StartCoroutine(SendItemDataDB());
       
    }
    IEnumerator SendItemDataDB()
    {
        // var userNet = new NetworkUserLogInManager();
        WWWForm form = new WWWForm();
        Debug.Log("Storing User ID:" + user_id + " item...");       
        form.AddField("puser_id", user_id);
        Debug.Log(itemName);
		form.AddField("item_name", itemName);
        Debug.Log(itemCode);
        form.AddField("item_code", itemCode);
        Debug.Log(level);
        form.AddField("level", level);
        Debug.Log(item_type_name);
		form.AddField("item_type", item_type_name);   
        Debug.Log("Item classification name: " + classification_name); 
        form.AddField("classification_name", classification_name);
        //Item chracteristics 0 - hp, 1 - dmg, 2 - shield
        form.AddField("HP_val", "HP:"+ itemChars[0].characteristicsValue);
        Debug.Log("Current items hp is " + (itemChars[0].characteristicsValue));
        form.AddField("DMG_val", "DMG:"+itemChars[1].characteristicsValue);
        Debug.Log("Current items dmg is " + itemChars[1].characteristicsValue);
        form.AddField("Shield_val", "SHD:"+ itemChars[2].characteristicsValue);
        Debug.Log("Shield_val" + itemChars[2].characteristicsValue);
         using (UnityWebRequest www = UnityWebRequest.Post(urlSendItemData, form))
        {
            //www.SetRequestHeader ("cookie", "csrftoken=" + csrfCookie);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {                                  
                isDataSentToDB = true;                    
               
            }
             Debug.Log(
                        isDataSentToDB ? 
                            "User Item data sent successfully sent to account!" : 
                            "ERROR! can't sent 781."
                      );
             isDataSentToDB = !isDataSentToDB; //Resetting the value for a next item information send
            itemInfoUIPanel.SetActive(isDataSentToDB);
            gamecnf.deleteGameItemInfoByName(itemName);
            Destroy(gameObject);
            // var getPageData = UnityWebRequest.Get(currentWebURL); 
            // Debug.Log(getPageData.downloadHandler.text);
        }
    }
    //NOTE: Make url dynamically changable later....
    public void FindGameObjectInPrefabsDir()
    {
        itemInfoUIPanel.SetActive(false);
       // btnItemText.text = "Select place"; //Veliau atkomentuoti
        //Find required object in prefab file folders
        //Debug.Log(itemsFolderUrl+"/PlayerMBasePrefabs/PlayerMBaseCharacters/Player"+item_type_name);
        var gameUnit = findGameObject(item_type_name);
        if (gameUnit != null)
        {
            if (ItemId > 0) //Checking if it is not a in game item
                deleteTrashItemRecords(ItemId); 
            mgr.createObjectInGame(gameUnit, itemName); //use item_type_name for locating template, later class name if needed
        }
        else
        {
            Debug.LogError("Didn't find the specified prefab file ERROR!");
        }
    }
    public GameObject findGameObject(string type)
    {
        //var path = dir + ".prefab";
        Debug.Log("Trying to load a prefab template in: " + type);
      //  UnityEngine.Object pPrefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        //var instance = Instantiate((Resources.Load("Player"+type), typeof(GameObject))) as GameObject;
        var instance = Resources.Load<GameObject>(templateDirectoryPath+type);
        //GameObject createdObject = GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        bool isLoaded = instance;
        Debug.Log(isLoaded ? "Item" + instance.name + " successfully loaded from the inventory" :
                         "Error! 784 Item can not be loaded");
        //return (GameObject)pPrefab;
        return instance;
    }
    //---------For deleting in db-----------
    
    public void deleteTrashItemRecords(int itemid) //Cleaning unwante records
    {
       
       StartCoroutine(deleteTransactionsTrash(itemid, gamecnf.getLocalAccountData().userID));
               
    }
    IEnumerator deleteTransactionsTrash(int itemId, int userid)
    {     
        bool recordsDeleted = false;
        WWWForm form = new WWWForm();
        // Debug.Log(CURRENT_USER_ID);
        form.AddField("item_id", itemId);
        form.AddField("user_id", userid);
        
        using (UnityWebRequest www = UnityWebRequest.Post(itemTransactionUrlDel, form))
        {
            //www.SetRequestHeader ("cookie", "csrftoken=" + csrfCookie);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {                                  
                recordsDeleted = !recordsDeleted;                    
               
            }
             Debug.Log(
                 recordsDeleted ? 
                    "Records Item deleted successfully" : 
                    "ERROR! can't delete 789."
            );  
        }
    }
    public void destroyBtn(){
        Destroy(gameObject);
    }
    public void setBtnText(Text buttonText)
    {
        btnItemText = buttonText;
    }
    public void setBtnText(string buttonText)
    {
        btnItemText.text = buttonText;
    }
    public void setItemName(string name)
    {
        itemName = name;
    }
    public void setLevel(int lvl)
    {
        level = lvl;
    }
    public void setClassification_name(string classname)
    {
        classification_name = classname;
    }
    public void setItem_type_name(string type)
    {
        item_type_name = type;
    }
   
    public void setItemCode(string iCode)
    {
        itemCode = iCode;
    }
    public void setItem_status(string status)
    {
        item_status = status;
    }
}

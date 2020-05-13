using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game user local inventory storage
// Use this storage file for get item info from the CubeMarket database
[System.Serializable]
public class NEMdata{
    [SerializeField] string walletAddress;
    [SerializeField] string privateKey;
    [SerializeField] string publicKey;
    [SerializeField] int tokienBalance;
    [SerializeField] int userID;
    [SerializeField] bool isDataSaved = false;
    public string WalletAddress { get {return walletAddress;} }
    public string PrivateKey { get {return privateKey;} }
    public string PublicKey { get {return publicKey;} }
    public bool IsDataSaved { get{return isDataSaved;}}
    public int UserID {get {return userID;}}
    public int TokienBalance {get {return tokienBalance; } set{tokienBalance = value; }}
    public NEMdata() {} //For initial declaration
    public NEMdata(string waddr, string privkey, string pubkey, int userid)
    {
        walletAddress = waddr;
        privateKey = privkey;
        publicKey = pubkey;
        userID = userid;
        isDataSaved = !isDataSaved; //Marking that NEM data is saved
    }

}
[System.Serializable]
public class SItemCharacteristics{
    [SerializeField] int m_itemid = 0;
    [SerializeField] string m_charname;
    [SerializeField] int m_charval;
    public SItemCharacteristics(string n, int v) {
        m_charname = n;
        m_charval = v;
    }
    public string charname { get{ return m_charname; } set {m_charname = value;}}
    public int     charval { get{ return m_charval; }  set {m_charval  = value;}}
    public int    itemid { get{ return m_itemid; } set{m_itemid = value;}}
}
[System.Serializable]
public class GameItem{
  
    [SerializeField] int m_userId; //Add this for the item usage function
    [SerializeField] string m_gameItemName;
    [SerializeField] string m_gameItemType;
    [SerializeField] int m_itemLevel;
    [SerializeField] string m_class;
    [SerializeField] string m_itemCode;
    [SerializeField] string m_itemStatus;
    [SerializeField] int m_itemid = 0;
    [SerializeField] List<SItemCharacteristics> m_itemChardetails = new List<SItemCharacteristics>();
    public string gameItemName { get { return m_gameItemName; } set{ m_gameItemName = value; }}
    public string gameItemType { get { return m_gameItemType; } set{ m_gameItemType = value; }}
    public int itemLevel { get { return m_itemLevel; } set{ m_itemLevel = value; }}
    public string className { get { return m_class; } set{ m_class = value; }}
    public string itemCode { get { return m_itemCode; } set{ m_itemCode = value;} }
    public int UserID { get {return m_userId; } set {m_userId = value;} }
    public List<SItemCharacteristics> itemChardetails { get { return m_itemChardetails; } set { m_itemChardetails = value; }}
    public string itemStatus { get {return m_itemStatus; } set { m_itemStatus = value; }}
    public int itemid { get{ return m_itemid; } set{ m_itemid = value; }}
    public string printDetails()
    {
        return "Stored item in local storage:\n" + 
                "Item Name: " + gameItemName +
                "\nItem Type: " + gameItemType + 
                "\nItem level: " + itemLevel +
                "\nItem Class: " + className +
                "\nItem Code: " + itemCode +
                "\nUser ID: " + UserID;
    }
}
[System.Serializable]
public class GameLevelResources{
    [SerializeField] int userid;
    [SerializeField] int energon;
    [SerializeField] int credits;
    [SerializeField] bool dataIsSaved = false;
    public int Userid  {get {return userid;}  set {userid = value;}}
    public int Energon {get {return energon;} set {energon = value;}}
    public int Credits {get {return credits;} set {credits = value;}}
    public bool DataIsSaved {get { return dataIsSaved; } set {dataIsSaved = value; }}
    public GameLevelResources() {}
    public GameLevelResources(int userid, int energon, int credits)
    {
        this.userid = userid;
        this.energon = energon;
        this.credits = credits;
        dataIsSaved = !dataIsSaved;
    }
}
[System.Serializable]
public class User{
    [SerializeField] int m_user_id; 
    [SerializeField] string m_nickname;
    [SerializeField] string m_email;
    [SerializeField] string m_password;
    [SerializeField] int m_energon;
    [SerializeField] int m_credits;
    [SerializeField] string loginTime;
    [SerializeField] string logoutTime;
    [SerializeField] string ip;
    // [SerializeField] int inGameEnergon;
    // [SerializeField] int inGameCredits;
    public int userID {get { return m_user_id; } set{m_user_id = value; }}
    public string nickname {get { return m_nickname;} set{m_nickname = value;}}
    public string email {get { return m_email; } set { m_email = value; } }
    public string password {get { return m_password;} set{m_password = value;}}
    public int energon { get { return m_energon; } set {m_energon = value; }}
    public int credits { get { return m_credits; } set {m_credits = value; }}
    public string LoginTime { get {return loginTime;} set{loginTime = value; }}
    public string LogoutTime { get {return logoutTime;} set{logoutTime = value; }}
    public string IP { get { return ip; } set{ ip = value; }}
    // public int InGameEnergon { get {return inGameEnergon; } set { inGameEnergon = value; }}
    // public int InGameCredits { get {return inGameCredits; } set { inGameCredits = value; }}
}
[CreateAssetMenu(fileName = "CubeMarketConf", menuName = "CubeMarketConf/gameAssetStorage", order = 0)]
public class GameStorageConf : ScriptableObject
{
    [SerializeField] ItemDetails itemInfoCodes;
    [SerializeField] bool isLoggedIn = false;    
    [SerializeField] List<GameItem> userItemsFromGame = new List<GameItem>();
    [SerializeField] User localGameAccount = new User();
    [SerializeField] NEMdata nemdata;
    [SerializeField] List<GameLevelResources> gameLocalAccRes = new List<GameLevelResources>(); 
    public NEMdata Nemdata {get { return nemdata; } set { nemdata = value; }}
    public bool IsLoggedIn {get { return isLoggedIn; } set { isLoggedIn = value; }}
    public List<GameLevelResources> GameLocalAccRes {get { return gameLocalAccRes; }}
    //Cached values
    // [SerializeField] List<GameObject> itemsReadyForLocalInventory = new List<GameObject>();
    // [SerializeField] List<GameObject> myAccountItems = new List<GameObject>();
    // public List<GameObject> MyAccountItems {get { return myAccountItems;} set { myAccountItems = value; } }

    public void markUserStatus( bool s)
    {
        isLoggedIn = s;
    }
    public bool isUserLoggedIn()
    {
        return isLoggedIn;
    }
    
    public void createLocalUserAccount(int userid, string n, string e, string pass)
    {
        // Creating a temporary local user account
        localGameAccount.userID = userid;
        localGameAccount.nickname = n;
        localGameAccount.email = e;
        localGameAccount.password = pass;
    }
    public void assignGameResourcesToLocAccount(int energon, int credits)
    {
        localGameAccount.energon = energon;
        localGameAccount.credits = credits;
    }
    public void collectUserItem(GameObject obj)
    {
     
        //Storing item in local storage file
       foreach (var item in userItemsFromGame)
       {
           if(item.itemCode == obj.GetComponent<ItemDescription>().getItemCode())
           {
               Debug.Log("Object already exists in a list! Not adding then");
               return;
           }
       }
       var gameItem = new GameItem();
       var itemInfo = obj.GetComponent<ItemDescription>();
       var chList = new List<SItemCharacteristics>();
       chList.Add(new SItemCharacteristics("HP", itemInfo.getHP()));
       chList.Add(new SItemCharacteristics("DMG", itemInfo.getDmg()));
       chList.Add(new SItemCharacteristics("SHD", itemInfo.getShield()));
       gameItem.itemChardetails = chList;     
       gameItem.gameItemName = itemInfo.getAssetName();
       gameItem.gameItemType = itemInfo.getAssetType();
       gameItem.itemLevel = itemInfo.getAssetLvl();
       gameItem.className = itemInfo.getClassificationName();
       gameItem.UserID =  getLocalAccountData().userID;
       gameItem.itemStatus = "game";
       //Add this to function
        string assetCode = itemInfo.getAssetName().Substring(3) +
        "-" + itemInfo.getAssetType().Substring(itemInfo.getAssetType().Length - 2)
          + itemInfo.getAssetLvl().ToString();
        Debug.Log(assetCode);

       gameItem.itemCode = assetCode;
       itemInfoCodes.addCode(assetCode);
       Debug.Log(gameItem.printDetails());
       //Adding details about characteristics


       userItemsFromGame.Add(gameItem);
    }
    public int getUserItemCount(int userid)
    {
        int amount = 0;
        if(userItemsFromGame.Count > 0) 
        {
            foreach (var item in userItemsFromGame)
            {
                if (item.UserID == userid)
                   amount++; 
            }
            return amount;
        }
        else
        {
            return amount;
        }
    }
    public void collectItemsFromDataString(List<string> gameItemsData, List<string> gameItemCh)
    {    
       if (!IsLoggedIn) return;
       GameItem gameItem = new GameItem();
       var chList = new List<SItemCharacteristics>();
      //Main item data collecting process
      foreach(var gitem in gameItemsData)
      {
          if (!string.IsNullOrEmpty(gitem))
          {
            var temp = gitem.Split(' ');
            if (getLocalAccountData().userID == int.Parse(temp[0]) && !itemExistsInFile(temp[1]))
            {    
                //NOTE: Calculate object amount           
                    gameItem.UserID = int.Parse(temp[0]);              
                    gameItem.itemid = int.Parse(temp[1]);                
                    gameItem.gameItemName = temp[2];
                    gameItem.gameItemType = temp[6];
                    gameItem.itemLevel = int.Parse(temp[7]);
                    gameItem.className = temp[5];
                    gameItem.itemCode = temp[3];
                    itemInfoCodes.addCode(temp[3]);//Adding received item code to configuration file
                    gameItem.itemStatus = temp[4];
                    
                    
                   
                
            }
          }
      }
      if (gameItem.itemid > 0 && gameItem != null)
      {
        //Collecting item characteristic data
        int p = 0;
        // List<List<SItemCharacteristics>> allItemCh = new List<List<SItemCharacteristics>>();
        foreach (var gitem in gameItemCh)
        { 
            if (!string.IsNullOrEmpty(gitem))
            {               
                var temp = gitem.Split('-');
                if (gameItem.itemid == int.Parse(temp[0]))
                {                      
                    SItemCharacteristics chHP;
                    SItemCharacteristics chDMG;
                    SItemCharacteristics chSHD;          
                    if (temp[1] == "HP")
                    {
                        chHP = new SItemCharacteristics(temp[1], int.Parse(temp[2]));
                        if (chHP != null) //Checking if object is not empty
                        {
                            chHP.itemid = gameItem.itemid;
                            chList.Add(chHP);
                        }
                    }
                    else if (temp[1] == "DMG")
                    {
                        chDMG = new SItemCharacteristics(temp[1], int.Parse(temp[2]));
                        if (chDMG != null) //Checking if object is not empty 
                        {
                            chDMG.itemid = gameItem.itemid;
                            chList.Add(chDMG);
                        }
                    }
                    else if (temp[1] == "SHD")
                    {
                        chSHD = new SItemCharacteristics(temp[1], int.Parse(temp[2]));
                        if (chSHD != null) //Checking if object is not empty
                        {
                            chSHD.itemid = gameItem.itemid;
                            chList.Add(chSHD);
                        }
                    }
                   
                
                }
            }
        }
      }
       if (chList.Count > 0) //If there is atleast one chracteristic, then add list to gameItem record
       {
           gameItem.itemChardetails = chList;
       }
      //Finally adding itemToLocal inventory
      if (gameItem.itemid > 0)
            userItemsFromGame.Add(gameItem);
    }
    public void assignUserBalance(int amount)
    {
       if (IsLoggedIn && nemdata.IsDataSaved)
       {
           nemdata.TokienBalance = amount;
       }
       else
       {
           Debug.Log("No amount received from NEM server or usr balance current is 0!");
       }
    }
    private bool itemExistsInFile(string itemid)
    {
       if (userItemsFromGame.Count > 0) //Checking by item id if item currently exists in local configuration file
       {
            foreach(var item in userItemsFromGame)
            {
                if(item.itemid == int.Parse(itemid))
                   return true;
            }
       }
       return false;
    }
    public void assignNemData(string waddr, string priv, string pub)
    {
        if (IsLoggedIn)
        {
            var nemDataRecord = new NEMdata(waddr, priv, pub, localGameAccount.userID);
            nemdata = nemDataRecord;
        }
        else
        {
           Debug.LogError("User is not logged in to the game, so no data related to nem blockchain is save"); 
        }
    }
    public User getLocalAccountData()
    {
        return localGameAccount;
    }
    public void destroyLocalAccount() //Resets account
    {
        localGameAccount = new User();   
        Nemdata = new NEMdata();     
    }
    public List<GameItem> getGameItemList()
    {
        return userItemsFromGame;
    }
    public bool createdGRRecordExists(int userid)
    {
        
            foreach (var item in gameLocalAccRes)
            {
                if (item.Userid == userid)
                {                   
                    return true;
                }
            }
          
           return false;
        
       
    }
    public GameLevelResources getGRRecord(int userid)
    {
        if (gameLocalAccRes.Count > 0)
        {
            foreach(var item in gameLocalAccRes)
            {
                if (item.Userid == userid)
                    return item;
            }
        }
        return null;
    }
    public void updateOrCreateGRRecord(int userid, int energon, int credits)
    {
        if (gameLocalAccRes.Count > 0)
        {
            foreach (var item in gameLocalAccRes)
            {
                if (item.Userid == userid)
                {
                    item.Userid = userid;
                    item.Energon = energon;
                    item.Credits = credits;
                    return;
                }
            }
            //If the record does not exist then add as a new one
            gameLocalAccRes.Add(new GameLevelResources(userid, energon, credits));
        }
        else if (gameLocalAccRes != null)
        {
             gameLocalAccRes.Add(new GameLevelResources(userid, energon, credits));
        }
        else
        {
            return;
        }
    }
    public void deleteGameItemInfoByName(string name)
    {
        foreach(var o in userItemsFromGame)
        {
            if(o.gameItemName == name)
            {
                userItemsFromGame.RemoveAt(userItemsFromGame.IndexOf(o));
                return;
            }
        }
    }
}

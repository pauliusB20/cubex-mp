using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    [Header("Item configuration parameters")]
    [SerializeField] GameStorageConf gamecnf;
    [SerializeField] bool readyForTransfer = false; //For Checking if object is ready for transfering into the inventory
    [SerializeField] string assetName = "LightTroopUnit";
    [SerializeField] string classificationName = "TroopUnit";
    [SerializeField] string asseTtype = "LightTroop";
    [SerializeField] int assetLevel = 5;
    [SerializeField] string itemCode = "undefined";
    [SerializeField] int hp_points = 15;
    [SerializeField] int dmg_points = 20;
    [SerializeField] int shield_points = 100;    
    [SerializeField] GameObject transferInvWindow;
    [SerializeField] GameObject successPanel;
    [SerializeField] bool isBuilding = false;
    private void OnMouseDown() {  
      if (gamecnf.isUserLoggedIn() && !isBuilding)     
       transferInvWindow.SetActive(true); 
    }
    public void makeReadyForTransfer()
    {
        readyForTransfer = true;
        successPanel.SetActive(readyForTransfer);
    }
    public bool itemReadyForTransfer()
    {
        return readyForTransfer;
    }
    public void setAssetName(string n)
    {
        assetName = n;
    }
    public void setClass(string n)
    {
        classificationName = n;
    }
    public void setAssetType(string n)
    {
        asseTtype = n;
    }
    public void setAssetLevel(int n)
    {
        assetLevel = n;
    }
    public void setHP(int n)
    {
        hp_points = n;
    }
    public void setDmg(int n)
    {
        dmg_points = n;
    }
    public void setShield(int n)
    {
        shield_points = n;
    }
    public string getClassificationName()
    {
        return classificationName;
    }
    public string getAssetName()
    {
        return assetName;
    }
    public string getAssetType()
    {
        return asseTtype;
    }
    public int getAssetLvl()
    {
        return assetLevel;
    }
    public string getItemCode()
    {
        return itemCode;
    }
    public void setItemCode(string ic)
    {
        itemCode = ic;
    }
    public int getHP()
    {
        return hp_points;
    }
    public int getDmg()
    {
        return dmg_points;
    }
    public int getShield()
    {
        return shield_points;
    }
}

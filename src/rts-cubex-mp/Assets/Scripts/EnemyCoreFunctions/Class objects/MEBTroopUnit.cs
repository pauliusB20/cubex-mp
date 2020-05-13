using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MEBTroopUnit : System.Object
{
    [Header("Main troop unit configuration parameters")]
    [SerializeField] string tCode = "none";
    [SerializeField] string troopDomeTag = "eADome";
    [SerializeField] int sHealth = 100;
    [SerializeField] int sDmgPoints = 25;
    [SerializeField] int troopPriceInCredits = 5;
    [SerializeField] GameObject troopUnitModel;
    [SerializeField] int originalHealth = 0;
    [SerializeField] int originalDmgPoints = 0;
    
    public GameObject getTroopUnit()
    {
        this.troopUnitModel.GetComponent<HealthEnemyAI>().setHealth(sHealth);
        var troopDome = FindGameObjectInChildWithTag (troopUnitModel, troopDomeTag);
        var spinGun = troopDome.GetComponent<AutoAttackTest>();
        spinGun.getSpinGun().GetComponent<FireLaser>().setMaxDmgPoints(sDmgPoints);

        return troopUnitModel;
    }
    public GameObject FindGameObjectInChildWithTag (GameObject parent, string tag)
     {
         Transform t = parent.transform;
 
         for (int i = 0; i < t.childCount; i++) 
         {
             if(t.GetChild(i).gameObject.tag == tag)
             {
                 return t.GetChild(i).gameObject;
             }
                 
         }
             
         return null;
     }
    public void setHealth(int h)
    {
        originalHealth = sHealth;
        sHealth = h;
    }
    public void setDmgPoints(int dmg)
    {
        originalDmgPoints = sDmgPoints;
        sDmgPoints = dmg;
    }
    public string getTCode()
    {
        return tCode;
    }
    public int getStartingHealth()
    {
        return sHealth;
    }
    public void resetStateParameters()
    {
        originalHealth = 0;
        originalDmgPoints = 0;
    }
    public int getSDmgPoints()
    {
        return sDmgPoints;
    }
    public int getCreditPrice()
    {
        return troopPriceInCredits;
    }
    public bool parUpdated()
    {
        return originalHealth != sHealth && originalDmgPoints != sDmgPoints;
    }
}

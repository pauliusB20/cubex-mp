using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ThLvl : System.Object
{
    [Header("Main configuration parameters")]
    [SerializeField] int starterLvl = 0;
    [SerializeField] int maxLvl = 2;
    [SerializeField] int lvlIncreasePriceCredits = 40;
    [SerializeField] bool isReachedMax = false;

    [Header("Main upgrade parameters")]
    [SerializeField] int healthUpgradeOffset = 30;
    [SerializeField] int healthWallUpgradeOffset = 30;
    [SerializeField] int damagePointsOffset = 15;
    [SerializeField] int damagePointsTurretOffset = 15;

    public void increaseCurrentLvl()
    {   
        if (starterLvl >= maxLvl)
        {
            starterLvl = maxLvl;
            isReachedMax = true;
        }
        else
        {
            starterLvl++;
        } 
    }
    public bool ReachedMax()
    {
        return isReachedMax;
    }

    public int getLvl()
    {
        return starterLvl;
    }
    public void setStartingLvl(int lvl)
    {
        starterLvl = lvl;
    }
    public int getWallUpgradeHealth()
    {
        return healthWallUpgradeOffset;
    }
    public int getHealthUpgradeOffset()
    {
        return healthUpgradeOffset; 
    }
    public int getDamagePointsOffset()
    {
        return damagePointsOffset;
    }
    public bool upgradedLevelState()
    {
        return starterLvl > 0;
    }
    public int getUpgradePrice()
    {
        return lvlIncreasePriceCredits;
    }
    public int getDamagePointsTurretOffset()
    {
        return damagePointsTurretOffset;
    }
}

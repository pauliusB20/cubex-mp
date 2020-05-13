using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MBuildingStructures", menuName = "EnemyAI_Test/MBuildingStructures", order = 0)]
public class MBaseStructures : ScriptableObject
{
    //Configuration file for storing and searching for a building prefab file and it's current price

    [SerializeField] List<BuildingStructre> mBaseStructures;

    public GameObject getBuilding(string mbCode)
    {
        if (mBaseStructures == null) return null;

        for (int mbStIndex = 0; mbStIndex < mBaseStructures.Count; mbStIndex++)
        {
            if (mBaseStructures[mbStIndex].getBuildingCode() == mbCode)
            {
                return mBaseStructures[mbStIndex].getBuildingStructure();
            }
        }
        return null;
    }
    public bool canBuy(string mBCode, int creditAmount)
    {
       if (mBaseStructures == null) return false;

        for (int mbStIndex = 0; mbStIndex < mBaseStructures.Count; mbStIndex++)
        {
            if (mBaseStructures[mbStIndex].getBuildingCode() == mBCode && mBaseStructures[mbStIndex].getBuildingPrice() <= creditAmount)
            {
                return true;
            }
        }
        return false;  
    }
    public int getEnergonPrice(string mBCode)
    {
       if (mBaseStructures == null) return 0;

        for (int mbStIndex = 0; mbStIndex < mBaseStructures.Count; mbStIndex++)
        {
            if (mBaseStructures[mbStIndex].getBuildingCode() == mBCode)
            {
                return mBaseStructures[mbStIndex].getRequiredEnergonAmount();
            }
        }
        return 0; 
    }
    public int getBuildingPrice(string mBCode)
    {
        if (mBaseStructures == null) return 0;

        for (int mbStIndex = 0; mbStIndex < mBaseStructures.Count; mbStIndex++)
        {
            if (mBaseStructures[mbStIndex].getBuildingCode() == mBCode)
            {
                return mBaseStructures[mbStIndex].getBuildingPrice();
            }
        }
        return 0;
    }
}

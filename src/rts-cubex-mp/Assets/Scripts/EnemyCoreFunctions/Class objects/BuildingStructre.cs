using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingStructre : System.Object
{
    [Header("Main configuration parameters")]
    [SerializeField] GameObject buildingStucture;
    [SerializeField] string buildingCode;
    [SerializeField] int buildingPrice;
    [SerializeField] int requiredEnergonAmount;


    public GameObject getBuildingStructure()
    {
        return buildingStucture;
    }
    public string getBuildingCode()
    {
        return buildingCode;
    }
    public int getBuildingPrice()
    {
        return buildingPrice;
    }
    public int getRequiredEnergonAmount()
    {
        return requiredEnergonAmount;
    }
}

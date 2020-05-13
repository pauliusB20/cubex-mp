using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillitaryBaseUnit
{
    //Main parent class for millitary base unit data 
    string unitName;

    GameObject unitModel;

    int price;

    public MillitaryBaseUnit(string uname, GameObject uModel, int price)
    {
        this.unitName = uname;
        this.unitModel = uModel;
        this.price = price;
    }
    
    public int getPrice()
    {
        return price;
    }
    public string getUnitName()
    {
        return unitName;
    }
    public GameObject getUnitModel()
    {
        return unitModel;
    }
    
    
}

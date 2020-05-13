using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CubeMarketConf", menuName = "CubeMarketConf/itemDetails", order = 0)]
public class ItemDetails : ScriptableObject
{
    [SerializeField] List<string> itemCodes;

    public void addCode(string code){
        //Checking if item code exists
        foreach (var itemCode in itemCodes)
        {
            if (itemCode == code) 
                return;
        }
        //If the code does not exist than adding a new item code
        itemCodes.Add(code);
    }
    public List<string> getItemCodes()
    {
        return itemCodes;
    }
}

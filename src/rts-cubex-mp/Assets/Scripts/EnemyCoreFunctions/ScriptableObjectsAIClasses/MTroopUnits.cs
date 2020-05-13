using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MTroopUnits", menuName = "EnemyAI_Test/MTroopUnits", order = 0)]
public class MTroopUnits : ScriptableObject
{
   
   [SerializeField] List<MEBTroopUnit> troopUnits;
   
   //Method for getting the specific troop unit from the list... NOTE: add lvl parameter for tech level
   public GameObject getTroopUnit(string tCode)
   {
       for (int tIndex = 0; tIndex < troopUnits.Count; tIndex++)
       {
           if (troopUnits[tIndex].getTCode() == tCode)
           {
              return troopUnits[tIndex].getTroopUnit();
           }
       }

       return null;
   }
   public int getUnitPriceInCredits(string tCode)
   {
        for (int tIndex = 0; tIndex < troopUnits.Count; tIndex++)
       {
           if (troopUnits[tIndex].getTCode() == tCode)
           {
              return troopUnits[tIndex].getCreditPrice();
           }
       }

       return 0;
   }
  public List<MEBTroopUnit> getTroopList()
  {
      return troopUnits;
  }
   
     
}

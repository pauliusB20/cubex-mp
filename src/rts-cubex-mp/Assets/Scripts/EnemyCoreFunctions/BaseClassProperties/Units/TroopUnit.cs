using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopUnit : MillitaryBaseUnit
{
   string troopType;

   public TroopUnit(string name, GameObject model, string troopType, int price) : base(name, model, price)
   {
       this.troopType = troopType;
   }

   public string getTroopType()
   {
       return troopType;
   }
   public string toString()
   {
       return "TUnit: name=" + base.getUnitName() + ", troopType=" + troopType + ", price=" + base.getPrice(); 
   }
}

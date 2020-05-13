using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerUnit : MillitaryBaseUnit
{
   string workerType;

   public WorkerUnit(string name, GameObject model, string workerType, int price) : base(name, model, price)
   {
       this.workerType = workerType;
   }

   public string getWorkerType()
   {
       return workerType;
   }
   public string toString()
   {
       return "TUnit: name=" + base.getUnitName() + ", troopType=" + workerType + ", price=" + base.getPrice(); 
   }
}

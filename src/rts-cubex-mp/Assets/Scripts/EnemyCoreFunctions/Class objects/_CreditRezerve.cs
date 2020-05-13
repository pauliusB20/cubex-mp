using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class _CreditRezerve : System.Object
{
    //Credit reserve storage code
   [SerializeField] private int creditReserve = 0;
   [SerializeField] private int maxReserveCap = 120;
   [SerializeField] private string creditReserveName;
   [SerializeField] private bool isFull = false;
   [SerializeField] private string enemyGroupCode = "";
   public _CreditRezerve(string crName, string eGc)
   {
      this.creditReserveName = crName;
      this.enemyGroupCode = eGc;
   }

   public void addCredits(int creditSum)
   {
       creditReserve += (creditSum  <=  maxReserveCap) ? creditSum : 0;
       isFull = (creditReserve >= maxReserveCap);
   }
   public int getCreditReserveCap()
   {
       return creditReserve;
   }
   public void setRezerveCap(int r)
   {
       this.creditReserve = r;
   }
   public string getReserveName()
   {
       return creditReserveName != null || creditReserveName != "" ? creditReserveName : "Name not found!";
   }
   public bool Full()
   {
       return isFull;
   }
   public string getBelogingGroup()
   {
       return enemyGroupCode;
   }
   public string toString()
   {
       return "RName= " + creditReserveName + ", creditCap= " + creditReserve + ", bgroup=" + enemyGroupCode ;
   }
   
}

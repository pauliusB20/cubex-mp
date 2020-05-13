using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnergonRezerve
{
     //Credit reserve storage code
   [SerializeField] private int energonReserve = 0;
   [SerializeField] private int maxReserveCap = 120;
   [SerializeField] private string energonReserveName;
   [SerializeField] private bool isFull = false;
   [SerializeField] private string belongingGroupCode = "";
   public EnergonRezerve(string erName, string eGc)
   {
      this.energonReserveName = erName;
      this.belongingGroupCode = eGc;
   }

   public void addEnergon(int energon)
   {
       energonReserve += energon  <=  maxReserveCap ? energon : 0;
       isFull = (energonReserve >= maxReserveCap);
   }
   public int getEnergonReserveCap()
   {
       return energonReserve;
   }
   public void setRezerveCap(int r)
   {
       this.energonReserve = r;

       if (energonReserve < maxReserveCap)
       isFull = false;
   }
   public string getReserveName()
   {
       return energonReserveName != null || energonReserveName != "" ? energonReserveName : "Name not found!";
   }
   public bool Full()
   {
       return isFull;
   }
   public string getBelongingCode()
   {
       return belongingGroupCode;
   }
   public string toString()
   {
       return "RName= " + energonReserveName + ", energonCap= " + energonReserve + ", bgroup= " + belongingGroupCode;
   }
}

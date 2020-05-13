using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditRezerve
{
   private int maxRezerve = 120;
   private int currentRezerve = 0;
   private bool isFull = false;
    
   public void addCredits(int credits)
   {
       
       if (!isFull)
       {
        currentRezerve += credits;
        if (currentRezerve >= maxRezerve)
        {
            isFull = true;
        }
       }
       else {
           isFull = false;
           return;
       }
   } 
   public void purchase(int price)
   {
       if (currentRezerve > 0)
       {
         currentRezerve -= price;
         isFull = false;
       }
       else {
           return;
       }
   }
   public int getCredits()
   {
       return currentRezerve;
   }
    public bool Full()
    {
        return isFull;
    }
    public void setMaxRezerve(int amount)
    {
        maxRezerve = amount;
    }
    public int getMaxRezerve()
    {
        return maxRezerve;
    }
    public string toString()
    {
        return "Current Rezerve: " + currentRezerve;
    }
}

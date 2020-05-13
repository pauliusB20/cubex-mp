using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTransferCollector : MonoBehaviour
{
   [SerializeField] List<string> gameAssetTags = new List<string>();
   [SerializeField] GameStorageConf logItemMgr;  
   
   [Header("UI Res. configuration")]
   [SerializeField] GameObject marketMenu;
   [SerializeField] GameObject menuMarketFailedStateWindow;
   [SerializeField] GameObject menuMarketSuccessStateWindow;

   [SerializeField] float exitDelay = 2f;
   public void collectItemsForInventory() {
     //Collecting user items when he quits or wins a game
     if (logItemMgr.isUserLoggedIn())
     {
        collectAllMarkedObjects();
         
     }    
   }
   public void collectAllMarkedObjects()
   {
       Debug.Log("Item size for storing in user inventory are: " + GameObject.FindGameObjectsWithTag(tag).Length);
         foreach(var tag in gameAssetTags)
         {
             var gtaggedObjs = GameObject.FindGameObjectsWithTag(tag);
             foreach (var o in gtaggedObjs)
             {
                 if (o.GetComponent<ItemDescription>().itemReadyForTransfer())
                 {
                    logItemMgr.collectUserItem(o);
                    Debug.Log("Item: " + o.name + " for inventory collected!");
                 }
             }
         }
     
   }
   public void takeRemainingResources()
   {
      var playerBase = GameObject.FindGameObjectWithTag("PlayerBase");
      if (playerBase != null)
      {
         int rEnergon = 0, rCredits = 0;

         rEnergon = playerBase.GetComponent<Base>().getEnergonAmount();
         rCredits = playerBase.GetComponent<Base>().getCreditsAmount();

         if (rEnergon > 0 || rCredits > 0)
         {
            logItemMgr.updateOrCreateGRRecord
            (
               logItemMgr.getLocalAccountData().userID,
               rEnergon,
               rCredits
            );
            if (logItemMgr.createdGRRecordExists(logItemMgr.getLocalAccountData().userID))
            {
               marketMenu.SetActive(false);
               menuMarketSuccessStateWindow.SetActive(true);
            }
            else
            {
                marketMenu.SetActive(false);
                menuMarketFailedStateWindow.SetActive(true); 
            }
         }
         else
         {
            marketMenu.SetActive(false);
            menuMarketFailedStateWindow.SetActive(true);
         }
      }
      else
      {
         Debug.Log("Player Base not found :(( skipping resources saving step!");
      }
   }
   
}

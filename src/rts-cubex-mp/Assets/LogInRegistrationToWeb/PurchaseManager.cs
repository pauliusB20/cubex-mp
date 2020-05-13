using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class PurchaseManager : MonoBehaviour
{
    [Header("Configuration parameters")]
    [SerializeField] string urlDbTableUpdater;
    [SerializeField] Text userResText;
    [SerializeField] int decEnergonFactor = 10;
    [SerializeField] int decCreditsFactor = 10;
    [SerializeField] int originalEnergonAmount;
    [SerializeField] int originalCreditsAmount;
    [SerializeField] bool isDataAltered;
    AccountManager accountManager;
    NetworkUserLogInManager userNet;
    void Start()
    {
        accountManager = GetComponent<AccountManager>();
        userNet = FindObjectOfType<NetworkUserLogInManager>();
        if (!userNet) return;

       
    }

    // Update is called once per frame
    void Update()
    {
        // if (userNet.UserLoggedIn())
        // {
        //     userResText.text = "Resources:\nEnergon:" + accountManager.getCurrentUsersEnergonAmount() 
        //         + "\nCredits:" + accountManager.getCurrentUsersCreditsAmount();

            
        // }
    }
//     private void checkChanges()
//     {
//         var currentEnergonAmount = accountManager.getCurrentUsersEnergonAmount();
//         var currentCreditsAmount = accountManager.getCurrentUsersCreditsAmount();

//         if (originalEnergonAmount != currentEnergonAmount
//          && originalEnergonAmount >= currentEnergonAmount ||
//           originalCreditsAmount != currentCreditsAmount &&
//           originalCreditsAmount >= currentCreditsAmount)
//           {
//              originalEnergonAmount = currentEnergonAmount;
//              originalCreditsAmount = currentCreditsAmount;
//              StartCoroutine(refreshRecData(currentEnergonAmount, currentCreditsAmount));
             
//           }
//     }
//    IEnumerator refreshRecData(int currentEnergon, int currentCreditsAmount)
//     {
//          WWWForm form = new WWWForm();
//          Debug.Log("Current ID:" + userNet.getCurrentCurrentUserID());
//         form.AddField("id", userNet.getCurrentCurrentUserID());
// 		form.AddField("eamount", currentEnergon);
// 		form.AddField("camount", currentCreditsAmount);

        
//         using (UnityWebRequest www = UnityWebRequest.Post(urlDbTableUpdater, form))
//         {
//             //www.SetRequestHeader ("cookie", "csrftoken=" + csrfCookie);
//             yield return www.SendWebRequest();

//             if (www.isNetworkError || www.isHttpError)
//             {
//                 Debug.Log(www.error);
//             }
//             else
//             {                                  
//                 isDataAltered = true;                    
               
//             }
//              Debug.Log(
//                  isDataAltered ? 
//                     "Data updated successfully" : 
//                     "ERROR! can't update 789."
//             );  
//             // var getPageData = UnityWebRequest.Get(currentWebURL); 
//             // Debug.Log(getPageData.downloadHandler.text);
//         }
//     }
//     public void setStartingResourcesAmounts()
//     {
//         originalEnergonAmount = accountManager.getCurrentUsersEnergonAmount();
//         originalCreditsAmount = accountManager.getCurrentUsersCreditsAmount();
//     }
//     public void spendEnergon()
//     {
//          accountManager.setEnergonAmount(accountManager.getCurrentUsersEnergonAmount()-decEnergonFactor); 
//          checkChanges();
//     }
//      public void spendCredits()
//     {
//          accountManager.setCreditsAmount(accountManager.getCurrentUsersCreditsAmount()-decCreditsFactor); 
//          checkChanges();
//     }
}

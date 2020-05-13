using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AccResourcesManager : MonoBehaviour
{
    //NOTE:: ADD starting credit and energon amount to local account
   [Header("Network cnf.")]
   [SerializeField] CMRequestUrl cmurl;
   [Header("Resources sending conf.")]
   [SerializeField] GameStorageConf gamecnf;
   [SerializeField] int amountToTransferEnergon = 0;
   [SerializeField] int amountToTransferCredits = 0;
   [SerializeField] InputField inputEnergonField;
   [SerializeField] InputField inputCreditsField;
   [SerializeField] string sendingURL;
   [SerializeField] bool areResourcesSent = false;
   [SerializeField] GameObject successWindow; //Message for succeding in sending
   [SerializeField] GameObject failedWindow; //Message for failling to send
   [SerializeField] GameObject senderWindow;
   private void Start() 
   {
       sendingURL = cmurl.ResourcesSendingURL; //Assigning resource sending url
   }
    public void sendResources()
    {   
        if (!gamecnf.isUserLoggedIn()) return; //If user is not logged in, return
        if (gamecnf.getGRRecord(gamecnf.getLocalAccountData().userID) == null)
        {
            senderWindow.SetActive(false);
            failedWindow.SetActive(true);
            return;
        }
        int tempcheck;
        if (inputEnergonField.text.Length > 0 )
        {      
          if (int.TryParse(inputEnergonField.text, out tempcheck)) 
          {             
            amountToTransferEnergon = int.Parse(inputEnergonField.text);
            if (amountToTransferEnergon > gamecnf.getGRRecord(gamecnf.getLocalAccountData().userID).Energon
             || amountToTransferEnergon < 0)
            {
                Debug.Log("ERROR: Inputed energon resources amount does not exist in users current energon resources amount!");
                senderWindow.SetActive(false);
                failedWindow.SetActive(true);
                return;
            }
            
          }
          else
          {
            Debug.Log("ERROR: Invalid energon value");
            failedWindow.SetActive(true);
          }
        }
        else
        {
            amountToTransferEnergon = 0;
        } 
        if (inputCreditsField.text.Length > 0)
        {
            if (int.TryParse(inputCreditsField.text, out tempcheck))  
            {  
                amountToTransferCredits = int.Parse(inputCreditsField.text);
                if (amountToTransferCredits > gamecnf.getGRRecord(gamecnf.getLocalAccountData().userID).Credits || amountToTransferCredits < 0)
                {
                    Debug.Log("ERROR: Inputed credits resources amount does not exist in users current credits resources amount!");
                    senderWindow.SetActive(false);
                    failedWindow.SetActive(true);
                    return;
                }
            }
            else
            {
                Debug.Log("ERROR: Invalid credits value");
                senderWindow.SetActive(false);
                failedWindow.SetActive(true);
            }
        }
        else
        {
            amountToTransferCredits = 0;
        }
        Debug.Log("Sending: " +  (amountToTransferEnergon + ", " + amountToTransferCredits));
        transferResources(amountToTransferEnergon, amountToTransferCredits);
        // if (inputCreditsField.text.Length > 0)
        //     transferResources(0, amountToTransferCredits);
        // else if (inputEnergonField.text.Length > 0)
        //     transferResources(amountToTransferEnergon, 0); 
        // else if (inputEnergonField.text.Length > 0 && inputCreditsField.text.Length > 0)
        //      transferResources(amountToTransferEnergon, amountToTransferCredits); 
        // else 
        //     failedWindow.SetActive(true);
    }
    private void transferResources(int energon, int credits)
    {
        StartCoroutine(transferRes(energon, credits));
    }

    IEnumerator transferRes(int energon, int credits)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", gamecnf.getLocalAccountData().userID);
		form.AddField("energon", energon);
		form.AddField("credits", credits);
		
        using (UnityWebRequest www = UnityWebRequest.Post(sendingURL, form))
        {
            
            //www.SetRequestHeader ("cookie", "csrftoken=" + csrfCookie);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                senderWindow.SetActive(false);
                failedWindow.SetActive(true);
            }
            else
            {                                  
                areResourcesSent = !areResourcesSent;  
                //Decreasing current credits and energon amount  
                if (gamecnf.GameLocalAccRes.Count > 0)    
                {
                    foreach (var item in gamecnf.GameLocalAccRes)
                    {
                        if (item.Userid == gamecnf.getLocalAccountData().userID)
                        {
                            item.Energon -= energon;
                            item.Credits -= credits;
                            senderWindow.SetActive(false);
                            successWindow.SetActive(areResourcesSent);//If resources are sent, presenting a success window           
                            break;
                        }
                    }
                }
                
               
            }
             Debug.Log(
                 areResourcesSent ? 
                    "Resources registered successfully" : 
                    "ERROR! invalid resources or something wrong with the server."
            );
            
            

            //Display success window
             
             areResourcesSent = !areResourcesSent;  
        }    
    }

   
}


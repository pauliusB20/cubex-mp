using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CollectorBuild : MonoBehaviour
{
   //This script is attached on the build collector button 
    //************************UPDATE****************************************** */
    [Header("Main collector button configuration parameters ")]
    //State variable for checking when the user clicked on the button
    [SerializeField] bool btnCollectorClicked = false;
     //State variable for checking if the user's chosen collector object is constructed
    [SerializeField] bool collectorStructureBuilt = false;
    //Variable for knowing which button could be disabled
    [SerializeField] Button buildButton;
    //Variable for checking the button's text
    [SerializeField] Text buttonText;
    //Variable for remembering the button's text
    private Base playerbase;
    [SerializeField] int minNeededEnergonAmount;
    [SerializeField] int minNeededCreditsAmount;
    [SerializeField] float neededWorkerJobAmountForStrucuture = 0.66f;
    //remembering the button's text
    private void Start() {
        
         if(FindObjectOfType<Base>() == null)
         {
           return;
         }
         else{
           playerbase = FindObjectOfType<Base>();
         }
         
         buttonText.text = "Create Energon Station\n" + "(" + minNeededEnergonAmount + " energon & " +  minNeededCreditsAmount + " credits)";
    }
    private void Update() {
        //Checks if the collector structure is built, and if it is built, then this button is unlocked
        if (collectorStructureBuilt) 
        {
            //Sets the button click variable to false
           btnCollectorClicked = false;
           buildButton.interactable = true; 
           //Displays the previous button's text
           buttonText.text = "Create Energon Station\n" + "(" + minNeededEnergonAmount + " energon & " +  minNeededCreditsAmount + " credits)";
           //sets this variable to false because the collector structure is already constructed
           collectorStructureBuilt = false;
           return;
        }
    }
    //When the user clicked on the button, the player need's to select the deposit, which has the class BuildMiningStation
    //This method used on the button's OnClick() section
    public void clickedOnBuildCollectorBtn()
    {       
        if (playerbase.getEnergonAmount() < minNeededEnergonAmount || playerbase.getCreditsAmount() < minNeededCreditsAmount) // patikrina esamus zaidejo resursus
        {
            playerbase.setResourceAMountScreenState(true);
            return;
        }
        var playerWorkers = GameObject.FindGameObjectsWithTag("Worker");
        for(int i = 0; i < playerWorkers.Length; i++)
        {
           // Debug.Log(Math.Round(playerWorkers[i].GetComponent<WorkerLifeCycle>().getWorkerJobLeft(),2));
            if(!playerWorkers[i].GetComponent<Worker>().isWorkerAssigned() &&  Math.Round(playerWorkers[i].GetComponent<WorkerLifeCycle>().getWorkerJobLeft(),2) >= Math.Round(neededWorkerJobAmountForStrucuture,2))
            {
              //Debug.Log(playerWorkers[i].GetComponent<WorkerLifeCycle>().getWorkerJobLeft());
              btnCollectorClicked = true;
              buildButton.interactable = false;
              buttonText.text = "Select a deposit";  
              return;      
            }
        }
            
        playerbase.setErrorStateForPlayerCollector(true);
        return;  
            
    }   
    //Method for getting the state of the button, is it clicked - true or not - false
    public bool getClickedBuildCollectorBtnState()
    {
        return btnCollectorClicked;
    }
    public void setClickedBuildCollectorBtnState(bool newState)
    {
        btnCollectorClicked = newState;
    }
    //Method for sending a message to the collector build button that the collector structure is constructed..
    public void setCollectorStructureBuilt(bool state/*, string name*/)
    {
        collectorStructureBuilt = state;
        //originalBtnText = name;
    }
    public int getMinNeededEnergonAmountForEnergonCollector()
    {
      return minNeededEnergonAmount;
    } 
    public int getMinNeededCreditsAmountForCreditsCollector()
    {
      return minNeededCreditsAmount;
    } 
    public float getNeededWorkerJobAmountForStrucuture()
    {
      return neededWorkerJobAmountForStrucuture;
    }
}

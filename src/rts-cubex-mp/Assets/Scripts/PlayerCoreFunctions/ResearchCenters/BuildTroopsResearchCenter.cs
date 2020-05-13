using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildTroopsResearchCenter : MonoBehaviour
{
    //Button for building a barracks millitary structure...

    //NOTE: When you implement the barracks spawn system, then use this variable! :)
    //[SerializeField] GameObject barrack;
    [Header("Main configuration parameters")]
    //boolean variable for indicating when the user can build a barracks structure
    [SerializeField] bool canBuildTroopsResearchCentre = false;
     //boolean variable for indicating if the barracks structure is built
    [SerializeField] bool structureBuilt = false;
    //Button variable, which will used for disablying when the user clicked on the barracks construction button
    [SerializeField] Button buildTroopsResearchCentreBtn;
    [SerializeField] Text buttonText;
    [SerializeField] Text troopsResearchAmountText;
    private Base playerbase;
    [SerializeField] int minNeededEnergonAmountForResearchCentre;
    [SerializeField] int minNeededCreditsAmountForResearchCentre;
    private void Start() 
    {
        if(FindObjectOfType<Base>() == null)
        {
           return;
        }
        else
        {
           playerbase = FindObjectOfType<Base>();
        }
        buttonText.text = "Build Troops Research Center (" + minNeededCreditsAmountForResearchCentre + " credits & " + minNeededEnergonAmountForResearchCentre  + " energon)\n";
        troopsResearchAmountText.text = playerbase.getTroopsResearchCentreUnitAmount()+ "/1";
    }
    private void Update() {
        troopsResearchAmountText.text = playerbase.getTroopsResearchCentreUnitAmount()+ "/1";
        //Checks if the barracks structure is built in the base
        if (structureBuilt)
        {
            playerbase.setTroopsResearchCentreUnitAmount(playerbase.getTroopsResearchCentreUnitAmount() + 1);
            buttonText.text = "Build Troops Research Center (" + minNeededCreditsAmountForResearchCentre + " credits & " + minNeededEnergonAmountForResearchCentre  + " energon)\n";
            buildTroopsResearchCentreBtn.interactable = false;
            canBuildTroopsResearchCentre = false; 
            structureBuilt = false;
        }
    }
    //When you've clicked on the button, this method will be invoked in the Unity ClickOn() section
    public void buildResearchCentreAction()
    {
         if (playerbase.getEnergonAmount() < minNeededEnergonAmountForResearchCentre || playerbase.getCreditsAmount() < minNeededCreditsAmountForResearchCentre) // patikrina esamus zaidejo resursus
         {
          playerbase.setResourceAMountScreenState(true);    
          return; 
         }
        playerbase.setBuildingArea(true);
        //State variable is setted to true, which means that the button is clicked
        canBuildTroopsResearchCentre = true;
        //Button interaction state is setted to false
        buildTroopsResearchCentreBtn.interactable = false;
        buttonText.text = "Select Place";  
        //Add button locking system...
        //Like showing the text which says place the barracks object in the base area
        //Debug.Log("Select a place where to build a barrack.");
    }

    public bool buildResearchCentre()
    {
        return canBuildTroopsResearchCentre;
    }
    public void canBuildAgain(bool state) 
    {
        structureBuilt = state;
    }
    public int getMinNeededEnergonAmountForTroopsResearchCentre()
    {
      return minNeededEnergonAmountForResearchCentre;
    } 
    public int getMinNeededCreditsAmountForTroopsResearchCentre()
    {
      return minNeededCreditsAmountForResearchCentre;
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResearchCentreBuild : MonoBehaviour
{
     //Button for building a barracks millitary structure...

    //NOTE: When you implement the barracks spawn system, then use this variable! :)
    //[SerializeField] GameObject barrack;
    [Header("Main configuration parameters")]
    //boolean variable for indicating when the user can build a barracks structure
    [SerializeField] bool canBuildResearchCentre = false;
     //boolean variable for indicating if the barracks structure is built
    [SerializeField] bool structureBuilt = false;
    //Button variable, which will used for disablying when the user clicked on the barracks construction button
    [SerializeField] Button buildResearchCentreBtn;
    [SerializeField] Text buttonText;
    [SerializeField] Text availableResearchCenters;
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
         buttonText.text = "Build Building Research Center (" + minNeededCreditsAmountForResearchCentre + " credits & " + minNeededEnergonAmountForResearchCentre  + " energon)\n";
         availableResearchCenters.text = playerbase.getResearchCentreUnitAmount()+ "/1";
    }  
    private void Update() {
        availableResearchCenters.text = playerbase.getResearchCentreUnitAmount()+ "/1";
        //Checks if the structure is built in the base
        if (structureBuilt)
        {
            playerbase.setResearchCentreUnitAmount(playerbase.getResearchCentreUnitAmount() + 1);
            buttonText.text = "Build Building Research Center (" + minNeededCreditsAmountForResearchCentre + " credits & " + minNeededEnergonAmountForResearchCentre  + " energon)\n";
            buildResearchCentreBtn.interactable = false;
            canBuildResearchCentre = false; 
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
        canBuildResearchCentre = true;
        //Button interaction state is setted to false
        buildResearchCentreBtn.interactable = false;
        buttonText.text = "Select Place";  
        //Add button locking system...
        //Like showing the text which says place the barracks object in the base area
        //Debug.Log("Select a place where to build a barrack.");
    }

    public bool buildResearchCentre()
    {
        return canBuildResearchCentre;
    }
    public void canBuildAgain(bool state) 
    {
        structureBuilt = state;
    }
    public int getMinNeededEnergonAmountForResearchCentre()
    {
      return minNeededEnergonAmountForResearchCentre;
    } 
    public int getMinNeededCreditsAmountForResearchCentre()
    {
      return minNeededCreditsAmountForResearchCentre;
    } 
    public Text getResearchButtonText()
    {
      return buttonText;
    }
    public void setResearchButtonText(string newText)
    {
      buttonText.text = newText;
    }
}

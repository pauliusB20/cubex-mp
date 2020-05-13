using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurretBuild : MonoBehaviour
{
     //Button for building a barracks millitary structure...

    //NOTE: When you implement the barracks spawn system, then use this variable! :)
    //[SerializeField] GameObject barrack;
    [Header("Main configuration parameters")]
    //boolean variable for indicating when the user can build a barracks structure
    [SerializeField] bool canBuildTurret = false;
     //boolean variable for indicating if the barracks structure is built
    [SerializeField] bool structureBuilt = false;
    //Button variable, which will used for disablying when the user clicked on the barracks construction button
    [SerializeField] Button buildTurretBtn;
    [SerializeField] Text buttonText;
    private Base playerbase;
    [SerializeField] int minNeededEnergonAmountForTurret;//5
    [SerializeField] int minNeededCreditsAmountForTurret;//50
   // [SerializeField] GameObject clickUndo;
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
    //    clickUndo.SetActive(false);
       buttonText.text = "Create Turret\n" + "(" + minNeededEnergonAmountForTurret + " energon & " + minNeededCreditsAmountForTurret  + " credits)";
    }  
    private void Update() {

        //Checks if structure is built in the base
        if (structureBuilt)
        {
             buildTurretBtn.interactable = true;
             buttonText.text = "Create Turret\n" + "(" + minNeededEnergonAmountForTurret + " energon & " + minNeededCreditsAmountForTurret  + " credits)";
             canBuildTurret = false;
             structureBuilt = false;
        }
    }
    //When you've clicked on the button, this method will be invoked in the Unity ClickOn() section
    public void buildTurretAction()
    {
         if (playerbase.getEnergonAmount() < minNeededEnergonAmountForTurret || playerbase.getCreditsAmount() < minNeededCreditsAmountForTurret) // patikrina esamus zaidejo resursus
         {
          playerbase.setResourceAMountScreenState(true);    
          return; 
         }
        playerbase.setBuildingArea(true);
     //   clickUndo.SetActive(true);
        //State variable is setted to true, which means that the button is clicked
        canBuildTurret = true;
        //Button interaction state is setted to false
        buildTurretBtn.interactable = false;
        buttonText.text = "Select Place";  
        //Add button locking system...
        //Like showing the text which says place the barracks object in the base area
        //Debug.Log("Select a place where to build a barrack.");
    }

    public bool buildTurret()
    {
        return canBuildTurret;
    }
    public void canBuildAgain(bool state) 
    {
        structureBuilt = state;
    }
    public int getMinNeededEnergonAmountForTurret()
    {
      return minNeededEnergonAmountForTurret;
    } 
    public int getMinNeededCreditsAmountForTurret()
    {
      return minNeededCreditsAmountForTurret;
    } 
}

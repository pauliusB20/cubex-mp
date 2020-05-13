using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPlayerWall : MonoBehaviour
{
    
    //Button for building a barracks millitary structure...

    //NOTE: When you implement the barracks spawn system, then use this variable! :)
    //[SerializeField] GameObject barrack;
    [Header("Main configuration parameters")]
    //boolean variable for indicating when the user can build a barracks structure
    [SerializeField] bool canBuildWall = false;
     //boolean variable for indicating if the barracks structure is built
    [SerializeField] bool structureBuilt = false;
    //Button variable, which will used for disablying when the user clicked on the barracks construction button
    [SerializeField] Button buildPlayerWallBtn;
    [SerializeField] Text buttonText;
    private Base playerbase;
    [SerializeField] int minNeededEnergonAmount;
    [SerializeField] int minNeededCreditsAmount;
  //  [SerializeField] GameObject clickUndo;
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
        buttonText.text = "Create Defensive Wall\n" + "(" + minNeededEnergonAmount + " energon & " +  minNeededCreditsAmount + " credits)";
    }  
    private void Update() {

        //Checks if structure is built in the base
        if (structureBuilt)
        {
             buildPlayerWallBtn.interactable = true;
             buttonText.text = "Create Defensive Wall\n" + "(" + minNeededEnergonAmount + " energon & " + minNeededCreditsAmount  + " credits)";
             canBuildWall = false;
             structureBuilt = false;
        }
    }
    //When you've clicked on the button, this method will be invoked in the Unity ClickOn() section
    public void buildPLayersWallsAction()
    {
        if (playerbase.getEnergonAmount() < minNeededEnergonAmount || playerbase.getCreditsAmount() < minNeededCreditsAmount) // patikrina esamus zaidejo resursus
        {
        playerbase.setResourceAMountScreenState(true);    
        return; 
        }
        playerbase.setBuildingArea(true);
    //    clickUndo.SetActive(true);
        //State variable is setted to true, which means that the button is clicked
        canBuildWall = true;
        //Button interaction state is setted to false
        buildPlayerWallBtn.interactable = false;
        buttonText.text = "Select Build Site";  
        //Add button locking system...
        //Like showing the text which says place the barracks object in the base area
        //Debug.Log("Select a place where to build a barrack.");
    }

    public bool buildPlayerWall()
    {
        return canBuildWall;
    }
    public void canBuildAgain(bool state)
    {
        structureBuilt = state;
    }
    public int getMinNeededEnergonAmountForDefensiveWall()
    {
      return minNeededEnergonAmount;
    } 
    public int getMinNeededCreditsAmountForDefensiveWall()
    {
      return minNeededCreditsAmount;
    } 
}

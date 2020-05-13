using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrackBuild : MonoBehaviour
{
   //Button for building a barracks millitary structure...

    //NOTE: When you implement the barracks spawn system, then use this variable! :)
    //[SerializeField] GameObject barrack;
    [Header("Main configuration parameters")]
    //boolean variable for indicating when the user can build a barracks structure
    [SerializeField] bool canBuildBarrack = false;
     //boolean variable for indicating if the barracks structure is built
    [SerializeField] bool structureBuilt = false;
    //Button variable, which will used for disablying when the user clicked on the barracks construction button
    [SerializeField] Button buildBarrackBtn;
    [SerializeField] Text buttonText;
    private Base playerbase;
    [SerializeField] int minNeededEnergonAmount;
    [SerializeField] int minNeededCreditsAmount;
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
       buttonText.text = "Create Troops Barrack\n" + "(" + minNeededEnergonAmount + " energon & " +  minNeededCreditsAmount + " credits)";
    }  
    private void Update() {

        //Checks if the barracks structure is built in the base
        if (structureBuilt)
        {
             buildBarrackBtn.interactable = true;
             buttonText.text = "Create Troops Barrack\n" + "(" + minNeededEnergonAmount + " energon & " +  minNeededCreditsAmount + " credits)";
             canBuildBarrack = false;
             structureBuilt = false;
        }
    }
    //When you've clicked on the button, this method will be invoked in the Unity ClickOn() section
    public void buildBarrackAction()
    {
        if (playerbase.getEnergonAmount() < minNeededEnergonAmount || playerbase.getCreditsAmount() < minNeededCreditsAmount) // patikrina esamus zaidejo resursus
        {
        playerbase.setResourceAMountScreenState(true);    
        return; 
        }
        playerbase.setBuildingArea(true);
        //State variable is setted to true, which means that the button is clicked
        canBuildBarrack = true;
        //Button interaction state is setted to false
        buildBarrackBtn.interactable = false;
        buttonText.text = "Select Build Site";  
        //Add button locking system...
        //Like showing the text which says place the barracks object in the base area
        //Debug.Log("Select a place where to build a barrack.");
    }

    public bool buildBarrack()
    {
        return canBuildBarrack;
    }
    public void canBuildAgain(bool state)
    {
        structureBuilt = state;
    }
    public int getMinNeededEnergonAmountForTroopsBarrack()
    {
      return minNeededEnergonAmount;
    } 
    public int getMinNeededCreditsAmountForTroopsBarrack()
    {
      return minNeededCreditsAmount;
    } 
   
}

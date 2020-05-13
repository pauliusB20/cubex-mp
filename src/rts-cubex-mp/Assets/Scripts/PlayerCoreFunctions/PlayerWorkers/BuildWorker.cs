using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildWorker : MonoBehaviour
{
   //Button for building a barracks millitary structure...

    //NOTE: When you implement the barracks spawn system, then use this variable! :)
    //[SerializeField] GameObject barrack;
    [Header("Main configuration parameters")]
    //boolean variable for indicating when the user can build a barracks structure
    [SerializeField] bool canBuildWorker = false;
     //boolean variable for indicating if the barracks structure is built
    [SerializeField] bool structureBuilt = false;
    //Button variable, which will used for disablying when the user clicked on the barracks construction button
    [SerializeField] Button buildWorkerBtn;
    [SerializeField] Text buttonText;
    private Base playerbase;
    [SerializeField] int minNeededEnergonAmount = 10;
   // [SerializeField] GameObject clickUndo;
    private void Start() 
    {
        playerbase = FindObjectOfType<Base>();
        buttonText.text = "Create Worker\n" + "(" + minNeededEnergonAmount + " energon)";
    }  
    private void Update() {
        //Checks if the barracks structure is built in the base
        if (structureBuilt)
        {
             canBuildWorker = false;
             structureBuilt = false;
        }
    }
    //When you've clicked on the button, this method will be invoked in the Unity ClickOn() section
    public void buildWorkerAction()
    {
        if (playerbase.getEnergonAmount() < minNeededEnergonAmount ) // patikrina esamus zaidejo resursus
        {
        playerbase.setResourceAMountScreenState(true);    
        return; 
        }
        playerbase.setBuildingArea(true);
     //   clickUndo.SetActive(true);
        //State variable is setted to true, which means that the button is clicked
        canBuildWorker = true;
        //Button interaction state is setted to false
        buildWorkerBtn.interactable = false;
        buttonText.text = "Select Build Site";  
        //Add button locking system...
        //Like showing the text which says place the barracks object in the base area
        //Debug.Log("Select a place where to build a barrack.");
    }

    public bool buildWorker()
    {
        return canBuildWorker;
    }
    public void canBuildAgain(bool state)
    {
        structureBuilt = state;
    }
    public int getMinNeededEnergonAmount()
    {
      return minNeededEnergonAmount;
    } 
}

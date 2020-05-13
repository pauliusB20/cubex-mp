using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorkerSpawningTimer : MonoBehaviour
{
    /*
     Timer countdown system V2
     System installation:
     1) Add this script to a button's child gameObject, which is the text object. 
        Note: the button, which has the text gameObject is the one that will used for spawning certain objects in the game.
     2) Them add the required system configuration GameObjects(which are indicated by the [SerializeField] property), which will be used for outputting certain data on the game screen.
     3) Then start the current game prortotype and check if this timer is working

     The way it works:
     1) When the user clicks on the button, the boolean value startcountdown is setted to true and the current timer's button 
        interactable state is setted to false. As well, once clicked on the button, a message apears on the UI Text component, which indicates that
        a timer has started working.

     2) The boolean value is signal for starting the timer coundown. Once this value is true, the system takes the timeStart variable
        and subtracts it by using the Unity system's frame time class Time variable's deltaTime value, which the current game time in
        Unity. When it subtracts it, then the system uses the Mathf.Round() function for rounding the variable, so the time data could
        be more easily understand and looking more appealing to the user when comes by printing information in the game window.

     3) When the system tryes calculating the certain time, it tryes calculating from the starting time value in seconds from the timeStart variable's
        value to 0. Whyle it is calculating it prints the current timeStart value on the button text. Once the timer has done counting then
        it uses the string variable startBtn to rest the current timer's button text variable's value by setting the button's text value to be the starting
        button's text.
    
     */
    //Initial variables for the timer countdown system
    [Header("Main Timer configuration parameters")]
    //Button variable, which will used in setting the current button's interactability state. The button
    [SerializeField] Button startBtn;
    [SerializeField] Button additionalWorker;
    Base playerbase; 
    bool startCountdown = false;
    [SerializeField] Text btnText;
    [SerializeField] Text btnWorkersAmountText;
    [SerializeField] BuildWorker buildRegWorker;
    float timeStart;
	// Use this for initialization
	void Start () 
    {
      playerbase = GetComponent<Base>();
	}
	
	// Update is called once per frame
	void Update () {
        btnWorkersAmountText.text = playerbase.getExistingworkersAmount() + "/" + playerbase.getMaxWorkerAmountInLevel();
        //Main timer countdown processs
        if (startCountdown)
        {
            //The system tryes to subract from current starting timer value...
            timeStart -= Time.deltaTime;
            //When it subracts, then the system rounds up the value when it will try to outputthe current time's values.
           // textBox.text = Mathf.Round(timeStart).ToString();
            btnText.text = Mathf.Round(timeStart).ToString();
            //Checks if the timeStart value reached 0, when true then the timer button becomes interactable, a message is printed, which indicates 
            //that the time coundown is done and resets the current timeStart variable's value and text. As well, it returns nothing, so it could cancel
            //the system for continuing any further.. 
            if (timeStart <= 0)
            {
                //resets the timeStart variable to value, which was at the start of the system.
    
                //Sets the current coundown signal the false, which would say that the system can not run and that it can run once the user clicked on the button.
                startCountdown = false;

                //Sets the state of button by making it interactable again
                startBtn.interactable = true;
                
                additionalWorker.interactable = true;
                
                playerbase.Spawning();

                btnText.text = "Create Worker \n"  + buildRegWorker.getMinNeededEnergonAmount() + " credits)";
                return;
            }
        }
	}
    //public function, which will be used for the timer button. It is used for starting the current timer.
    public void startTimer(float buildingStartTime)
    {       
        timeStart = buildingStartTime;  
        //Gives the signal to start the coundown timer by setting the current startCountdown value to true.
        startCountdown = true; 
        //Makes the current timer button to be not interactable
        startBtn.interactable = false;
        additionalWorker.interactable = false;
    }

}

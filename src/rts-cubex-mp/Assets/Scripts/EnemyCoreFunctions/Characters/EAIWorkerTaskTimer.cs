using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAIWorkerTaskTimer : MonoBehaviour
{
    [Header("Main configuration parameters")]

    //Starting time value, from where the timer will go to 0
    [SerializeField] float timeStart = 0;
    [SerializeField] bool timerFinished = false;    
    //Cached references
    //Variable for checking when the timer should start and when it can stop
    bool startCountdown = false;
    
    //Variable for storing the current startTime value, from where it tryed calculating time. Once the current time reached 0, then
    //this value's variable will be used for resetting the current time.
    [SerializeField] float startingTime = 0;

	
	// Update is called once per frame
	void Update () {

        //Main timer countdown processs
        if (startCountdown)
        {
            //The system tryes to subract from current starting timer value...
            timeStart -= Time.deltaTime;
            //When it subracts, then the system rounds up the value when it will try to outputthe current time's values.

            //Checks if the timeStart value reached 0, when true then the timer button becomes interactable, a message is printed, which indicates 
            //that the time coundown is done and resets the current timeStart variable's value and text. As well, it returns nothing, so it could cancel
            //the system for continuing any further.. 
            if (timeStart <= 0)
            {
                //resets the timeStart variable to value, which was at the start of the system.
                timeStart = startingTime;

                //Sets the current coundown signal the false, which would say that the system can not run and that it can run once the user clicked on the button.
                startCountdown = false;
                timerFinished = true;
                return;
            }
        }
	}
    //public function, which will be used for the timer button. It is used for starting the current timer.
    public void startTimer(float dTime)
    {
        if (timerFinished)
            timerFinished = false;
        //Gives the signal to start the coundown timer by setting the current startCountdown value to true.
        timeStart = dTime;
        startingTime = timeStart;
        startCountdown = true; 

    }
    public bool timerDone()
    {
        return timerFinished;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSesionTime : MonoBehaviour
{
    [SerializeField] Text counterText;
    [SerializeField] float seconds;
    [SerializeField] float minutes;
    [SerializeField] bool isTimeFinished = false;
    [SerializeField] float maxMinutes;
    [SerializeField] bool isTimeUnlimited = false;
    // Update is called once per frame
    void Update()
    {
        if (!isTimeUnlimited)
        {
            if (minutes >= maxMinutes)
            {
                minutes = maxMinutes;
                seconds = 0;
                counterText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
                isTimeFinished = true;
                return;
            }
            if (!isTimeFinished)
            {
                seconds = (int)(Time.timeSinceLevelLoad % 60f);
                minutes = (int)(Time.timeSinceLevelLoad / 60f) % 60;
                counterText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            }
        }
        else
        {
             minutes = maxMinutes;
             seconds = 0;
             counterText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
    public bool timeFinished()
    {
        return isTimeFinished;
    }
}

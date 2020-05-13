using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Hint parameters")]
    [SerializeField] string tutCompCode = "UTT";
    [SerializeField] int tutCompStatus = 0;
    [SerializeField] GameObject compMenuCompPanel;
    [SerializeField] GameObject hintTutPanel;
    [SerializeField] int maxDecisionCount = 2;
    private bool tutStatus = false;
    void Start()
    {
        //Check if the tutorial panel is completed...
        //Then status panel, which informs that the game guide is completed
        tutCompStatus = PlayerPrefs.GetInt(tutCompCode);
        tutStatus = tutCompStatus > 0;
        if (tutStatus)
        {
            compMenuCompPanel.SetActive(tutStatus);
        }
        else
        {
            //Else show panel, which shows a hit that the user needs to learn the tutorial guide to play the main game levels.
            hintTutPanel.SetActive(new RandomNumberGenerator().generateRandomNumber(0, maxDecisionCount) > 0);
        }
    }

    
}

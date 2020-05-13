using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Tutorial status parameters")]
    [SerializeField] int basicFunctionsTutorialStatus = 0;
    [SerializeField] int economyTutorialStatus = 0;
    [SerializeField] int troopTutorialStatus = 0;
    [SerializeField] int defenceTutorialStatus = 0;
    [SerializeField] int upgradeTutorialStatus = 0;
    [Header("UI Button configuration parameters")]
    [SerializeField] GameObject ecoTutBtn;
    [SerializeField] GameObject troopTutBtn;
    [SerializeField] GameObject defTutBtn;
    [SerializeField] GameObject upTutBtn;
    void Start()
    {
        var bfuncTutStat = PlayerPrefs.GetInt("BFTS");
        var eTutStat = PlayerPrefs.GetInt("ECTS");
        var tTutStat = PlayerPrefs.GetInt("TTS");
        var dTutStat = PlayerPrefs.GetInt("DMT");
        var uTutStat = PlayerPrefs.GetInt("UTT");

        //Checking player tutorials preferences parameters, if there int values are greater than zero, then setting true...
        ecoTutBtn.SetActive(bfuncTutStat > 0);
        troopTutBtn.SetActive(eTutStat > 0);
        defTutBtn.SetActive(tTutStat > 0);
        upTutBtn.SetActive(dTutStat > 0);

    }

  
}

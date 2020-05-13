using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowCredits : MonoBehaviour {
    private Base playerBase;
    private Research research;
    [SerializeField] Text credits;
    [SerializeField] Text researchLevel;
    [SerializeField] Text troopLevel;
    [SerializeField] Text heavyTroopsLevel;
    [SerializeField] ResearchConf oBGResearch;

    void Start () {
        if(FindObjectOfType<Base>() == null)
        {
           return;
        }
        else
        {
           playerBase = FindObjectOfType<Base>();
        }
        if(FindObjectOfType<Research>() == null)
        {
           return;
        }
        else
        {
           research = FindObjectOfType<Research>();
        }
       
    }

    void Update () {
            credits.text = "Credits: " + playerBase.getCreditsAmount();
            researchLevel.text = "Research level: " + oBGResearch.getResearchLevel();
            troopLevel.text = "Light Troop level: " + oBGResearch.getTroopLevel();
            heavyTroopsLevel.text = "Heavy Troop level: " + oBGResearch.getHeavyTroopLevel();
    }
}
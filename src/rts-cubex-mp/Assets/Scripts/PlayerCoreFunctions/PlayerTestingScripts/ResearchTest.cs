using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchTest : MonoBehaviour {
    [Header ("Research info")]
    [SerializeField] int researchLevel = 0;
    [SerializeField] int researchCost = 100;
    [SerializeField] int maxResearchLevel = 3;
    private BaseTest playerBase;
    public void increaseResearch () {
        playerBase = FindObjectOfType<BaseTest> ();
        if ((playerBase.getCreditsAmount () >= researchCost)&&(researchLevel<maxResearchLevel)) {
            playerBase.setCreditsAmount (playerBase.getCreditsAmount () - researchCost);
            researchLevel++;
        }
    }
    public int getResearch () {
        return researchLevel;
    }
}
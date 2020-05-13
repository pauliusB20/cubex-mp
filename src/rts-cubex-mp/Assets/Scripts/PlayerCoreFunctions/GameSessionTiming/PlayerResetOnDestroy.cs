using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerResetOnDestroy : MonoBehaviour
{
    [SerializeField] ResearchConf oBGResearch;
    private void OnDestroy() {
        // reset Troops Research
        oBGResearch.resetResearchLevel();
        oBGResearch.resetTroopLevel();
        oBGResearch.resetHeavyTroopLevel();
        oBGResearch.resetMaxHP();
        oBGResearch.resetHeavyMaxHP();
        oBGResearch.resetDamage();
        oBGResearch.resetHeavyDamage();
        oBGResearch.resetLightTroopScalingCoef();
        oBGResearch.resetHeavyTroopScalingCoef();
        oBGResearch.resetTroopResearchHealth();
        oBGResearch.resetBarrackHealth();
        // reset Building Research
        oBGResearch.resetBuildingResearchLevel();
        oBGResearch.resetMaxBuildingResearchLevel();
        oBGResearch.resetMinNeededEnergonAmountForResearch();
        oBGResearch.resetMinNeededCreditsAmountForResearch();
        oBGResearch.resetUpgradeBuildingResearchLevelHP();
        oBGResearch.resetPlayerScoreEarned();
        oBGResearch.resetBuildingResearchHealth();
        
    }
}

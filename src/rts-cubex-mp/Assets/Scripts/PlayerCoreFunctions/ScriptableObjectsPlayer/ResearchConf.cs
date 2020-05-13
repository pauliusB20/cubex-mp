using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="Research",menuName="PlayerCoreMForces",order=0)]
public class ResearchConf : ScriptableObject
{
    [Header("Troop Research and Barracks starting values")]
    [SerializeField] int sResearchLevel = 0;
    [SerializeField] int sTroopLevel = 1;
    [SerializeField] int sHeavyTroopLevel = 1;
    [SerializeField] private int sMaxHP = 100;
    [SerializeField] private int sHeavyMaxHP = 300;
    [SerializeField] int sDamage = 10;
    [SerializeField] private float sHeavyTroopScalingCoef = 300f;
    [SerializeField] private float sLightTroopScalingCoef = 100f;
    [SerializeField] private int sTroopResearchHealth = 250;
    [SerializeField] private int sBarrackHealth = 350;
    [Header("Troop Research and Barracks varying values")]
    [SerializeField] int researchLevel = 0;
    [SerializeField] int researchCost = 100;
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] int troopLevel = 1;
    [SerializeField] int heavyTroopLevel = 1;
    [SerializeField] int LightLevelCost = 200;
    [SerializeField] int heavyLevelCost = 200;
    [SerializeField] private int maxHP = 100;
    [SerializeField] private int heavyMaxHP = 300;
    [SerializeField] private int lightHealthIncrease = 30;
    [SerializeField] private int heavyHealthIncrease = 35;
    [SerializeField] private int lightDamageIncrease = 3;
    [SerializeField] private int heavyDamageIncrease = 2;
    [SerializeField] int damage = 10;
    [SerializeField] int heavyDamage = 10;
    [SerializeField] int maxLightTroopLevel = 10;
    [SerializeField] int maxHeavyTroopLevel = 10;
    [SerializeField] int maxResearchLevel = 3;
    [SerializeField] private float heavyTroopScalingCoef = 300f;
    [SerializeField] private float lightTroopScalingCoef = 100f;
    [SerializeField] private int troopResearchHealth = 250;
    [SerializeField] private int barrackHealth = 350;
    [Header("Buildings Research starting values")]
    [SerializeField] private int sBuildingResearchLevel;
    [SerializeField] private int sMaxLevelOfResearchCenter;
    [SerializeField] private int sMinNeededEnergonAmountForResearch; // for upgrade
    [SerializeField] private int sMinNeededCreditsAmountForResearch; // for upgrade
    [SerializeField] private int sUpgradeBuildingResearchLevelHP;
    [SerializeField] private int sPlayerScoreEarned;
    [SerializeField] private int sBuildingResearchHealth;
    [Header("Buildings Research varying values")]
    [SerializeField] private int buildingResearchLevel;
    [SerializeField] private int maxLevelOfResearchCenter;
    [SerializeField] private int minNeededEnergonAmountForResearch; // for upgrade
    [SerializeField] private int minNeededCreditsAmountForResearch; // for upgrade
    [SerializeField] private int upgradeBuildingResearchLevelHP;
    [SerializeField] private int playerScoreEarned;
    [SerializeField] private int buildingResearchHealth;
    public int getResearchCost(){
        return researchCost;
    }
    public float getSpawnDelay(){
        return spawnDelay;
    }
    public int getLightLevelCost(){
        return LightLevelCost;
    }
    public int getHeavyLevelCost(){
        return heavyLevelCost;
    }
    public int getLightHealthIncrease(){
        return lightHealthIncrease;
    }
    public int getHeavyHealthIncrease(){
        return heavyHealthIncrease;
    }
    public int getLightDamageIncrease(){
        return lightDamageIncrease;
    }
    public int getHeavyDamageIncrease(){
        return heavyDamageIncrease;
    }
    public int getMaxLightTroopLevel(){
        return maxLightTroopLevel;
    }
    public int getMaxHeavyTroopLevel(){
        return maxHeavyTroopLevel;
    }
    public int getMaxResearchLevel(){
        return maxResearchLevel;
    }
    public int getResearchLevel(){
        return researchLevel;
    }
    public int getTroopLevel(){
        return troopLevel;
    }
    public int getHeavyTroopLevel(){
        return heavyTroopLevel;
    }
    public int getMaxHP(){
        return maxHP;
    }
    public int getHeavyMaxHP(){
        return heavyMaxHP;
    }
    public int getDamage(){
        return damage;
    }
    public int getHeavyDamage(){
        return heavyDamage;
    }
    public float getHeavyTroopScalingCoef(){
        return heavyTroopScalingCoef;
    }
    public float getLightTroopScalingCoef(){
        return lightTroopScalingCoef;
    }
    public int getTroopResearchHealth(){
        return troopResearchHealth;
    }
    public int getBarrackHealth(){
        return barrackHealth;
    }
    public void resetTroopResearchHealth(){
        troopResearchHealth = sTroopResearchHealth ;
    }
    public void resetBarrackHealth(){
        barrackHealth = sBarrackHealth;
    }
    public void setResearchLevel(int x){
        researchLevel+=x;
    }
    public void resetResearchLevel(){
        researchLevel = sResearchLevel;
    }
    public void setTroopLevel(int x){
        troopLevel+=x;
    }
    public void resetTroopLevel(){
        troopLevel = sTroopLevel;
    }
    public void setHeavyTroopLevel(int x){
        heavyTroopLevel+=x;
    }
    public void resetHeavyTroopLevel(){
        heavyTroopLevel = sHeavyTroopLevel;
    }
    public void setMaxHP(int x){
        maxHP+=x;
    }
    public void resetMaxHP(){
        maxHP = sMaxHP;
    }
    public void setHeavyMaxHP(int x){
        heavyMaxHP+=x;
    }
    public void resetHeavyMaxHP(){
        heavyMaxHP = sHeavyMaxHP;
    }
    public void setDamage(int x){
        damage+=x;
    }
    public void setHeavyDamage(int x){
        heavyDamage+=x;
    }
    public void resetDamage(){
        damage = sDamage;
    }
    public void resetHeavyDamage(){
        heavyDamage = sDamage;
    }
    public void setLightTroopScalingCoef(float x){
        lightTroopScalingCoef+=x;
    }
    public void resetLightTroopScalingCoef(){
        lightTroopScalingCoef = sLightTroopScalingCoef ;
    }
    public void setHeavyTroopScalingCoef(float x){
        heavyTroopScalingCoef+=x;
    }
    public void resetHeavyTroopScalingCoef(){
        heavyTroopScalingCoef = sHeavyTroopScalingCoef;
    }
    // geteriai building research
    public int getBuildingResearchLevel()
    {
        return buildingResearchLevel;
    }
    public int getMaxBuildingResearchLevel()
    {
        return maxLevelOfResearchCenter;
    }
    public int getMinNeededEnergonAmountForResearch()
    {
        return minNeededEnergonAmountForResearch;
    }
    public int getMinNeededCreditsAmountForResearch()
    {
        return minNeededCreditsAmountForResearch;
    }
    public int getUpgradeBuildingResearchLevelHP()
    {
        return upgradeBuildingResearchLevelHP;
    }
    public int getPlayerScoreEarned()
    {
        return playerScoreEarned;
    }
    public int getBuildingResearchHealth()
    {
        return buildingResearchHealth;
    }
    // seteriai einamom reiksmems
    public void setBuildingResearchLevel(int newLevel)
    {
       buildingResearchLevel = newLevel;
    }
    public void setMinNeededEnergonAmountForResearch(int newAmount)
    {
       minNeededEnergonAmountForResearch = newAmount;
    }
    public void setMinNeededCreditsAmountForResearch(int newAmount2)
    {
        minNeededCreditsAmountForResearch = newAmount2;
    }
    public void setUpgradeBuildingResearchLevelHP(int newHpAm)
    {
        upgradeBuildingResearchLevelHP = newHpAm;
    }
    public void setPlayerScoreEarned(int newScore)
    {
        playerScoreEarned = newScore;
    }
    public void setBuildingResearchHealth(int newHealth)
    {
        buildingResearchHealth = newHealth;
    }
    // reiskmiu resetinimas
    public void resetBuildingResearchLevel()
    {
        buildingResearchLevel = sBuildingResearchLevel;
    }
    public void resetMaxBuildingResearchLevel()
    {
        maxLevelOfResearchCenter = sMaxLevelOfResearchCenter;
    }
    public void resetMinNeededEnergonAmountForResearch()
    {
        minNeededEnergonAmountForResearch = sMinNeededEnergonAmountForResearch;
    }
    public void resetMinNeededCreditsAmountForResearch()
    {
        minNeededCreditsAmountForResearch = sMinNeededCreditsAmountForResearch;
    }
    public void resetUpgradeBuildingResearchLevelHP()
    {
        upgradeBuildingResearchLevelHP = sUpgradeBuildingResearchLevelHP;
    }
    public void resetPlayerScoreEarned()
    {
        playerScoreEarned = sPlayerScoreEarned;
    }
    public void resetBuildingResearchHealth()
    {
        buildingResearchHealth = sBuildingResearchHealth;
    }
    public void setSpawnDelay(float x){
        spawnDelay=x;
    }
    public void setLightLevelCost(int x){
        LightLevelCost=x;
    }
    public void setHeavyLevelCost(int x){
        heavyLevelCost=x;
    }
    public void setLightHealthIncrease(int x){
        lightHealthIncrease=x;
    }
    public void setHeavyHealthIncrease(int x){
        heavyHealthIncrease=x;
    }
    public void setLightDamageIncrease(int x){
        lightDamageIncrease=x;
    }
    public void setHeavyDamageIncrease(int x){
        heavyDamageIncrease=x;
    }
    public void setMaxLightTroopLevel(int x){
        maxLightTroopLevel=x;
    }
    public void setMaxHeavyTroopLevel(int x){
        maxHeavyTroopLevel=x;
    }
    public void setMaxResearchLevel(int x){
        maxResearchLevel=x;
    }

}

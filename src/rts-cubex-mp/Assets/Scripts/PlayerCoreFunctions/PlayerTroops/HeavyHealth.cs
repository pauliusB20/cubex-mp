using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeavyHealth : MonoBehaviour {
    [SerializeField] private int unitHP;
    public GameObject health;
    [SerializeField] Image healthBarForeground;
    [SerializeField] Image healthBarBackground;
    [SerializeField] int scoreForEnemy = 20;

    [SerializeField] int regenerationAmount = 3;
    private bool isShot = false;
    private Base playerBase;
    [SerializeField] int TroopWeight=2;
    [SerializeField] ResearchConf upgrade;
    void Start () {
        health.SetActive (false);
        if(FindObjectOfType<Base>() == null)
        {
           return;
        }
        else
        {
           playerBase = FindObjectOfType<Base>();
        }
        unitHP = upgrade.getHeavyMaxHP ();
        healthBarForeground.fillAmount = unitHP / upgrade.getHeavyTroopScalingCoef();
    }
    void Update () {
        if (unitHP < upgrade.getHeavyMaxHP ()) {
            health.SetActive (true);
            StartCoroutine (RegenerateHealth ());
        }
        if (unitHP >= upgrade.getHeavyMaxHP ()) {
            health.SetActive (false);
        }
        healthBarForeground.fillAmount =unitHP / upgrade.getHeavyTroopScalingCoef();
        if (unitHP <= 0) {
            Destroy (gameObject);
            playerBase.addPlayerTroopsAmount(-TroopWeight);
            var var= upgrade.getHeavyTroopLevel();
            if(var==0){
                FindObjectOfType<GameSession>().AddEnemyScorePoints(scoreForEnemy);
            }
            else if (var==1){
                FindObjectOfType<GameSession>().AddEnemyScorePoints(2*scoreForEnemy);
            }
            else {
                FindObjectOfType<GameSession>().AddEnemyScorePoints(3*scoreForEnemy);
            }
        }
    }
    public void decreaseHealth (int damage) {
        unitHP -= damage;
        healthBarForeground.fillAmount =unitHP / upgrade.getHeavyTroopScalingCoef();
        isShot = true;
    }
    public void setHP (int HP) {
        unitHP += HP;
    }
    IEnumerator RegenerateHealth () {
        int x = unitHP;
        yield return new WaitForSeconds (15);
        if (x == unitHP) {
            isShot = false;
            while (unitHP < upgrade.getHeavyMaxHP () && !isShot) {
                unitHP += regenerationAmount;
                yield return new WaitForSeconds (0.3f);
            }
        }
    }
}
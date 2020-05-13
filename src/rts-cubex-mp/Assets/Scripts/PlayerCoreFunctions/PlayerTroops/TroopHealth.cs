using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopHealth : MonoBehaviour {
    [SerializeField] private int unitHP;
    public GameObject health;
    [SerializeField] Image healthBarForeground;
    [SerializeField] Image healthBarBackground;
    [SerializeField] ResearchConf upgrade;
    private bool isShot = false;
    [SerializeField] int scoreForEnemy = 10;
    [SerializeField] int regenerationAmount = 1;
    private bool oneTimeGetHP = false;
    private Base playerBase;
    [SerializeField] int TroopWeight=1;
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
        //upgrade = FindObjectOfType<Research> ();
        //unitHP = upgrade.getMaxHP ();
        //healthBar.sizeDelta = new Vector2 (unitHP * scalingCoef, healthBar.sizeDelta.y);
        //healthBarForeground.fillAmount = unitHP/upgrade.getLightTroopScalingCoef();
    }
    void Update () {
        if (FindObjectOfType<Research> () != null) {
            if (!oneTimeGetHP) {
                oneTimeGetHP = true;
                unitHP = upgrade.getMaxHP ();
            }
            healthBarForeground.fillAmount = unitHP / upgrade.getLightTroopScalingCoef ();
            if (unitHP < upgrade.getMaxHP ()) {
                health.SetActive (true);
                StartCoroutine (RegenerateHealth ());
            }
            if (unitHP >= upgrade.getMaxHP ()) {
                health.SetActive (false);
            }
            //healthBarForeground.fillAmount = unitHP/scalingCoef;
            if (unitHP <= 0) {
                Destroy (gameObject);
                playerBase.addPlayerTroopsAmount(-TroopWeight);
                var var = upgrade.getTroopLevel ();
                if (var == 0) {
                    FindObjectOfType<GameSession> ().AddEnemyScorePoints (scoreForEnemy);
                } else if (var == 1) {
                    FindObjectOfType<GameSession> ().AddEnemyScorePoints (2 * scoreForEnemy);
                } else {
                    FindObjectOfType<GameSession> ().AddEnemyScorePoints (3 * scoreForEnemy);
                }
            }
        }
                else {
                if (unitHP < upgrade.getMaxHP ()) {
                    health.SetActive (true);
                    StartCoroutine (RegenerateHealth ());
                }
                if (unitHP >= upgrade.getMaxHP ()) {
                    health.SetActive (false);
                }
                healthBarForeground.fillAmount = unitHP/upgrade.getLightTroopScalingCoef ();
                if (unitHP <= 0) {
                    Destroy (gameObject);
                    playerBase.addPlayerTroopsAmount(-TroopWeight);
                }
            }
    }
    /* Health bar is hidden by default, it only appears when the units, structures HP is not full and after some time, if not attacked again,
    Health starts to regenerate slowly and when HP is full, the Health Bar again hides itself. Also, if HP decreases down to 0, the Health holder
    is destroyed (death) */
    public void decreaseHealth (int damage) {
        unitHP -= damage;
        if (FindObjectOfType<Research> () != null) {
            healthBarForeground.fillAmount = unitHP / upgrade.getLightTroopScalingCoef ();
        }
        else {
            healthBarForeground.fillAmount = unitHP / upgrade.getDamage();
        }
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
            if (FindObjectOfType<Research> () != null) {
                while (unitHP < upgrade.getMaxHP () && !isShot) {
                    unitHP += regenerationAmount;
                    yield return new WaitForSeconds (0.3f);
                }
            }
            else {
                while (unitHP < upgrade.getMaxHP() && !isShot) {
                    unitHP += regenerationAmount;
                    yield return new WaitForSeconds (0.3f);
                 }
            }
        }
    }
}
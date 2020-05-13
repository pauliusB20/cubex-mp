using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretHealth : MonoBehaviour {
    [SerializeField] private int turretHP;
    [SerializeField] private int maxHP;

    public GameObject health;
    [SerializeField] Image healthBarForeground;
    [SerializeField] Image healthBarBackground;
     float descreaseHealthBarOfStructure;
    [SerializeField] int regenerateHpAmount;
    private bool isShot = false;
    void Start () {
        health.SetActive (false);
        descreaseHealthBarOfStructure = 0;
    }
    void Update () {
        if (turretHP < maxHP) {
            health.SetActive (true);
            StartCoroutine (RegenerateHealth ());
        }
        if (turretHP >= maxHP) {
            health.SetActive (false);
        }
        if (turretHP <= 0) {
            Destroy (gameObject);
        }
    }
    public void decreaseHealth (int damagePoints) {
         // is pradziu mes turime surasi koks yra tas procentas damage nuo kazkokio pastato hp
       // descreaseHealthBarOfStructure = Math.Round((((damagePoints*100)/healthOfStructureOriginal) / 100),5);
        descreaseHealthBarOfStructure = (((float)damagePoints * 100) / (float)maxHP) / 100;
        
       // Debug.Log(System.Math.Round(descreaseHealthBarOfStructure,5));
        
        turretHP -= damagePoints;  //  int 
       // health2 -= (float)damagePoints; // 265 // float
        healthBarForeground.fillAmount -= descreaseHealthBarOfStructure;  // 264.9
        
        isShot = true;

        descreaseHealthBarOfStructure = 0;

       // damagePoints += 20;
    }
    
    IEnumerator RegenerateHealth () {
        int x = turretHP;
        yield return new WaitForSeconds (15);
        if (x == turretHP) {
            isShot = false;
            while (turretHP < maxHP && !isShot) {
                turretHP += regenerateHpAmount;
                healthBarForeground.fillAmount += ((((float)regenerateHpAmount * 100) / (float)maxHP) / 100);
                yield return new WaitForSeconds (0.3f);
            }
        }
    }
    public int getTurretHealth()
    {
        return maxHP;
    }
    public void setTurretHealth(int HP)
    {
        maxHP = HP;
    }
    public int getCurrentTurretHealth()
    {
        return turretHP;
    }
    public void setHP (int HP) {
        turretHP = HP;
    } // for upgrading turret hp
}
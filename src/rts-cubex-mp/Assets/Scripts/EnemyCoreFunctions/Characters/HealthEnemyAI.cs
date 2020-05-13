using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthEnemyAI : MonoBehaviour
{
    [SerializeField] int healthOriginal;
    [SerializeField] GameObject healthBar;
    [SerializeField] Image healthBarForeground;
    [SerializeField] Image healthBarBackground;
    private bool isShot = false;
    float descreaseHealthBar;
    float increaseHealthBar;
    [SerializeField] int health;
    [SerializeField] int regenerateHpAmount;
    //int damagePoints = 15;
    void Start()
    {
       health = healthOriginal;
       healthBar.SetActive(true);
       descreaseHealthBar = 0;
       increaseHealthBar = 0;

    }
    void Update () 
    {
        if (health < healthOriginal) 
        {
          StartCoroutine(RegenerateHealth());
        }
        if (health <= 0)
        {
         Destroy(gameObject);
        }
    }

    public void decreaseHealth(int damagePoints)
    {
        // is pradziu mes turime surasi koks yra tas procentas damage nuo kazkokio pastato hp
       // descreaseHealthBarOfStructure = Math.Round((((damagePoints*100)/healthOfStructureOriginal) / 100),5);
        descreaseHealthBar = (((float)damagePoints * 100) / (float)healthOriginal) / 100;
        // Debug.Log("kakzkas0" + descreaseHealthBarOfStructure);
        // Debug.Log("Kazkas" + System.Math.Round(descreaseHealthBarOfStructure,5));
        
        health -= damagePoints;  //  int 
       // health2 -= (float)damagePoints; // 265 // float
        healthBarForeground.fillAmount -= descreaseHealthBar;  // 264.9
        
        isShot = true;

        descreaseHealthBar = 0;

       // damagePoints += 20;
    }
    IEnumerator RegenerateHealth () {
        int x = health;
        yield return new WaitForSeconds (15);
        if (x == health) {
            isShot = false;
            while (health < healthOriginal && !isShot) {
                health += regenerateHpAmount; // 300 // int
                increaseHealthBar = (((float)regenerateHpAmount * 100) / (float)healthOriginal) / 100;
                healthBarForeground.fillAmount += increaseHealthBar; // 299.7
                increaseHealthBar = 0;
                yield return new WaitForSeconds (0.3f);
            }
        }
    }
    public int getHealth()
    {
        return health;
    }
    public int getHealthOriginal()
    {
        return healthOriginal;
    }
    public void setHealthOriginal(int newHealth)
    {
        healthOriginal = newHealth;
    }
    public void setHealth(int newHealth)
    {
        healthOriginal = newHealth;
    }


    //Old Health script..
    // [SerializeField] int health = 100;
    // [SerializeField] RectTransform healthBar;

    // private int originalHealth;

    // private void Start() {
    //     originalHealth = health;
    // }
    // public void decreaseHealth(int damagePoints)
    // {
    //     health -= damagePoints;
    //      healthBar.sizeDelta=new Vector2(health*2,healthBar.sizeDelta.y);
    //     if (health <= 0)
    //     {
    //         Destroy(gameObject);
    //     }
    // }
    // public void setHealth(int hPointMax)
    // {
    //     this.health = hPointMax;
    // }
    // public int getHealth()
    // {
    //     return health;
    // }
}

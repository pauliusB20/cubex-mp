using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthOfRegBuilding : MonoBehaviour
{
    [SerializeField] int healthOfStructureOriginal; // variable which always holds the max value
    [SerializeField] int health; // variable which will be changed if player gets damage from enemy troops
    public GameObject healthOfBuilding;
    [SerializeField] Image healthBarForeground;
    [SerializeField] Image healthBarBackground;
    private bool isShot = false;
    float descreaseHealthBarOfStructure;
    float increaseHealthBarOfStructure;
    [SerializeField] int regenerateHpAmount;
    //int damagePoints = 15;
    void Start()
    {
       healthOfBuilding.SetActive(true);
       descreaseHealthBarOfStructure = 0;
       increaseHealthBarOfStructure = 0;

    }
    void Update () 
    {
     if (health < healthOfStructureOriginal) 
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
        descreaseHealthBarOfStructure = (((float)damagePoints * 100) / (float)healthOfStructureOriginal) / 100;
        
        health -= damagePoints;  //  int 
       // health2 -= (float)damagePoints; // 265 // float
        healthBarForeground.fillAmount -= descreaseHealthBarOfStructure;  // 264.9
        
        isShot = true;

        descreaseHealthBarOfStructure = 0;

       // damagePoints += 20;
    }
    IEnumerator RegenerateHealth () {
        int x = health;
        yield return new WaitForSeconds (15);
        if (x == health) {
            isShot = false;
            while (health < healthOfStructureOriginal && !isShot) {
                health += regenerateHpAmount; // 300 // int
                increaseHealthBarOfStructure = (((float)regenerateHpAmount * 100) / (float)healthOfStructureOriginal) / 100;
                healthBarForeground.fillAmount += increaseHealthBarOfStructure; // 299.7
                increaseHealthBarOfStructure = 0;
                yield return new WaitForSeconds (0.3f);
            }
        }
    }
    public int getHealth()
    {
        return health;
    }
    public int getHealthOfStructureOriginal()
    {
        return healthOfStructureOriginal;
    }
    public void setHealthOfStructureOriginal(int newHealth)
    {
        healthOfStructureOriginal = newHealth;
    }
    public void setHealth(int newHealth)
    {
        health = newHealth;
    }
}

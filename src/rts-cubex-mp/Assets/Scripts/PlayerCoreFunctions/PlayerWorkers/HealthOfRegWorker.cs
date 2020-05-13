using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthOfRegWorker : MonoBehaviour
{   
    [SerializeField] int healthOfStructureOriginal; 
    [SerializeField] int health;
    public GameObject healthOfBuilding;
    [SerializeField] Image healthBarForeground;
    [SerializeField] Image healthBarBackground;
    private bool isShot = false;
    float descreaseHealthBarOfStructure;
    [SerializeField] int regenerateHpAmount;
    void Start()
    {
       healthOfBuilding.SetActive(true);
       descreaseHealthBarOfStructure = 0;       
       
       GetComponent<ItemDescription>().setHP(health);
    }
    void Update () 
    {
     if (health < healthOfStructureOriginal) 
        {
          StartCoroutine(RegenerateHealth());
        }
     if (health <= 0) // if health of Worker is less or equal to 0 then baze variables which holds information about workers amount have to be changed
        {
         var playerbase = FindObjectOfType<Base>();
         playerbase.setExistingworkersAmount(playerbase.getExistingworkersAmount() - 1);
         playerbase.setworkersAmount(playerbase.getworkersAmount() - 1);   
         Destroy(gameObject);
             if(playerbase.getExistingworkersAmount() < playerbase.getMaxWorkerAmountInLevel())
             {
                playerbase.setWorkersAmountState(true);
             }  
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

      //  damagePoints += 20;
    }
    IEnumerator RegenerateHealth () {
        int x = health;
        yield return new WaitForSeconds (15);
        if (x == health) {
            isShot = false;
            while (health < healthOfStructureOriginal && !isShot) {
                health += regenerateHpAmount; // 300 // int
                healthBarForeground.fillAmount += ((((float)regenerateHpAmount * 100) / (float)healthOfStructureOriginal) / 100); // 299.7
                yield return new WaitForSeconds (0.1f);
            }
        }
    }
    public int getHealth1()
    {
        return health;
    }
    public int getHealthOfStructureOriginal1()
    {
        return healthOfStructureOriginal;
    }
    public void setHealthOfStructureOriginal1(int newHealth)
    {
        healthOfStructureOriginal = newHealth;
    }
    public void setHealth1(int newHealth)
    {
        health = newHealth;
    }
    public bool shot()
    {
        return isShot;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorkerTestingHealth : MonoBehaviour
{
    [SerializeField] int healthOfStructureOriginal = 100;
    [SerializeField] int health = 100;
    public GameObject healthOfBuilding;
    [SerializeField] Image healthBarForeground;
    [SerializeField] Image healthBarBackground;
    private bool isShot = false;
    float descreaseHealthBarOfStructure;
    [SerializeField] int regenerateHpAmount;
    //int damagePoints = 15;
    void Start()
    {
       healthOfBuilding.SetActive(true);
       descreaseHealthBarOfStructure = 0;
    }
    void Update () 
    {
     if (health < healthOfStructureOriginal) 
        {
          StartCoroutine(RegenerateHealth());
        }
     if (health <= 0)
        {
         var playerbase = FindObjectOfType<Base>();
         playerbase.setExistingworkersAmount(playerbase.getExistingworkersAmount() - 1);
         playerbase.setworkersAmount(playerbase.getworkersAmount() - 1);   
         Destroy(gameObject);
        }
    }

    public void decreaseHealth(int damagePoints)
    {
        // is pradziu mes turime surasi koks yra tas procentas damage nuo kazkokio pastato hp
        descreaseHealthBarOfStructure = (((float)damagePoints * 100) / (float)healthOfStructureOriginal) / 100;
        
        Debug.Log(System.Math.Round(descreaseHealthBarOfStructure,5));
        //Mathf.Round(descreaseHealthBarOfStructure,5);
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
                Debug.Log("Current health: " + health);
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

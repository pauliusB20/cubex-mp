using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyment : MonoBehaviour
{
    [SerializeField] int returnEnergonAmount; // returning values of player resources if object is desroyed by him
    [SerializeField] int returnCreditsAmount;
    private Base playerbase;
    HealthOfRegBuilding buildingHealth;
    // Start is called before the first frame update
    void Start()
    {
      if(FindObjectOfType<Base>() == null)
      {
        return;
      }  
      else
      {
        playerbase = FindObjectOfType<Base>();  
      }
      
      buildingHealth = GetComponent<HealthOfRegBuilding>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void destroyGameStrucuture() // destroy turret // desroy defensive wall // destroy barrack
    {
        Destroy(gameObject);
        playerbase.setEnergonAmount(playerbase.getEnergonAmount() + returnEnergonAmount);
        playerbase.setCreditsAmount(playerbase.getCreditsAmount() + returnCreditsAmount);
    }  
    public void destroyCollecotor() // destroy collector
    {   
        buildingHealth.setHealth(0);
        Destroy(gameObject, 1f);
        playerbase.setEnergonAmount(playerbase.getEnergonAmount() + returnEnergonAmount);
        playerbase.setCreditsAmount(playerbase.getCreditsAmount() + returnCreditsAmount);
        playerbase.setworkersAmount(playerbase.getworkersAmount() + 1);
    } 
    public void destroyResearchCenters() // destroy building/troops research centre
    {   
        buildingHealth.setHealth(0);
        playerbase.setEnergonAmount(playerbase.getEnergonAmount() + returnEnergonAmount);
        playerbase.setCreditsAmount(playerbase.getCreditsAmount() + returnCreditsAmount);
    } 
}

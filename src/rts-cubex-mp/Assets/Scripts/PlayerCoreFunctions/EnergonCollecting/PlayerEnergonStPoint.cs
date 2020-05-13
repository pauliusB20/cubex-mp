using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergonStPoint : MonoBehaviour
{

    //class, which checks how much did the player worker brought to the base
    
    //Value which stores how much energon did the player worker brought
    [SerializeField] int energon;
    private Base playerbase;
    private void Start() {
        energon = 0;
        if(FindObjectOfType<Base>() == null)
        {
           return;
        }
        else
        {
           playerbase = FindObjectOfType<Base>();
        }
    }

    //Systtem, which checks how much energon did the player worker brought
   private void OnTriggerEnter(Collider other) {
       if (other.gameObject.tag == "Worker")
       {
           //It takes the player object class
           var workerNav = other.gameObject.GetComponent<Worker>();
           if (workerNav.getEnergonAmount() > 0)    
           {
               //When we access, we add the value to the base energon storage variable
               
            // playerbase.addEnergonAmountToBase(workerNav.getEnergonAmount());
             playerbase.setEnergonAmount(playerbase.getEnergonAmount() + workerNav.getEnergonAmount());
             //When the storage process is done, we set the current enrgon value, which the worker is carrying to 0. 
            // After that, we send the worker back 
             //to the collector
             workerNav.setEnergonInWorker(0); 
           }
            //It also trys to find out where is the collector in the game
            //For making this search system more univeral, it would be a great idea for adding these 
            //Colelctors to a array datastructure and then using a for loop to iterate through them and store positions in the a array datastructure
            //which type could be CollectorManager[] cm = FindObjectOfType<CollectorManager>();
           //var collector = FindObjectOfType<Energon>();
            //Process for sending the player worker back to the collector position, this is used because then the colelcting process will be repeated
            //automatically
           workerNav.SetDestination(workerNav.getEnergonStationPozition());
       }
   }
   
   //For method for returning the storage position (x,y,z)
   public Vector3 getStoragePointPoisition() // sitoje tasku masyva
   {
       return transform.position;
   }
   public int getTakenEnergonAmount()
   {
   return energon;
   }
   public void setTakenEnergonAmount(int energonAM)
   {
       energon = energonAM;
   }
}

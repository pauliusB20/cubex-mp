using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildMiningStation : MonoBehaviour
{
    bool canBuild = false;
    private Base playerbase;
    private CollectorBuild collectorBuild;
    private string index = ""; // indexing each deposit unit
    private void Start() {
        if(FindObjectOfType<Base>() == null)
        {
          return;
        }
        else
        {
           playerbase = FindObjectOfType<Base>();
        }
        
       
    }
    void Update()
    {
        //Checks if there aare there any workers in the base area
        if(playerbase.getworkersAmount() > 0 ) // man nereikia nes as turiu skaitliuka, kuris atsako uz visus laisvu workeriu kieki.
        //if (FindObjectsOfType<Worker>().Length > 0)
         canBuild = true;
    }
    void OnMouseDown() 
    {
        if(FindObjectOfType<CollectorBuild>() != null)
        {
          collectorBuild = FindObjectOfType<CollectorBuild>();
        }
        else
        {
          return;  
        }

        if (!canBuild || !collectorBuild.getClickedBuildCollectorBtnState())
        {
           // Debug.Log("No workers found or build button was not clicked. So can't build a mining station here!");
            return;
        }

        playerbase.setCreditsAmount(playerbase.getCreditsAmount()-collectorBuild.getMinNeededCreditsAmountForCreditsCollector()); // uzsetiname naujas reksmes 
        playerbase.setEnergonAmount(playerbase.getEnergonAmount()-collectorBuild.getMinNeededEnergonAmountForEnergonCollector()); // uzsetiname naujas reksmes
        playerbase.setworkersAmount(playerbase.getworkersAmount()-1); // atimame is skailiuko vieneta
        collectorBuild.setCollectorStructureBuilt(true); 
        //Since there are workers and the button was clicked 
        //Then the system finds a worker which is available
        //If it finds a worker which is available, then this DEPOSIT, which has this class, sends it's coordinates to the near by worker
        //using the SetDestination() method which is in the PlayerWorker class
        var playerWorkers = GameObject.FindGameObjectsWithTag("Worker");
        for (int i = 0; i < playerWorkers.Length; i++)
        {
            if (!playerWorkers[i].GetComponent<Worker>().isWorkerAssigned() && System.Math.Round(playerWorkers[i].GetComponent<WorkerLifeCycle>().getWorkerJobLeft(),2) >= Math.Round(collectorBuild.getNeededWorkerJobAmountForStrucuture(),2)) // jeigu workeris yra laisvas tai jo reiksme yra false, kitu atveju bus true
            {
                //Variable for knowing, what's the current position of this object, which has this class
                Vector3 thisDepositPosition = transform.position;
                playerWorkers[i].GetComponent<Worker>().SetDestination(thisDepositPosition);
                playerWorkers[i].GetComponent<Worker>().setWorkerState(true);// kai paspaudziu tai workeris yra uzimtas
                index = playerWorkers[i].GetComponent<Worker>().getWorkerIndex();
                return;
            }               
        }
        //IF there aren't any available workers, then this message will be printed
       // Debug.Log("No workers found can't build a mining station here!");
    }
    public string getIndex()
    {
        return index;
    } 
}


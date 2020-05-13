using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Energon : MonoBehaviour
{  
    [Header("Tool display parameters")]
    [SerializeField] GameObject CollectorScreen;
    [SerializeField] Text energonLeft;
    [SerializeField] Text collectorHealth; 
    [SerializeField] int availableEnergon = 360; // kiek isviso energono gali pasiimti is kolektoriaus vienas workeris.
    HealthOfRegBuilding energonCollectorHealth;
    string workerIndex;
    [SerializeField] int takenEnergonAmount;
    bool savedIndex = false;
    private Base playerbase;
    void Start()
    {
        energonCollectorHealth = GetComponent<HealthOfRegBuilding>();
        CollectorScreen.SetActive(false);
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
        energonLeft.text = "Energon left :" + availableEnergon;
        collectorHealth.text = "Collector Health :" + energonCollectorHealth.getHealth() + " / " + energonCollectorHealth.getHealthOfStructureOriginal();
        if(energonCollectorHealth.getHealth() <= 0)
        {
            var playerWorkers = GameObject.FindGameObjectsWithTag("Worker");
            for(int i = 0; i < playerWorkers.Length; i++)
            {
               if(workerIndex == playerWorkers[i].GetComponent<Worker>().getWorkerIndex())
               {
                  playerWorkers[i].GetComponent<Worker>().setWorkerState(false);
                  playerWorkers[i].GetComponent<Worker>().SetDestination(transform.position);
                  return;
               }
            }
        }
    }
    void OnMouseDown()
    {
        CollectorScreen.SetActive(true);
    }
    private void OnTriggerEnter(Collider other) 
    {
        var enteredWorker = other.gameObject;
        if(!savedIndex && enteredWorker.tag == "Worker")
        {
            // issaugo kolektoroiuje dirbancio workerio prie kolektoriaus idexa
            workerIndex = enteredWorker.GetComponent<Worker>().getWorkerIndex();
            savedIndex = true;
        }
        
        if (other.gameObject.tag == "Worker")
        {
            var basePosition = FindObjectOfType<PlayerEnergonStPoint>();
            var workerNav = other.gameObject.GetComponent<Worker>();
            if(availableEnergon <= 0)
            {
            Destroy(gameObject);
            playerbase.setworkersAmount(playerbase.getworkersAmount()+1);
           
            enteredWorker.GetComponent<Worker>().setWorkerState(false); // workeris yra laisvas vel.
            enteredWorker.GetComponent<WorkerLifeCycle>().decreaseWorkerLife();
            return;
            }
            int temp = availableEnergon;
            availableEnergon--;
            workerNav.setEnergonInWorker(takenEnergonAmount);
            workerNav.setEnergonStationPozition(transform.position);
            workerNav.SetDestination(basePosition.getStoragePointPoisition());
            }
        else
        {
            return;
        }
    }
    public Vector3 getCollectorPosition()
    {
        return transform.position;
    }
     
    public string getWorkerIndex() // skirti uzfiksuoti kai spawnina workeris colektoiu koks workeris prie kurio kolektoriaus dirba
    {
        return workerIndex;
    }

    public void setWorkerIndex(string index)
    {
        workerIndex = index;
    } 
}



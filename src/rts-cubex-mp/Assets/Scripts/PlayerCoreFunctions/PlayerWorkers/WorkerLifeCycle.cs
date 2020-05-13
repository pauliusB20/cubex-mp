using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorkerLifeCycle : MonoBehaviour
{
    [SerializeField] Image WorkerLifeCycleBar; 
    [SerializeField] GameObject WorkerLifeCyclePanel;
    bool workerLifeCyclePanelStatus;
    [SerializeField] float totalJobPercentage = 99f;
    [SerializeField] float workerJobLeft = 99f; // variable which will hold how much persange is left of all worker job amount;
    [SerializeField] int workerJobsAmount = 3;
    [SerializeField] float decreaseLife;
    void Start()
    {
      workerLifeCyclePanelStatus = true;
      WorkerLifeCyclePanel.SetActive(true);
      totalJobPercentage /= 100;
      workerJobLeft /= 100;
      decreaseLife = totalJobPercentage / (float)workerJobsAmount;
    }
    void Update()
    {
      if(getWorkerLifeCyclePanelState())
      {
       WorkerLifeCyclePanel.SetActive(true);
      }
      else
      {
       WorkerLifeCyclePanel.SetActive(false);
      }
      
    }
    public void decreaseWorkerLife()
    {

    WorkerLifeCycleBar.fillAmount -= decreaseLife;
    workerJobLeft -= decreaseLife; 
    if(workerJobLeft <= 0.00f)
    { 
      var playerbase = FindObjectOfType<Base>();
      playerbase.setworkersAmount(playerbase.getworkersAmount()-1); // cia laisvu workeriu skaicius
      playerbase.setExistingworkersAmount(playerbase.getExistingworkersAmount()-1); // cia esamu zaidejo bazeje workeriu skaicius (laisvi ir ne laisvi)
      Destroy(gameObject);
         if(playerbase.getExistingworkersAmount() < playerbase.getMaxWorkerAmountInLevel())
         {
            playerbase.setWorkersAmountState(true);
         }  
      return;
    }
    
    }
    public float getWorkerJobLeft()
    {
      return workerJobLeft;
    }
    // geteriai skirti workerio life panelei
    public void setWorkerLifeCyclePanelState(bool state)
    {
      workerLifeCyclePanelStatus = state;
    }
    public bool getWorkerLifeCyclePanelState()
    {
      return workerLifeCyclePanelStatus;
    }
}

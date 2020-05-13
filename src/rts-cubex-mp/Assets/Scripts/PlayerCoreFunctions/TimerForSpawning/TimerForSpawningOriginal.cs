using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerForSpawningOriginal : MonoBehaviour
{ 
    // 0 - barako atskaitos laikas;
    // 1 - kolektoriaus atskaitos laikas;
    // 2 - turret atskaitos laikas; 
    // 3 - research centro ataskaitos laikas;
    // 4 - troop research center;
    // 5 - walls;
     [Header("Main Timer configuration parameters")]
    [SerializeField] float[] timereferencetasks; // masyvas skirtas saugoti timerio laika skirtingiems buildingams.
    bool startCountdown = false;
    [SerializeField] GameObject barrack; // cia ikelsime pacius buildingu objektus
    [SerializeField] GameObject turret;
    [SerializeField] GameObject collector;
    [SerializeField] GameObject researchCenter;
    [SerializeField] GameObject troopsResearchCenter;
    [SerializeField] GameObject playerDefenceWall;
    float startingTime = 0;
    //float startingTime1 = 0;
    float fillingvariable;
    //string buildingType;
    int timeArrayPozition;
    Vector3 pozition;
    private Base playerbase;
    [SerializeField] BarrackBuild barrackbuild;
    [SerializeField] TurretBuild turretbuild;
    [SerializeField] CollectorBuild coollectorbuild;
    [SerializeField] Text timeLeft;

    [SerializeField] int playerEarnedPoints = 10;
   
    [SerializeField] Image buildingBar; // building bar
   
    private Worker playerworker;
    private WorkerLifeCycle workerLife;
    [SerializeField] GameObject buildingbar;
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
        workerLife = GetComponent<WorkerLifeCycle>();
        playerworker = GetComponent<Worker>();
        buildingbar.SetActive(false);
        fillingvariable = 0;
    }

    void Update()
    {
       //Debug.Log(Time.deltaTime);
        
        if (startCountdown)
        {          
            buildingbar.SetActive(true);
            fillingvariable += Time.deltaTime;
            buildingBar.fillAmount = fillingvariable / timereferencetasks[timeArrayPozition];
            startingTime -= Time.deltaTime;
             if(startingTime >= 60.00f)
              {
                 timeLeft.text = Mathf.Round(startingTime / 60f) % 60 + " min " + Mathf.Round(startingTime % 60f ).ToString() + " sec";
              }
             else
              {
                 timeLeft.text = Mathf.Round(startingTime % 60f ).ToString() + " sec";
              }

            if (startingTime <= 0)
            {
                //resets the timeStart variable to value, which was at the start of the system.
                //startingTime = originalTime;
                
                //Sets the current coundown signal the false, which would say that the system can not run and that it can run once the user clicked on the button.
                startCountdown = false;
                buildingbar.SetActive(false);
                fillingvariable = 0;
                //Sets the state of button by making it interactable again
                //startBtn.interactable = true;
                //Resets the current button's text value, which can be defined in the inspector by using this class string variable nameBtn.
                //var playerworker = FindObjectsOfType<Worker>();
                var playerScorePoints = FindObjectOfType<GameSession>();
                switch(/*buildingType*/timeArrayPozition)
                {
                 case 0/*"barrack"*/:
                     Instantiate(barrack, pozition, Quaternion.identity);
                     if(playerScorePoints != null)
                     {
                         playerScorePoints.AddPlayerScorePoints(playerEarnedPoints);
                         playerScorePoints.addPlayerBarrackAmount(1);
                     }
                     FindObjectOfType<Base>().setworkersAmount(FindObjectOfType<Base>().getworkersAmount()+1);
                     playerworker.setWorkerState(false);
                     workerLife.decreaseWorkerLife();
                 break;
                 case 3/*"researchCenter"*/ :
                     Instantiate(researchCenter, pozition, Quaternion.identity);
                     if(playerScorePoints != null)
                     {
                         playerScorePoints.AddPlayerScorePoints(playerEarnedPoints);
                         playerScorePoints.addPlayerResearchAmount(1);
                     }
                     FindObjectOfType<Base>().setworkersAmount(FindObjectOfType<Base>().getworkersAmount() + 1);
                     playerworker.setWorkerState(false);
                     workerLife.decreaseWorkerLife();
                 break;
                 case 2/*"turret"*/:
                     Instantiate(turret, pozition, Quaternion.identity);
                     if(playerScorePoints != null)
                     {
                         playerScorePoints.AddPlayerScorePoints(playerEarnedPoints);
                         playerScorePoints.addPlayerTurretAmount(1);
                     }
                     FindObjectOfType<Base>().setworkersAmount(FindObjectOfType<Base>().getworkersAmount()+1);
                     playerworker.setWorkerState(false);
                     workerLife.decreaseWorkerLife();
                 break;
                 case 4/*"troopsReserach"*/:
                     Instantiate(troopsResearchCenter, pozition, Quaternion.identity);
                     if(playerScorePoints != null)
                     {
                        playerScorePoints.AddPlayerScorePoints(playerEarnedPoints);
                        playerScorePoints.addPlayerResearchAmount(1);
                     }
                     FindObjectOfType<Base>().setworkersAmount(FindObjectOfType<Base>().getworkersAmount() + 1);
                     workerLife.decreaseWorkerLife();
                     playerworker.setWorkerState(false); // statas reiskia kas workeris yra laisvas
                 break;
                 case 1/*"Depo"*/:
                     Instantiate(collector, pozition, Quaternion.identity);
                     if(playerScorePoints != null)
                     {
                         playerScorePoints.AddPlayerScorePoints(playerEarnedPoints);
                         playerScorePoints.addPlayerCollectorAmount(1);
                     }
                     workerLife.decreaseWorkerLife();
                     //playerworker.setWorkerState(false);
                 break;
                 case 5:
                 Instantiate(playerDefenceWall, pozition, Quaternion.identity);
                     if(playerScorePoints != null)
                     {
                        playerScorePoints.AddPlayerScorePoints(playerEarnedPoints);
                        playerScorePoints.addPlayerWallsAmount(1);
                     }
                     FindObjectOfType<Base>().setworkersAmount(FindObjectOfType<Base>().getworkersAmount() + 1);
                     workerLife.decreaseWorkerLife();
                     playerworker.setWorkerState(false); // statas reiskia kas workeris yra laisvas
                 break;
                }
                //Once the timer has finished counting, a message appear swhich says that the timer has finished counting
                //displayText.text = "Hurray! it is done!";
                //Then when this process has done counting, it return nothing, at system restarts.
                return;
            }
        }
    }
     public void startTimer(/*string order,*/ Vector3 poz, int arrayPosition)
     {
        startCountdown = true; 
        //buildingType = order;
        pozition = poz;
        timeArrayPozition = arrayPosition;
        switch(timeArrayPozition)
        {
         case 0:
            startingTime = timereferencetasks[timeArrayPozition];
         break;
         case 1:
            startingTime = timereferencetasks[timeArrayPozition];
         break;
         case 2:
            startingTime = timereferencetasks[timeArrayPozition];
         break;
         case 3:
            startingTime = timereferencetasks[timeArrayPozition];
         break;
         case 4:
            startingTime = timereferencetasks[timeArrayPozition]; 
         break;
         case 5:
            startingTime = timereferencetasks[timeArrayPozition]; 
         break;
         /* 
         case "barrack":
            startingTime1 = timereferencetasks[0];
            startingTime = timereferencetasks[0];
         break;
         case "turret":
            startingTime1 = timereferencetasks[2];
            startingTime = timereferencetasks[2];
         break;
         case "Depo":
            startingTime1 = timereferencetasks[1];
            startingTime = timereferencetasks[1];
         break;
         case "researchCenter":
            startingTime1 = timereferencetasks[3];
            startingTime = timereferencetasks[3];
         break;
         case "troopsReserach":
            startingTime1 = timereferencetasks[4];
            startingTime = timereferencetasks[4]; 
         break;
        }*/
        }
    }
} 


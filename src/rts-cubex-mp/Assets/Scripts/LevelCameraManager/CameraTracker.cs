using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTracker : MonoBehaviour
{
   [SerializeField] GameObject[] gameCameras;
   [SerializeField] string camCode;
   [SerializeField] bool hasTroopEntered = false;

   private void Update() {
       if (hasTroopEntered)
       {
           for (int cIndex = 0; cIndex < gameCameras.Length; cIndex++)
           {
               if (gameCameras[cIndex].GetComponent<AreaCamera>().getCameraCode() == camCode)
               {
                   gameCameras[cIndex].SetActive(true);
               }
               
           }
       }
   }

   public void setCamCode(string code)
   {
       camCode = code;
   }
   public void hasEnteredTheSwitchArea(bool state)
   {
        hasTroopEntered = state;
   }
}

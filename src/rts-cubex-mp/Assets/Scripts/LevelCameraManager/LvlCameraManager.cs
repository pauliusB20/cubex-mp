using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlCameraManager : MonoBehaviour
{
   [SerializeField] List <GameObject> currentLvlCameras;
   
   [SerializeField] string currentCameraCode = "M1";

   [SerializeField] int currentCameraIndex = 0;

   GameObject currentActiveCamera;
   [SerializeField] bool camerasSwitched = false;
   private void Start() {
       //PMBase is active..........
       //currentLvlCameras[currentCameraIndex].SetActive(true);
   }

   private void Update() {

       if (currentCameraCode != null && currentLvlCameras != null)
       {
            if (camerasSwitched)
            {
                for (int cameraIndex = 0; cameraIndex < currentLvlCameras.Count; cameraIndex++)
                {
                    if (currentLvlCameras[cameraIndex].GetComponent<AreaCamera>().getCameraCode() == currentCameraCode)
                    {
                            currentLvlCameras[cameraIndex].SetActive(true);
                            break;
                    }
                }
                camerasSwitched = false;
            }
        
            for (int cameraIndex = 0; cameraIndex < currentLvlCameras.Count; cameraIndex++)
            {
                if (currentLvlCameras[cameraIndex].GetComponent<AreaCamera>().getCameraCode() != currentCameraCode)
                {
                        currentLvlCameras[cameraIndex].SetActive(false);
                }
            }

       }
   }
   

    public void setCamerCode(string code)
    {
        currentCameraCode = code;
        camerasSwitched = true;
    }

    public string getCameraCode()
    {
            return currentCameraCode;
    }
}

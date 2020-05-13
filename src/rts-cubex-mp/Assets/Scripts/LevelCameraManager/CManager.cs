using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CManager : MonoBehaviour
{
    [SerializeField] List<GameObject> lvlCameras;
    [SerializeField] int currentCameraIndex = 0;
    public void goToCameraAt(int cIndex, bool setState)
    {
        currentCameraIndex = cIndex;
        lvlCameras[cIndex].SetActive(setState);
    }
   
}

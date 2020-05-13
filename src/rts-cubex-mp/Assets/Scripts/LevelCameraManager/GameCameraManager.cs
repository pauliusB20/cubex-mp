using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraManager : MonoBehaviour
{
    [SerializeField] List<GameObject> gameCameras;
    [SerializeField] int camListPosition = 0;

    public void goToRightCamera()
    {
        //Checks if the position has reached the last element in the camera list..
        if (camListPosition >= gameCameras.Count)
            camListPosition = 0;
        
        if (camListPosition > 0)
            gameCameras[camListPosition-1].SetActive(false);
        //Go to the right side of the list...
              
        gameCameras[camListPosition].SetActive(true);
        camListPosition++;
        //Checking if there was a camera before...
        
    } 
    public void goToLeftCamera()
    {
          //Checks if the position has reached the last element in the camera list..
        if (0 >= camListPosition)
            camListPosition = 0;
        //Checking if there was a camera before...
        if (camListPosition <= gameCameras.Count && 0 < camListPosition)
            gameCameras[camListPosition-1].SetActive(false);
        
        //Go to the right side of the list...
        gameCameras[camListPosition].SetActive(true);

        if (camListPosition > 0)
            camListPosition--;
    }
}

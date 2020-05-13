using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] GameObject cameraA, cameraB;
    [SerializeField] bool switchCamera = false;
    [SerializeField] List<string> playerUnitTags; 

    private void OnTriggerEnter(Collider other) {
        if (!switchCamera)
        {
            for (int tagIndex = 0; tagIndex < playerUnitTags.Count; tagIndex++)
            {
                if (other.gameObject.tag == playerUnitTags[tagIndex])
                {
                    cameraA.SetActive(false);
                    cameraB.SetActive(true);
                    switchCamera = true;
                    return;
                }
            }
        }
        else
        {
            for (int tagIndex = 0; tagIndex < playerUnitTags.Count; tagIndex++)
            {
                if (other.gameObject.tag == playerUnitTags[tagIndex])
                {
                    cameraA.SetActive(true);
                    cameraB.SetActive(false);
                    switchCamera = false;
                    return;
                }
            }
        }
    }
    
   
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageArea : MonoBehaviour
{
    [SerializeField] int cameraIndex = 0;
    [SerializeField] bool activeCamera = false;
    [SerializeField] string playerTroopTag;
    [SerializeField] CManager manager;
    [SerializeField] int minLayer;
    [SerializeField] int maxLayer;
    // private void Start() {
    //     Physics.IgnoreLayerCollision(minLayer, maxLayer);
    // }
    private void Update() {
        if (activeCamera)
        {
            manager.goToCameraAt(cameraIndex, activeCamera);
           
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag(playerTroopTag))
        {
            Debug.Log("Unit has entered the area!");
            activeCamera = true;
            
       }
    }
    // private void OnTriggerExit(Collider other) 
    // {
    //     if (other.gameObject.tag == playerTroopTag)
    //     {
    //         activeCamera = false;
    //         FindObjectOfType<CManager>().goToCameraAt(cameraIndex, activeCamera);
    //     }
    // }
}

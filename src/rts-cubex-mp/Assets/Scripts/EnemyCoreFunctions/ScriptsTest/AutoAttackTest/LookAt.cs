using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour{
[SerializeField] GameObject SpinGun;
private Vector3 Target;
private bool isPlayerNear=false;

    void Update() {
        if(isPlayerNear){
        Target = FindObjectOfType<PlayerLightTroop>().getUnitPosition();
         SpinGun.transform.LookAt(Target);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Unit"){
        isPlayerNear=true;
        }
    }
    private void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Unit"){
        isPlayerNear = false;
        }
 }
    public bool getIsPlayerNear(){
        return isPlayerNear;
    }
}
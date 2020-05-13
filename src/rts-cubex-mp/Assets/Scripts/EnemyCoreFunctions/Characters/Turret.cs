using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
  [SerializeField] string playerTag;
  [SerializeField] GameObject spinGun;
  private Vector3 targetObject;
  private bool isEnemyNear = false;

  private void Update () {
    if (isEnemyNear) {
      // Debug.Log(isEnemyNear);
      // troopBody.GetComponent<EnemyAILightUnit>().setDestination(transform.position);
      //troopBody.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
      //  targetObject = FindObjectOfType<PlayerLightTroop>().getUnitPosition();
      if (targetObject != null) {
        //  Debug.Log(gameObject.name + " says: I see an enemy! looking at it! state - " + isEnemyNear);
        spinGun.transform.LookAt (targetObject);
      } else {
        isEnemyNear = false;
      }
    }
  }
  private void OnTriggerEnter (Collider other) {
    if (other.gameObject.tag == playerTag) {
      isEnemyNear = true;
      targetObject = other.gameObject.transform.position;
      Debug.Log (gameObject.name + " says: I see an enemy! state - " + isEnemyNear);
    } else {
      isEnemyNear = false;
      //Debug.Log(gameObject.name + " says: I don't see an enemy! " + isEnemyNear);
    }
  }
  private void OnTriggerStay (Collider other) {
    if (other.gameObject.tag == playerTag) {
      isEnemyNear = true;
      targetObject = other.gameObject.transform.position;
      Debug.Log (gameObject.name + " says: I see an enemy! getting position and attacking! state - " + isEnemyNear);
      // Debug.Log(isEnemyNear);
    }

  }
  private void OnTriggerExit (Collider other) {
    if (other.gameObject.tag != playerTag) {
      isEnemyNear = false;
      Debug.Log (gameObject.name + " says: Enemy has dissapeared! state - " + isEnemyNear);
      // Debug.Log(isEnemyNear);
    }
  }
  public bool isPlayerTroopNear () {
    return isEnemyNear;
  }
  public GameObject getSpinGun()
  {
    if (spinGun != null)
      return spinGun;
    else
      return null; 
  }
}
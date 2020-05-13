using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackTest : MonoBehaviour {
  // [SerializeField] string playerTag;
  [SerializeField] string[] playerTags;
  [SerializeField] GameObject spinGun;
  [SerializeField] GameObject troopBody;
  private Collider isInside;
  private Transform targetObject;
  private bool isEnemyNear = false;

  private void Update () {
    if (isEnemyNear && isInside) {
      //  Debug.Log(gameObject.name + " says: I see an enemy! looking at it! state - " + isEnemyNear);
        spinGun.transform.LookAt (targetObject.position);
    } else {
      isEnemyNear = false;
    }
  }
  private void OnTriggerEnter (Collider other) {
    if (isPlayer(other.gameObject.transform.tag)) {
      isEnemyNear = true;
      targetObject = other.gameObject.transform;
      isInside = other;
      troopBody.GetComponent<UnityEngine.AI.NavMeshAgent> ().isStopped = true;
      Debug.Log (gameObject.name + " says: I see an enemy! state - " + isEnemyNear);
     } else {
      // troopBody.GetComponent<UnityEngine.AI.NavMeshAgent> ().isStopped = false;
       isEnemyNear = false;
       //Debug.Log(gameObject.name + " says: I don't see an enemy! " + isEnemyNear);
    }
  }
  private void OnTriggerStay (Collider other) {
    if (isPlayer(other.gameObject.transform.tag)) {
      isEnemyNear = true;
      targetObject = other.gameObject.transform;
      isInside = other;
      Debug.Log (gameObject.name + " says: I see an enemy! getting position and attacking! state - " + isEnemyNear);
      //troopBody.GetComponent<UnityEngine.AI.NavMeshAgent> ().isStopped = true;
      // Debug.Log(isEnemyNear);
    }

  }
  private void OnTriggerExit (Collider other) {
    if (isPlayer(other.gameObject.transform.tag)) {
      //troopBody.GetComponent<UnityEngine.AI.NavMeshAgent> ().isStopped = false;
      isEnemyNear = false;
      Debug.Log (gameObject.name + " says: Enemy has dissapeared! state - " + isEnemyNear);
      troopBody.GetComponent<UnityEngine.AI.NavMeshAgent> ().isStopped = false;
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
  //Method for checking is it an object that belongs to the player field...
  public bool isPlayer(string tag)
  {

      for (int tIndex = 0; tIndex < playerTags.Length; tIndex++)
      {
        if(tag == playerTags[tIndex])
        {
          return true;
        }
      }
      return false;
  }
}
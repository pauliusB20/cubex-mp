using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemy : MonoBehaviour {
    [SerializeField] GameObject Troop;
    [SerializeField] string [] enemyTags;
    private Vector3 Target;
    private bool isEnemyNear = false;
    private Collider isInside;

    void Update () {
        if (isEnemyNear && isInside) {
            Troop.transform.LookAt (Target);
        } else if (isEnemyNear && !isInside) {
            isEnemyNear = false;
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (isEnemy(other.gameObject.tag)) {
            isEnemyNear = true;
            Target = other.gameObject.transform.position;
            isInside = other;
        }
    }
    private void OnTriggerStay (Collider other) {
        if (isEnemy(other.gameObject.tag)) {
            isEnemyNear = true;
            Target = other.gameObject.transform.position;
        }
    }
    private void OnTriggerExit (Collider other) {
        if (isEnemy(other.gameObject.tag)) {
            isEnemyNear = false;
        }
    }
    public bool getIsEnemyNear () {
        return isEnemyNear;
    }
    private bool isEnemy(string tag){
        foreach(var i in enemyTags){
            if(tag==i){
                return true;
            }
        }
        return false;
    }
    /* this script is needed for the troops to detect and aim at their enemy, everything is done with colliders */
}
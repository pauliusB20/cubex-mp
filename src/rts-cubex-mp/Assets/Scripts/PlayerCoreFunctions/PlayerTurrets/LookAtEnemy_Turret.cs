using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemy_Turret : MonoBehaviour {
    [SerializeField] GameObject Troop;
    private Vector3 Target;
    private bool isEnemyNear = false;
    private Collider isInside;

    void Update () {
        if (isEnemyNear && isInside) {
            //Target = FindObjectOfType<TurretFire>();
            Troop.transform.LookAt (Target);
        } else if (isEnemyNear && !isInside) {
            isEnemyNear = false;
        }
    }

    private void OnTriggerEnter (Collider other) {
        if ((other.gameObject.tag == "enemyTroop") || (other.gameObject.tag == "EnemyBase") || (other.gameObject.tag == "EnemyAIWorker")) {
            isEnemyNear = true;
            Target = other.gameObject.transform.position;
            isInside = other;
        }
    }
    private void OnTriggerStay (Collider other) {
        if ((other.gameObject.tag == "enemyTroop") || (other.gameObject.tag == "EnemyBase") || (other.gameObject.tag == "EnemyAIWorker")) {
            isEnemyNear = true;
            Target = other.gameObject.transform.position;
        }
    }
    private void OnTriggerExit (Collider other) {
        if ((other.gameObject.tag == "enemyTroop") || (other.gameObject.tag == "EnemyBase") || (other.gameObject.tag == "EnemyAIWorker")) {
            isEnemyNear = false;
        }
    }
    public bool getIsEnemyNear () {
        return isEnemyNear;
    }
}
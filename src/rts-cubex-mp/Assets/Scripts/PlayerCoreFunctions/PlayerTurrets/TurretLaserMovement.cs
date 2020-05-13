using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaserMovement : MonoBehaviour {
    Rigidbody laserMove;
    TurretFire damage;
    [SerializeField] float thrust = 2f;
    // Start is called before the first frame update
    void Start () {
        damage = GetComponent<TurretFire> ();
        laserMove = GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    void Update () {
        laserMove.velocity = transform.TransformDirection (new Vector3 (0f, 0f, thrust));
    }
    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "enemyTroop") {
            other.gameObject.GetComponent<HealthEnemyAI> ().decreaseHealth (damage.getDamage ());
            Destroy (gameObject);
        }

    }
}
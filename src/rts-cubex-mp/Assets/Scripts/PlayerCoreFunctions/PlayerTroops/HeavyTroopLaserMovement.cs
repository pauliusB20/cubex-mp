using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTroopLaserMovement : MonoBehaviour {
    Rigidbody laserMove;
    private Research damage;
    [SerializeField] float thrust = 2f;
    [SerializeField] int defaultDamage=10;
    [SerializeField] ResearchConf oBGResearch;
    [SerializeField] string [] enemyTags;
    void Start () {
        laserMove = GetComponent<Rigidbody> ();
        //if (FindObjectOfType<Research> () != null) {
        //damage = FindObjectOfType<Research> ();}
    }
    void Update () {
        laserMove.velocity = transform.TransformDirection (new Vector3 (0f, 0f, thrust));
    }
    private void OnTriggerEnter (Collider other) {
        /*if (isEnemy(other.gameObject.tag)){
            other.gameObject.GetComponent<HealthEnemyAI>().decreaseHealth (oBGResearch.getDamage());
            Destroy (gameObject);
        }
        else {
            Destroy (gameObject);
        }*/
        switch(other.gameObject.tag) {
        case "enemyTroop":
            other.gameObject.GetComponent<HealthEnemyAI>().decreaseHealth(oBGResearch.getHeavyDamage());
            Destroy(gameObject);
            break;
            
            case "EnemyBase":
            other.gameObject.GetComponent<HealthEnemyAI>().decreaseHealth(oBGResearch.getHeavyDamage());
            Destroy(gameObject);
            break;

            case "EBCollector":
            other.gameObject.GetComponent<HealthEnemyAI>().decreaseHealth(oBGResearch.getHeavyDamage());
            Destroy(gameObject);
            break;

            case "EMDef":
            other.gameObject.GetComponent<HealthEnemyAI>().decreaseHealth(oBGResearch.getHeavyDamage());
            Destroy(gameObject);
            break;

            case "EmDefBody":
            other.gameObject.GetComponent<HealthEnemyAI>().decreaseHealth(oBGResearch.getDamage());
            Destroy(gameObject);
            break;

            case "EBWorker":
            other.gameObject.GetComponent<HealthEnemyAI>().decreaseHealth(oBGResearch.getHeavyDamage());
            Destroy(gameObject);
            break;

            case "EBBarracks":
            other.gameObject.GetComponent<HealthEnemyAI>().decreaseHealth(oBGResearch.getHeavyDamage());
            Destroy(gameObject);
            break;

            case "LvlMap":
                Destroy(gameObject);
            break;

            case "Arrow":
                Destroy(gameObject);
            break;
        }
    }
    private bool isEnemy(string tag){
        foreach(var i in enemyTags){
            if(tag==i){
                return true;
            }
        }
        return false;
    }
    /* if the object (bullet) hits an enemy, it decreases their health the bullet is destroyed */
}
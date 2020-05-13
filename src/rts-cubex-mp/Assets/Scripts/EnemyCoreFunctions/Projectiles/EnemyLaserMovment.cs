using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserMovment : MonoBehaviour
{
    [SerializeField] float speed = 5f; 
    [SerializeField] int damagePoints = 25; 
    // [SerializeField] string playerUnitTag = "Unit";
    // [SerializeField] string playerBaseTag = "PlayerBase";
    Rigidbody laserMov;
    void Start()
    {
        laserMov = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        laserMov.velocity = transform.TransformDirection(new Vector3(0f, 0f, speed));
    }
    private void OnTriggerEnter(Collider other) {
        switch(other.gameObject.tag)
        {
            case "Unit":
            if(other.gameObject.GetComponent<TroopHealth>()!=null) {
            other.gameObject.GetComponent<TroopHealth>().decreaseHealth(damagePoints);
            }
            else
            {
                other.gameObject.GetComponent<HeavyHealth>().decreaseHealth(damagePoints);
            }
            Destroy(gameObject);
            break;
            
            case "PlayerBase":
            other.gameObject.GetComponent<HealthOfRegBuilding>().decreaseHealth(damagePoints);
            Destroy(gameObject);
            break;

            case "energonFactory":
            other.gameObject.GetComponent<HealthOfRegBuilding>().decreaseHealth(damagePoints);
            Destroy(gameObject);
            break;

            case "Worker":
            other.gameObject.GetComponent<HealthOfRegBuilding>().decreaseHealth(damagePoints);
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
    public void setMaxDamagePoints(int mDmgPoints)
    {
        this.damagePoints = mDmgPoints;
    }
    public int getMaxDamagePoints()
    {
        return damagePoints;
    }
}

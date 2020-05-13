using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour
{
   [SerializeField] private Transform gunPoint;
   [SerializeField] private GameObject projectile;
   [SerializeField] float fireRate = 0.5f;
   [SerializeField] int maxDamagePoints = 25; 
   private AutoAttackTest inRange;

    private float nextFire = 0.0f;
    GameObject parent;
    private void Start() {
        //FIX this
        parent = gameObject.transform.parent.gameObject;
        inRange= parent.GetComponent<AutoAttackTest>();
    }
    public void Update()
    {
       if(inRange.isPlayerTroopNear())
       {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
       }
       else 
        return;
    }

    private void Shoot()
    {
      if (projectile != null)
      {
        projectile.GetComponent<EnemyLaserMovment>().setMaxDamagePoints(maxDamagePoints);
        GameObject projectileInstance = Instantiate(projectile, gunPoint.position, gunPoint.rotation);
      }
      else
      {
          Debug.LogError("No projectile prefab asigned!");
          return; 
      }
    }
    public Vector3 getUnitPosition(){
        return transform.position;
    }

    public void setMaxDmgPoints(int mDPoints)
    {
        this.maxDamagePoints = mDPoints;
    }
    public int getDmgPoints()
    {
        return this.maxDamagePoints;
    }
}

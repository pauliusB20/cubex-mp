using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFireMotion : MonoBehaviour
{
    [SerializeField] private Transform gunPoint;
    [SerializeField]  private GameObject projectile;
    private Turret inRange;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] int maxDamagePoints = 25; 
    private float nextFire = 0.0f;

    GameObject parent;
    private void Start() {
        //FIX this
        parent = gameObject.transform.parent.gameObject;
        // Debug.Log(parent.name);
        inRange=parent.GetComponent<Turret>();
    }
    public void Update()
    {
       if(inRange.isPlayerTroopNear()){
        if(Time.time > nextFire){
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
    public int getMaxDmgPoints()
    {
       return maxDamagePoints;
    }
}

using System;
using UnityEngine;

public class Fire2 : MonoBehaviour
{
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Rigidbody projectilePrefab;
    [SerializeField]
    private float launchForce = 700f;
    private LookAt inRange;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    private void Start() {
        inRange=FindObjectOfType<LookAt>();
    }
    public void Update()
    {
       if(inRange.getIsPlayerNear()){
        if(Time.time > nextFire){
            nextFire = Time.time + fireRate;
            Shoot();
        }
       }
    }

    private void Shoot()
    {
      var projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
    public Vector3 getUnitPosition(){
        return transform.position;
    }
    
}
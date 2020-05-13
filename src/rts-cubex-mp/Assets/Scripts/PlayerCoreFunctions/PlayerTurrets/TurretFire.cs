using System;
using UnityEngine;

public class TurretFire : MonoBehaviour {
    [SerializeField] private Transform firePoint;
    [SerializeField] private Rigidbody projectilePrefab;
    [SerializeField] private float launchForce = 700f;
    [SerializeField] private int turretDamage = 10;
    private LookAtEnemy_Turret inRange;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;

    private void Start () {
        inRange = FindObjectOfType<LookAtEnemy_Turret> ();
    }
    public void Update () {
        if (inRange.getIsEnemyNear ()) {
            if (Time.time > nextFire) {
                nextFire = Time.time + fireRate;
                Shoot ();
            }
        }
    }

    private void Shoot () {
        var projectileInstance = Instantiate (projectilePrefab, firePoint.position, firePoint.rotation);
        projectileInstance.AddForce (firePoint.forward * launchForce);
    }
    public Vector3 getUnitPosition () {
        return transform.position;
    }
    public int getDamage () {
        return turretDamage;
    }
    public void setDamagePoints(int newDamagePoints)
    {
        turretDamage = newDamagePoints;
    }
}
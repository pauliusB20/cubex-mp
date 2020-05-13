using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAreaEnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyAIBase eb;
    private GameObject enemyBase;
    private Vector3 sizeInUnits;
    private int depoAmount = 0;
    

    private void Start() {
        sizeInUnits = Vector3.Scale(transform.localScale, GetComponent<MeshFilter>().mesh.bounds.size);
        Debug.Log("Current size of the build area on the zAxis is " + sizeInUnits.z);
    }
    

    public void addDepoAmount(int amount)
    {
       eb.addDepositCount(amount);
    }
    public Vector3 getBuildAreaSizeOnAxis()
    {
        return sizeInUnits;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class workerTest : MonoBehaviour
{
    //Moving AI Worker from a deposit to base
    Vector3 depositPosition, basePosition;
    Vector3 destinationPosition;

    LevelManager lm;
    UnityEngine.AI.NavMeshAgent nv;
    void Start()
    {
        nv = GetComponent<UnityEngine.AI.NavMeshAgent>();
        lm = FindObjectOfType<LevelManager>();
        if (lm != null)
        {
            depositPosition = lm.getDepositPosition();
            basePosition = lm.getBasePosition();

            destinationPosition = depositPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        nv.destination = destinationPosition; 
    }

    public void setDestination(Vector3 pos)
    {
        destinationPosition = pos;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrollV2 : MonoBehaviour
{
        // [Header("Troop unit configuartion parameters")]
        // [SerializeField] float stoppingDistance = 0.8f;
        // [SerializeField] bool patrolMode = false;

        // private int destPoint = 0;
        // private UnityEngine.AI.NavMeshAgent tAgent;
        // private AITroopManager mg; 
        // int desIndex = 0;

        // void Start () {
        //     tAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //     // Disabling auto-braking allows for continuous movement
        //     // between points (ie, the agent doesn't slow down as it
        //     // approaches a destination point).
        //     tAgent.autoBraking = false;

        //     GotoNextPoint();
        // }


        // void GotoNextPoint() {
        //     // Returns if no points have been set up
        //     if (points.Length == 0)
        //         return;

        //     // Set the agent to go to the currently selected destination.
        //     tAgent.destination = points[destPoint].position;

        //     // Choose the next point in the array as the destination,
        //     // cycling to the start if necessary.
        //     destPoint = (destPoint + 1) % points.Length;
        // }


        // void Update () {
        //     // Choose the next destination point when the agent gets
        //     // close to the current one.
        //     if (!tAgent.pathPending && tAgent.remainingDistance < 0.5f)
        //         GotoNextPoint();
        // }
}

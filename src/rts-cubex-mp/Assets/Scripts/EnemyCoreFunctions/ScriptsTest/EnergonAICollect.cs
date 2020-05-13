using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergonAICollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.gameObject.tag == "Worker")
        {
            var basePosition = FindObjectOfType<LevelManager>();
            otherObject.gameObject.GetComponent<workerTest>().setDestination(basePosition.getBasePosition());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanOffMapObjects : MonoBehaviour
{
   private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "enemyTroop" || other.gameObject.tag == "Unit")
        {
            Destroy(gameObject);
        }    
   }
}

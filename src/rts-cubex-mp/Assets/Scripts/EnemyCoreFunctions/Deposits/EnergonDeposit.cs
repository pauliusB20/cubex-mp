using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergonDeposit : MonoBehaviour
{
   private bool isTouchingBuildArea = false;
   private int depositCode = 0;

   private void Update() {
       if (isTouchingBuildArea)
       {
           isTouchingBuildArea = true;
       }
   }
   //If the energon deposit collides once, then adds a 1 to the deposit count variable that is the enemy base class
   private void OnTriggerEnter(Collider other) {
       if (other.gameObject.tag == "BuildAreaAI" && !isTouchingBuildArea)
       {
           isTouchingBuildArea = true;
           other.gameObject.GetComponent<BuildAreaEnemyAI>().addDepoAmount(1);
           Debug.Log("Touch build area state: " + isTouchingBuildArea);
       }
   }
   
   public bool touchingBuildArea()
   {
       return isTouchingBuildArea;
   }
  
}

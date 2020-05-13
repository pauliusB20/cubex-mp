using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseTag : MonoBehaviour
{
   [SerializeField] string enemyMBaseTag = "A1";

   public void setEnemyMBaseTag(string tag)
   {
       enemyMBaseTag = tag;
   }
   public string getEnemyMBaseTag()
   {
       return enemyMBaseTag;
   }

   
}

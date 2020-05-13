using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   [SerializeField] GameObject enemyBase, deposit;
    
   public Vector3 getBasePosition()
   {
       return enemyBase.transform.position;
   }

   public Vector3 getDepositPosition()
   {
       return deposit.transform.position;
   }
}

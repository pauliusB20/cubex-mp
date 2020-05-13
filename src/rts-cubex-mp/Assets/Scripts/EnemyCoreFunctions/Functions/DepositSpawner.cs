using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositSpawner : MonoBehaviour
{
  [SerializeField] List <Transform> depositPoints;
  [SerializeField] GameObject energon;
    
  private void Awake()
  {      
      SpawnDesposits();
  }
  private void SpawnDesposits()
  {
      for (int index = 0; index < depositPoints.Count; index++)
      {
          Instantiate(energon, depositPoints[index].position, Quaternion.identity);
      }
  }
}

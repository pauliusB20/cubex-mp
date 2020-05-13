using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreRank
{
   [Header("Current rank configuration")]
   [SerializeField] private int minReqScore;
   [SerializeField] private int maxReqScore;
   [SerializeField] private string rankName;

   public string getCurrentRank(int score)
   {
       return minReqScore < score && score <= maxReqScore ? rankName : null;
   }

}

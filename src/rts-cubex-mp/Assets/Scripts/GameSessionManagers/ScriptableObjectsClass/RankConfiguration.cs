using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RankConfiguration", menuName = "Cubex/RankConfiguration", order = 0)]

public class RankConfiguration : ScriptableObject {
    
    [SerializeField] List<ScoreRank> gameRanks;

    public string getRank(int score)
    {
        string rank = "";

        for (int rIndex = 0; rIndex < gameRanks.Count; rIndex++)
        {
            rank = gameRanks[rIndex].getCurrentRank(score);
            if (rank != null)
            {
                return rank;
            }
        }
        //Return a default rank, which is jus simply pro
        return "Pro";
        
    }
}

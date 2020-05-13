using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManagerTroop : MonoBehaviour
{
    [SerializeField] int killPointsForPlayer = 5;
    [SerializeField] bool isTroop = false;
   private HealthEnemyAI health;
   private GameSession gs;
   private void Start() {
       gs = FindObjectOfType<GameSession>();
       health = GetComponent<HealthEnemyAI>();
   }

   private void Update() {
       if (health.getHealth() <= 0)
       {
           gs.AddPlayerScorePoints(killPointsForPlayer);
           if (isTroop)
            gs.increaseEPlayerUnitKillAmount();
           Destroy(gameObject);
       }
   }
}

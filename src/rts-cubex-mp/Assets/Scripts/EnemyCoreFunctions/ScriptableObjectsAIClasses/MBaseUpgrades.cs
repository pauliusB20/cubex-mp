using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MBaseUpgradeLevels", menuName = "EnemyAI_Test/MBaseUpgrades", order = 0)]
public class MBaseUpgrades : ScriptableObject {

   [SerializeField] List <ThLvl> baseTechLvl;

   public List <ThLvl> getTechLevels()
   {
       return baseTechLvl;
   }


}

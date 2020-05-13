using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTagMgr : MonoBehaviour
{

   public Vector3 getCurrentBasePositionAtTag(string tag, string enemyTag) 
   {
       var enemyBases = GameObject.FindGameObjectsWithTag(enemyTag);      
     //  Debug.Log("EBase size: " + enemyBases.Length);     
       foreach (var o in enemyBases)
       {
           if (o.GetComponent<EnemyBaseTag>().getEnemyMBaseTag() == tag)
           {
               return o.GetComponent<AIResourcesManagement>().getResourceCollectionPoint();
           }
       }
        //  var enemySpawner = FindObjectOfType<EnemyBaseSpawner>();
        // var baseList = enemySpawner.getEnemyBaseList();

        // foreach (var o in baseList)
        // {
        //     if (o.GetComponent<EnemyBaseTag>().getEnemyMBaseTag() == tag)
        //     {
        //         return o.GetComponent<AIResourcesManagement>().getResourceCollectionPoint();
        //     }
        // }
       return Vector3.zero;
   }
   public GameObject getCurrentEnemyBaseByTag(string tag, string enemyTag)
   {
       var enemyBases = GameObject.FindGameObjectsWithTag(enemyTag);      
      // Debug.Log("EBase size: " + enemyBases.Length);  
       foreach (var o in enemyBases)
       {
           if (o.GetComponent<EnemyBaseTag>().getEnemyMBaseTag() == tag)
           {
               return o;
           }
       }
       return null;
        // var enemySpawner = FindObjectOfType<EnemyBaseSpawner>();
        // var baseList = enemySpawner.getEnemyBaseList();

        // foreach (var o in baseList)
        // {
        //     if (o.GetComponent<EnemyBaseTag>().getEnemyMBaseTag() == tag)
        //     {
        //         return o;
        //     }
        // }
        // return null;
   }
   public void setEnemyUnitOrStructureTag(GameObject unit, string tag)
   {
       unit.GetComponent<EnemyTag>().setEnemyTag(tag);
   }
   public void setEnemyUnitsOrStructuresTag(GameObject[] obj, string tag)
   {
       foreach (var o in obj)
       {
           o.GetComponent<EnemyTag>().setEnemyTag(tag);
       }
   }

    public bool enemyObjectExists(string enemyTag, string gameObjectTag)
    {
         return  GameObject.FindGameObjectWithTag(gameObjectTag) != null;
    }
    public bool enemyObjectsExistWithTag(string enemyTag, string gameObjectTag)
    {
        int size = 0;
        var eObjects = GameObject.FindGameObjectsWithTag(gameObjectTag);
        foreach (var obj in eObjects)
        {
            if (obj.GetComponent<EnemyTag>().getEnemyTag() == enemyTag)
                size++;
        }
        
        return size > 0;
    }
    public GameObject locateEnemyObjectWithTag(string enemyTag, string gameObjectTag)
    {
        var eObjects = GameObject.FindGameObjectsWithTag(gameObjectTag);
        foreach (var obj in eObjects)
        {
            if (obj.GetComponent<EnemyTag>().getEnemyTag() == enemyTag)
                return obj;
        }
        return null;
    }
    
    
    public GameObject[] locateEnemyObjectsWithTag(string enemyTag, string gameObjectTag)
    {
        var foundObjects = GameObject.FindGameObjectsWithTag(gameObjectTag);
        
        int size = 0;
        if (foundObjects.Length > 0)
        {
            foreach (var o in foundObjects)
            {
                size += o.GetComponent<EnemyTag>().getEnemyTag() == enemyTag &&  
                    o.GetComponent<EnemyTag>().getEnemyObjectName() == gameObjectTag ? 1 : 0;
            }

            GameObject[] temp = new GameObject[size];
            int tIndex = 0;
            for (int fIndex = 0; fIndex < foundObjects.Length; fIndex++)
            {
                if ((foundObjects[fIndex].GetComponent<EnemyTag>().getEnemyTag() == enemyTag && 
                    foundObjects[fIndex].GetComponent<EnemyTag>().getEnemyObjectName() == gameObjectTag) && tIndex <= size)
                {
                    temp[tIndex] = foundObjects[fIndex];
                    tIndex++;
                }
            }

            return temp;
        }
        return null;
    }
    public bool enemyObjectsExist(string gameObjectTag)
    {
        return GameObject.FindGameObjectsWithTag(gameObjectTag).Length > 0;
    }
      public bool enemyObjectsRequiredAmountExist(string gameObjectTag, int size)
    {
        return GameObject.FindGameObjectsWithTag(gameObjectTag).Length > size;
    }
     public bool enemyObjectsWithTagExist(string enemyTag, string gameObjectTag)
    {
        var objects = GameObject.FindGameObjectsWithTag(gameObjectTag);
        int size = 0;
        if (objects.Length > 0) 
        {
             foreach (var o in objects)
            {
                size += o.GetComponent<EnemyTag>().getEnemyTag() == enemyTag ? 1 : 0;
            }

            GameObject[] temp = new GameObject[size];

            for (int fIndex = 0; fIndex < temp.Length; fIndex++)
            {
                if (objects[fIndex].GetComponent<EnemyTag>().getEnemyTag() == enemyTag)
                    temp[fIndex] = objects[fIndex];
            }

            return temp.Length > 0;
        }

        return false;
    }
}

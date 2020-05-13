using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointSet
{
    [SerializeField] private float distanceToObject;
    [SerializeField] private Vector3 collectedPoint;
    [SerializeField] GameObject foundObject;

    public PointSet(float distance, Vector3 point, GameObject obj) { distanceToObject = distance; collectedPoint = point; foundObject = obj; }

    public float getDistanceToObject() { return distanceToObject; }
    public Vector3 getCollectedPoint() { return collectedPoint;   }
    public GameObject getFoundObject() { return foundObject;      }
}
public class EAIWallRotator : MonoBehaviour
{
    [SerializeField] GameObject wallBody;
    [SerializeField] float scannerRotationSpeed = 1f;
    [SerializeField] List<PointSet> collectedPoints;
    [SerializeField] int maxPointAmount = 5;
    [SerializeField] string avoidObjectsLayer;
    [SerializeField] bool isStopped = false;
    [SerializeField] bool isMaxDistanceToObject = false;
   
    void Update()
    {
        //Main logic making self adapting wall...
        
        if (maxPointAmount <= collectedPoints.Count) 
         isStopped = true;

        if (maxPointAmount >= collectedPoints.Count && !isStopped)
         scannerRotate();
        
        if (isStopped && !isMaxDistanceToObject)
        {
           var maxPoint = checkForFurhestPoint();
           
           if (maxPoint != null)
            wallBody.transform.LookAt(maxPoint);
        }
    }

    //Method for finding the maximum object...
    private Transform checkForFurhestPoint()
    {
        //Sort the list at first..
        SortPoints objectPoints = new SortPoints();
        objectPoints.heapSort(collectedPoints);
        //After that find the maxDistance
        isMaxDistanceToObject = true;
        return collectedPoints[collectedPoints.Count - 1].getFoundObject().transform;
        
    }

    //Method for collecting points.. 
    private void scannerRotate()
    {
        transform.Rotate(new Vector3(0f, 45f, 0f) * Time.deltaTime * scannerRotationSpeed);
        
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer(avoidObjectsLayer)))
        {      
           if ((hit.transform.gameObject.layer == LayerMask.NameToLayer(avoidObjectsLayer)) )
           {   
               Debug.DrawRay(transform.position, transform.forward, Color.green); 
               collectedPoints.Add(new PointSet(hit.distance, hit.point, hit.transform.gameObject));
           }
        }
       
    }
   
    
}


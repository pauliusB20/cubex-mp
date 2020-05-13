using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchCenterAnim : MonoBehaviour
{
    [Range(0.1f, 5f)]
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float angleY = 45f;
    [SerializeField] string SpinnerAnimTag;
    private GameObject spinner;

    private void Start() {
        spinner = FindGameObjectInChildWithTag(gameObject, SpinnerAnimTag);
    }
    void Update()
    {
        if (spinner != null)
            spinner.transform.Rotate(new Vector3(0f, angleY, 0f) * rotationSpeed * Time.deltaTime);
        else
            return;
    }

     public GameObject FindGameObjectInChildWithTag (GameObject parent, string tag)
     {
         Transform t = parent.transform;
 
         for (int i = 0; i < t.childCount; i++) 
         {
             if(t.GetChild(i).gameObject.tag == tag)
             {
                 return t.GetChild(i).gameObject;
             }
                 
         }
             
         return null;
     }
}

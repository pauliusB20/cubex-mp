using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathOperations : MonoBehaviour
{
    [SerializeField] TestScObj test;
    [SerializeField] TestScObj test_orginal;
    void Start()
    {
        Debug.Log(test.getA());
        Debug.Log(test.getB());
        Debug.Log(test.getA() + test.getB());
    }

   void OnDestroy()
   {
        // test.setA(test_orginal.getA());
   }
}

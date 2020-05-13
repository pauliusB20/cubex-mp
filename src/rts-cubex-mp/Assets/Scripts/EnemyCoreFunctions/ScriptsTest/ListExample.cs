using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListExample : MonoBehaviour
{
    [SerializeField] List <int> numberList;
    void Start()
    {
        UnityEngine.Debug.Log("Given values:");
        for (int i = 0; i < numberList.Count; i++)
        {
            UnityEngine.Debug.Log(numberList[i]);
        }
        
        numberList.RemoveAt(0);

         UnityEngine.Debug.Log("New values:");
         for (int i = 0; i < numberList.Count; i++)
        {
            UnityEngine.Debug.Log(numberList[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

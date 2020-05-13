using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialCode : MonoBehaviour
{
    [SerializeField] float timeDelay = 2f;
    void Start()
    {
        StartCoroutine(printMessageAfterDelay());
    }

    IEnumerator printMessageAfterDelay()
    {
        yield return new WaitForSeconds(timeDelay);
        UnityEngine.Debug.Log("Hello world");
    }
    
}

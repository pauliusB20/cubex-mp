using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathFunctions : MonoBehaviour
{
    [SerializeField] float number = 2f;
    [SerializeField] float nNumber = 0;
    [SerializeField] bool hasChanged = false;

    void Start()
    {
        nNumber = Mathf.Pow(number, 2f);
        if (number < nNumber)
            hasChanged = true;
    }

    public bool Changed()
    {
        return hasChanged;
    }
    public float getNewNumber()
    {
        return nNumber;
    }
    public float getNumber()
    {
        return number;
    }
}

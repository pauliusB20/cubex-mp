using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TroopResearch", menuName = "Tests/Test1", order = 1)]
public class TestScObj : ScriptableObject
{
    [SerializeField]
    int a = 0;
    [SerializeField]
    int b = 0;


    public int getA()
    {
        return a;
    }

    public int getB()
    {
        return b;
    }
}

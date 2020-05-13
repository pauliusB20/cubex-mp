using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopCode : MonoBehaviour
{
    [SerializeField] string troopCode = "lm";

    public string getCode()
    {
        return troopCode;
    }
    public void setCode(string c)
    {
        this.troopCode = c;
    }
}

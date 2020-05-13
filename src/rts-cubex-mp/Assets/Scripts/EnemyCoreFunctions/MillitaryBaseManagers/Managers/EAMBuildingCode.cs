using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAMBuildingCode : MonoBehaviour
{
    [SerializeField] string buildingCode = "Empty";

    public string getBuildingCode()
    {
        return buildingCode;
    }

}

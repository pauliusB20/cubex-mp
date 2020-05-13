using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCamera : MonoBehaviour
{
    [SerializeField] string cameraCode = "C1";

    public string getCameraCode()
    {
        return cameraCode;
    }

    public void enableCamera(bool state)
    {
        gameObject.SetActive(state);
    }
}

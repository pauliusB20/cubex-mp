using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCameras : MonoBehaviour
{
    [SerializeField] GameObject cameraA;
    [SerializeField] GameObject cameraB;
    [SerializeField] string switchTag = "Unit";

    // Update is called once per frame
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == switchTag)
        {
            cameraA.SetActive(false);
            cameraB.SetActive(true);
        }
    }
}

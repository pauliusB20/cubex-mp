using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanArea : MonoBehaviour
{
    [SerializeField] float scannSpeed = 1f;
    [SerializeField] float rangeDistance = 10f;
    [SerializeField] float rotationAngle = 45f;
    [SerializeField] float rotationDelay = 5f;
    
    //Cached references
    AIwanderer aiWand;
    private Vector3 depoPosition;

    private bool isDepoFound;

    private void Start() {
        aiWand = gameObject.GetComponentInParent<AIwanderer>() as AIwanderer;
        isDepoFound = false;
    }

    void Update()
    {
        //Main loop for searching for the energon deposit
        if (aiWand.lookingForDeposit())
        {
            rotateScanner();

            if (!isDepoFound)
            {
                emitSearchRay();
            }
            else
            {
            return;
            }
        }
        else
        {
            return;
        } 
    }
    private void emitSearchRay()
    {
        RaycastHit searchRayHit;

        if (Physics.Raycast(transform.position, transform.forward, out searchRayHit, rangeDistance))
        {
           
            if (searchRayHit.transform.gameObject.layer == LayerMask.NameToLayer("Energon"))
            {
                Debug.Log("Found the deposit at coordinates: " + (searchRayHit.point));
                Debug.DrawRay(transform.position,  transform.TransformDirection(Vector3.forward) * rangeDistance, Color.green);
                depoPosition = searchRayHit.point;
                isDepoFound = true;
            }
           
        }
    }
    //Function to rotate the scanner object....
    private void rotateScanner()
    {
        transform.Rotate(new Vector3(0f, rotationAngle, 0f) * scannSpeed * Time.deltaTime);

    //     Quaternion rotateFrom = Quaternion.Euler(new Vector3(0.0f, rotationAngle, 0.0f));
    //     Quaternion rotateTo = Quaternion.Euler(new Vector3(0.0f, -rotationAngle, 0.0f));
 
    //    float transitionSpeed = 0.5f * (1.0f + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * scannSpeed));

    //     transform.localRotation = Quaternion.Lerp(rotateFrom, rotateTo, transitionSpeed);
    }
    public Vector3 getDepoPosition()
    {
        return depoPosition;
    }
    public bool DepoFound()
    {
        return isDepoFound;
    }   
    public void orderToSearchNewDepo(bool state)
    {
        isDepoFound = state;
    }
}

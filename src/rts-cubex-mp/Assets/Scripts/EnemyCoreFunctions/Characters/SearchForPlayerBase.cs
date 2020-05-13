using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForPlayerBase : MonoBehaviour
{
    [SerializeField] float scannSpeed = 1f;
    [SerializeField] float rangeDistance = 10f;
    [SerializeField] float rotationAngle = 45f;
    [SerializeField] float rotationDelay = 5f;
    
    //Cached references
    AITBaseSearch aiSearcher;
    FollowLeaderAction fAction;
    private AITroopManager tmanager;
    private Vector3 pltPosition;

    private bool isPlayerAreaFound;

    private void Start() {
        aiSearcher = gameObject.GetComponentInParent<AITBaseSearch>() as AITBaseSearch;
        fAction = gameObject.GetComponentInParent<FollowLeaderAction>() as FollowLeaderAction;
        tmanager = FindObjectOfType<AITroopManager>();
        isPlayerAreaFound = false;
        
    }

    void Update()
    {
        //Main loop for searching for the energon deposit
        if (aiSearcher.searchingForPlayerBase() || fAction.SearchingForBase())
        {
            //Debug.Log("Is searching for player base: " + aiSearcher.searchingForPlayerBase());
            rotateScanner();

            if (!isPlayerAreaFound)
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
           
            if (searchRayHit.transform.gameObject.layer == LayerMask.NameToLayer("PlayerBase"))
            {
                Debug.Log("Player base name: " + searchRayHit.transform.gameObject.name);
                Debug.Log("Spotted the player's territory at coordinates: " + (searchRayHit.point));
                Debug.DrawRay(transform.position,  transform.TransformDirection(Vector3.forward) * rangeDistance, Color.green);
                pltPosition = searchRayHit.point;
                tmanager.addTravelPointToBase(pltPosition);
                isPlayerAreaFound = true;
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
    public Vector3 getDiscoveryPosition()
    {
        return pltPosition;
    }
    public bool PlayerAreaFound()
    {
        return isPlayerAreaFound;
    }   
    public void orderToFortheNewPlayerBase(bool state)
    {
        isPlayerAreaFound = state;
    }
}

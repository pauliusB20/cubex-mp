using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {
    [SerializeField] private LayerMask clickablesLayer;

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            RaycastHit rayHit;
            if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out rayHit, Mathf.Infinity, clickablesLayer)) {
                ClickOn clickOnScript = rayHit.collider.GetComponent<ClickOn> ();
                clickOnScript.SetSelected (!clickOnScript.GetSelected ()); //---------------------------------------------------
                clickOnScript.ClickMe ();
                /* if the unit is clicked by player, its select state is changed. Color changing method in the script "ClickOn" is invoked */
            }
        }
    }

}
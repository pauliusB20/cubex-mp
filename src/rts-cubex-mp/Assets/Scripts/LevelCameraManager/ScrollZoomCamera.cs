﻿using UnityEngine;
class ScrollZoomCamera : MonoBehaviour
{
#if UNITY_IOS || UNITY_ANDROID
    [Header ("Camera limitation parameters")]
    [SerializeField] Camera Camera;
    [SerializeField] float x_max=80f;
    [SerializeField] float x_min=-100f;
    [SerializeField] float y_max=150f;
    [SerializeField] float y_min=15f;
    [SerializeField] float z_max=50f;
    [SerializeField] float z_min=-120f;
    public bool Rotate;
    protected Plane Plane;

    private void Awake()
    {
        if (Camera == null)
            Camera = Camera.main;
    }

    private void Update()
    {
        Debug.Log("Zoom"+Camera.transform.position);
        Camera.transform.position = new Vector3 (Mathf.Clamp(Camera.transform.position.x,x_min,x_max), Camera.transform.position.y,Mathf.Clamp(Camera.transform.position.z,z_min,z_max));
        //Update Plane
        if (Input.touchCount >= 1)
            Plane.SetNormalAndPosition(transform.up, transform.position);

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //Scroll
        if (Input.touchCount >= 1)
        {
            Delta1 = PlanePositionDelta(Input.GetTouch(0));
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                Camera.transform.Translate(Delta1, Space.World);
        }

        //Pinch
        if (Input.touchCount >= 2)
        {
            var pos1  = PlanePosition(Input.GetTouch(0).position);
            var pos2  = PlanePosition(Input.GetTouch(1).position);
            var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);
            
            //calc zoom
            // Debug.Log("Zoom distance for the camera Non delta:" + Vector3.Distance(pos1, pos2));
            // Debug.Log("Zoom distance for the camera Non delta:" + Vector3.Distance(pos1b, pos2b));
            var zoom = Vector3.Distance(pos1, pos2) /
                       Vector3.Distance(pos1b, pos2b);
            //edge case
            if (zoom == 0 || zoom > 10)
                return;
           
            //Move cam amount the mid ray
            Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);
            Camera.transform.position = new Vector3 (Camera.transform.position.x, Mathf.Clamp(Camera.transform.position.y,y_min,y_max),Camera.transform.position.z);
            

            if (Rotate && pos2b != pos2)
                Camera.transform.RotateAround(pos1, Plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal));
        }

    }

    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        //delta
        var rayBefore = Camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = Camera.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        //not on plane
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        var rayNow = Camera.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }
#endif
}
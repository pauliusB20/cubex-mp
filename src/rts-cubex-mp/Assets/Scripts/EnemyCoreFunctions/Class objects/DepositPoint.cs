using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositPoint
{
    // Start is called before the first frame update
    
    private float distanceToThisObject;
    private Vector3 depoPosition;
    private string objName;
    private string status; 
    public DepositPoint(float dis, Vector3 pos, string objName, string status)
    {
        this.distanceToThisObject = dis;
        this.depoPosition = pos;
        this.objName = objName;
        this.status = status;
    }
 
    public Vector3 getPosition()
    {
        return depoPosition;
    }
    public float getDistance()
    {
        return distanceToThisObject;
    }
    public void setPointStatus(string status)
    {
        this.status = status;
    }
    public string getPointStatus()
    {
        return status;
    }
    public string toString()
    {
        return "Object distance: " + distanceToThisObject + ", Coordinates: " + depoPosition.ToString() + ", name: " + objName + ", status: " + status;
    }

    
}

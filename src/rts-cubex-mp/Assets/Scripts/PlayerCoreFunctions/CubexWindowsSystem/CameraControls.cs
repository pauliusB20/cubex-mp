using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] float speed = 2f;

  
    public void moveForward()
   {
       transform.Translate(Vector3.forward * speed * Time.deltaTime);
   }
   public void moveDownwards()
   {
       transform.Translate(-Vector3.forward * speed * Time.deltaTime);
   }
   public void moveLeft()
   {
       transform.Translate(Vector3.left * speed * Time.deltaTime);
   }
   public void moveRight()
   {
       transform.Translate(-Vector3.left* speed * Time.deltaTime);
   }
}

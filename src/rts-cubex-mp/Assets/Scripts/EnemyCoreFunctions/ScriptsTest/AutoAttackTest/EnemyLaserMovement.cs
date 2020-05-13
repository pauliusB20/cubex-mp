using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserMovement : MonoBehaviour
{
    Rigidbody laserMove;

    [SerializeField] float thrust=2f;
    [SerializeField] bool hittedPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        laserMove=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        laserMove.velocity=transform.TransformDirection(new Vector3(0f,0f,thrust));
    }
     private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "playerTroop"){
            hittedPlayer = true;
            Debug.Log("I hitted the player: " + hittedPlayer);
            Destroy(gameObject);
        }
     }  
     private void OnTriggerExit(Collider other)
     {
         hittedPlayer = false;
     } 
}

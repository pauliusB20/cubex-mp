using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergonRepo : MonoBehaviour
{
   
   [SerializeField] float speed = 2f;
   [SerializeField] GameObject energonRepoBody;
       // Update is called once per frame
   
   //Fix animation

    void Update()
    {
        energonRepoBody.transform.Rotate(new Vector3(45, 45, 45) * speed * Time.deltaTime);
    }

  
}

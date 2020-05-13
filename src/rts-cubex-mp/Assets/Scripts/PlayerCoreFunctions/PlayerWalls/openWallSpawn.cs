using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openWallSpawn : MonoBehaviour
{
    [SerializeField] GameObject menu;
    //[SerializeField] PanelCode code;
    void Start()
    {

    }
    private void OnMouseDown() {
        menu.SetActive(true);
        
    }
}

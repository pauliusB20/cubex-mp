using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWallRotate : MonoBehaviour
{
    [SerializeField] GameObject menu;
    //[SerializeField] PanelCode code;
    [SerializeField] GameObject Wall;
    private void OnMouseDown() {
        //FindObjectOfType<PanelCodeManager>().setCurrentPanelCode(FindObjectOfType<PanelCode>().getPanelCode());
        menu.SetActive(true);
    }
    public void destroyWall(){
        Destroy(Wall);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAll : MonoBehaviour {
    [SerializeField] Text select;
    private int count;
    private void Update() {
        count=0;
        int size = FindObjectsOfType<ClickOn> ().Length;
        ClickOn[] unitNumber = FindObjectsOfType<ClickOn> ();
        for (int j = 0; j < size; j++) {
            if(unitNumber[j].isSelected){
                count++;
            }
        }
        if(count == 0){
            select.text ="Select All";
        }
    }
    public void selectAllUnits () {
        count=0;
        int size = FindObjectsOfType<ClickOn> ().Length;
        ClickOn[] unitNumber = FindObjectsOfType<ClickOn> ();
        if (select.text == "Select All") {
            for (int i = 0; i < size; i++) {
            unitNumber[i].isSelected = true;
            unitNumber[i].ClickMe ();
            count++;
        }
            select.text ="Deselect All";
        }
        else {
            for (int i = 0; i < size; i++) {
            unitNumber[i].isSelected = false;
            unitNumber[i].ClickMe ();
            count++;
        }
            select.text ="Select All";
        }
    }
}
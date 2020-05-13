using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openErrorPanel : MonoBehaviour
{
    [SerializeField] GameObject errorPanel;
   // [SerializeField] PanelCode code;
    [SerializeField] Text errorText;
    public void openError () {
        errorPanel.SetActive (true);
    }
    public void setText(string text){
        errorText.text=text;
    }
}

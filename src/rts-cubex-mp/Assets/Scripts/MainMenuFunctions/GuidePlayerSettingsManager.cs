using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidePlayerSettingsManager : MonoBehaviour
{
    [Header("Save settings for tutorial tracker")]
    [SerializeField] int statusCompletion = 0;
    [SerializeField] string settingsCode = "BFTS";
    
    public void setTutorialStatus()
    {
        PlayerPrefs.SetInt(settingsCode, statusCompletion);
    }
}

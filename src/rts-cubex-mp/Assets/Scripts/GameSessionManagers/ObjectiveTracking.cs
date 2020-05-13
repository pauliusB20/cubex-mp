using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveTracking : MonoBehaviour
{
    private Text objText;
    private CurrentLevelManager lm;
    private bool textAdded = false;
    void Start()
    {
        lm = FindObjectOfType<CurrentLevelManager>();
        objText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lm.LevelDone() && !textAdded)
        {
            objText.text += lm.EnemyBasesDestroyed() ? "done" : "failed";
            
            textAdded = true;
        }
    }
}

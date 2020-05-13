using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTracer : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Task tracing configuration parameters")]
    [SerializeField] float startValue = 0f;
    [SerializeField] float maxValue = 100f;
    [SerializeField] GameObject BackgroundTaskBar;
    [SerializeField] GameObject taskBar;
    [SerializeField] bool taskFinished = false;
    
    public bool TaskFinished { get { return taskFinished;} set{ taskFinished = value; }}

    public void startTaskTracking()
    {
        BackgroundTaskBar.SetActive(true);
    }
    public void addCompletionValue(float v)
    {
       if (!taskFinished) return;
       startValue += v;
       taskBar.GetComponent<RectTransform>().sizeDelta += new Vector2(startValue, 0f);
       if (v >= maxValue) 
       {
          taskBar.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 0f);
          startValue = 0;
          taskFinished = true;
           BackgroundTaskBar.SetActive(false);
       }
    }
}

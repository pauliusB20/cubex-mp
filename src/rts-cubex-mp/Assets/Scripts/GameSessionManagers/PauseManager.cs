using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] bool isGamePaused = false;
    [Range(0f, 2f)]
    [SerializeField] float gameSpeed = 1f;
    private float originalSpeed;

    private void Start() {
        originalSpeed = gameSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        if (isGamePaused)
        {
            gameSpeed = 0f;
        }
        else
        {
            gameSpeed = originalSpeed;
        }
    }

    public void pauseGame()
    {
        isGamePaused = true;
    }
    public void unpauseGame()
    {
        isGamePaused = false;
    }
}

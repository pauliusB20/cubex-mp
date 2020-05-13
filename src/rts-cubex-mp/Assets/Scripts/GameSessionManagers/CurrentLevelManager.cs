using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CurrentLevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    enum Messages {WinMsg = 0, LooseMsg = 1};

    [Header("Main Manager configuration parameters")]
    [SerializeField] GameObject gameCompletionScreen;
    [SerializeField] int enemyCount, playerCount;
    [SerializeField] float loadLevelCompliationDelay = 2f;
    [SerializeField] float loadWindowDelay = 1f;
    [SerializeField] Text statusLvlText;
    [SerializeField] Text msgText;
    [SerializeField] List<string> msgTexts;
    [SerializeField] private bool areEnemyBasesDestroyed = false;
    [SerializeField] private bool isLevelDone = false;
    //"Level managing configuration parameters"
    private string settingsName = "Lvl";
  
    private GameSession gs;
    private GameSesionTime gst;
    
    private void Start() 
    {
        if (msgTexts == null) Debug.LogError("There are no messages for level completion!");

        gs = FindObjectOfType<GameSession>();
        gst = FindObjectOfType<GameSesionTime>();
        gameCompletionScreen.SetActive(false);
    }
    void Update()
    {
         enemyCount = GameObject.FindGameObjectsWithTag("EBase").Length;
         playerCount = GameObject.FindGameObjectsWithTag("PBase").Length;

         if (enemyCount <= 0 || playerCount <= 0 || gst.timeFinished())
         {
             isLevelDone = true;
             if (enemyCount <= 0)
              areEnemyBasesDestroyed = true;
             else
                areEnemyBasesDestroyed = false; 
            
            //Setting compliation status..
            PlayerPrefs.SetInt(settingsName + SceneManager.GetActiveScene().buildIndex, areEnemyBasesDestroyed ? 1 : 0);
            FindObjectOfType<GameSession>().enemyDestroyedState(areEnemyBasesDestroyed);
            StartCoroutine(loadCompWindow());
         }
            

    }
    public bool LevelDone()
    {
        return isLevelDone;
    }
    public bool EnemyBasesDestroyed()
    {
        return areEnemyBasesDestroyed;
    }
    IEnumerator loadCompWindow()
    {
        yield return new WaitForSeconds(loadWindowDelay);
            //Load level compliation board..

             gameCompletionScreen.SetActive(true);

             //Set the current level state text
             statusLvlText.text = gs.getSessionText();

             if (enemyCount <= 0)
             {
                  msgText.text = msgTexts[(int)Messages.WinMsg];
             }
             else
             {
                  msgText.text = msgTexts[(int)Messages.LooseMsg];
             }

             StartCoroutine(loadCompScene());
    }
    IEnumerator loadCompScene()
    {
        yield return new WaitForSeconds(loadLevelCompliationDelay);
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }
}

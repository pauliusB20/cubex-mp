using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net.Mime;
public class CubexWindowManager : MonoBehaviour
{
    [SerializeField] GameStorageConf gamecnf;
     [Header("Resources management parameters")]
    [SerializeField] GameObject resourceMenuMarket;   
   
    [SerializeField] int levelindex;
    public void LoadLevel()
    {
        SceneManager.LoadScene(levelindex);
    }
    public void ResetGameSession()
    {
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void returnWithMarket()
    {
        if (gamecnf.isUserLoggedIn())
        {
            resourceMenuMarket.SetActive(true);
        }
        else
        {
             SceneManager.LoadScene(levelindex);
        }
    }
}

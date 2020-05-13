using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletionManager : MonoBehaviour
{
    // Start is called before the first frame update

     enum Levels {
         Level01Completed = 0, Level02Completed = 1, Level03Completed = 2
    };

    [Header("Configuration parameters")]
    [SerializeField] string[] levelNames;
    [SerializeField] int[] statusCodes;
    [SerializeField] Button[] levelButtons;

    private bool windowOpened = false;
    [Header("Game compliation panel")]
    [SerializeField] GameObject youWinPanel;
    void Start()
    {
        for (int sIndex = 0; sIndex < statusCodes.Length; sIndex++)
        {
            statusCodes[sIndex] = PlayerPrefs.GetInt(levelNames[sIndex]);
        }

        levelButtons[(int)Levels.Level02Completed].interactable = PlayerPrefs.GetInt(levelNames[(int)Levels.Level01Completed]) > 0;
        levelButtons[(int)Levels.Level03Completed].interactable = PlayerPrefs.GetInt(levelNames[(int)Levels.Level02Completed]) > 0;

        //If Level03 is passed, then show once the congatiulations window...
      if (PlayerPrefs.GetInt(levelNames[(int)Levels.Level03Completed]) > 0 && !windowOpened)
      {
        youWinPanel.SetActive(PlayerPrefs.GetInt(levelNames[(int)Levels.Level03Completed]) > 0);
        windowOpened = true;
      }

    }

   
}

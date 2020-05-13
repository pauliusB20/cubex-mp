using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutorialManager : MonoBehaviour
{
    [SerializeField] List<GameObject> tutorialPanels;
    [SerializeField] List<GameObject> tutorialPanelsTroopAndDefence;

    public void tutorialPanelAt(int index, bool state)
    {
        FindObjectOfType<PauseManager>().pauseGame();
        tutorialPanels[index].SetActive(state);
    }
    public void tutorialPanelTroopAt(int index, bool state)
    {
        FindObjectOfType<PauseManager>().pauseGame();
        tutorialPanelsTroopAndDefence[index].SetActive(state);
    }
}

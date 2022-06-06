using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuScreen : GUIScreens
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] private GameObject firstButton;

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(firstButton); 
    }

    public void OpenLevels()
    {
        TurnOffMainMenu();
        UIController.Open(typeof(LevelsScreen).Name); 
    }

    public void OpenTutorial()
    {
        TurnOffMainMenu();
        UIController.Open(typeof(TutorialScreen).Name);
    }


    public void OpenSetings()
    {
        TurnOffMainMenu();
        UIController.Open(typeof(SettingScreen).Name);
    }

    private void TurnOffMainMenu()
    {
        UIController.Close(typeof(MainMenuScreen).Name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

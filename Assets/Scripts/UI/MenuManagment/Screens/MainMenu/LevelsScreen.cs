using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelsScreen : GUIScreens
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] private GameObject firstLevel;

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(firstLevel);   
    }

    public void OpenMenu()
    {
        UIController.Open(typeof(MainMenuScreen).Name);
        UIController.Close(typeof(LevelsScreen).Name);
    }
}

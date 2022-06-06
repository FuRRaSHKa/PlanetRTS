using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialScreen : GUIScreens
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] private GameObject firstButton;

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(firstButton);
    }

    public void OpenMenu()
    {
        UIController.Open(typeof(MainMenuScreen).Name);
        UIController.Close(typeof(TutorialScreen).Name);
    }
}

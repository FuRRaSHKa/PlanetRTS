using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; 

public class EndGameLoseScreen : GUIScreens
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] private GameObject firstButton;

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(firstButton);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<bool> OnEndGame;
    public static event Action OnStartGame;

    private void OnDisable()
    {
        OnEndGame = null;
        OnStartGame = null;
    }

    public static void StartGame()
    {
        OnStartGame?.Invoke();
    }

    public static void EndGame(bool value)
    {
        OnEndGame?.Invoke(value);
    }

}

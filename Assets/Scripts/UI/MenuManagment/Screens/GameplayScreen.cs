using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScreen : GUIScreens
{
    protected override void Start()
    {
        base.Start();
        EventManager.OnEndGame += EndGame;
    }

    private void EndGame(bool value)
    {
        UIController.Close(this.GetType().Name); 
    }
}

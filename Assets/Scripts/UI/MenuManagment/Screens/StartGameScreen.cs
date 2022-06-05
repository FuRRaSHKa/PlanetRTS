using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameScreen : GUIScreens
{
    protected override void Start()
    {
        base.Start();
        EventManager.OnStartGame += () => 
        {
            UIController.Close(this.GetType().Name);
            UIController.Open(typeof(GameplayScreen).Name);
        };
    }
}

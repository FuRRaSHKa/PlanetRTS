using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameWinScreen : GUIScreens
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(LoadMenu());
    }

    private IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(0);
    }

}

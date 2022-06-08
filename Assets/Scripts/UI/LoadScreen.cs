using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadLevel());        
    }

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(2); 
    }
}

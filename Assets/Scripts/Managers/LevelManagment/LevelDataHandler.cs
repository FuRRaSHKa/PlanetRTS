using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataHandler : MonoBehaviour
{
    public LevelData LevelData;
    public LevelStatus LevelStatus = LevelStatus.Open;

    public static LevelDataHandler Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

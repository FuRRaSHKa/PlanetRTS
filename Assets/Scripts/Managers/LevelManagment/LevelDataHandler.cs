using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataHandler : MonoBehaviour
{
    private LevelData levelData;
    private LevelStatus levelStatus = LevelStatus.Open;

    public static LevelDataHandler Instance;

    public LevelData LevelData
    {
        get
        {
            return levelData; 
        }
        set
        {
            levelData = value; 
        }
    }

    public LevelStatus LevelStatus
    {
        get
        {
            return levelStatus;
        }
        set
        {
            levelStatus = value;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelStatus
{
    Closed, 
    Open,
    Completed
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;

    private LevelDataSaver dataSaver;

    private void Awake()
    {
        dataSaver = new LevelDataSaver();
        ReadLevelSaves();
    }

    private void Start()
    {
        ReadLevelStatus();
        SaveLevels();
        InitButtons();
    }

    private void ReadLevelStatus()
    {
        LevelData levelData = LevelDataHandler.Instance.LevelData;
        if (levelData == null)
        {
            return;
        }

        int id = levels.FindIndex(f => f.levelData == levelData);
        levels[id].levelStatus = LevelDataHandler.Instance.LevelStatus;
        if (levels[id].levelStatus == LevelStatus.Completed && id + 1 < levels.Count && levels[id + 1].levelStatus == LevelStatus.Closed)
        {
            levels[id + 1].levelStatus = LevelStatus.Open;
        }
    }

    private void ReadLevelSaves()
    {
        List<int> statuses = dataSaver.LoadLevelData();
        if (statuses == null)
        {
            return;
        }

        for (int i = 0; i < levels.Count; i++)
        {
            if (i >= statuses.Count)
            {
                return;
            }

            levels[i].levelStatus = (LevelStatus)statuses[i];
        }
    }

    private void SaveLevels()
    {
        List<int> statuses = levels.Select(s => (int)s.levelStatus).ToList();
        dataSaver.SaveLevelData(statuses);
    }

    private void InitButtons()
    {
        foreach (Level level in levels)
        {
            level.levelButtonController.SetStatus(level.levelStatus);
        }
    }

    public void LoadLevel(int id)
    {
        LevelDataHandler.Instance.LevelData = levels[id].levelData;
        SceneManager.LoadScene(1);
    }
}

[System.Serializable]
public class Level
{
    public LevelButtonController levelButtonController;

    public LevelData levelData;

    public LevelStatus levelStatus;
}

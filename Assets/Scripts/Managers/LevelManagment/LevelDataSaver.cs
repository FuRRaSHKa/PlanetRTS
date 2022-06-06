using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelDataSaver
{
    public void SaveLevelData(List<int> levelStatuses)
    {
        Statuses statuses = new Statuses() { statuses = levelStatuses };
        string statusArray = JsonUtility.ToJson(statuses);
        File.WriteAllText(Application.dataPath + "/LevelsStatus.json", statusArray);
    }

    public List<int> LoadLevelData()
    {
        if (!File.Exists(Application.dataPath + "/LevelsStatus.json"))
        {
            return null;
        }

        string savedText = File.ReadAllText(Application.dataPath + "/LevelsStatus.json");
        List<int> output = JsonUtility.FromJson<Statuses>(savedText).statuses;

        return output;
    }
}

[System.Serializable]
public class Statuses
{
    public List<int> statuses;
}

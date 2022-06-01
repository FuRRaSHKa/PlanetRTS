using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    [SerializeField] private int playerPlanetCount;
    [SerializeField] private int enemyPlanetCount;
    [SerializeField] private int freePlanetCount;

    public int PlayerPlanetCount => playerPlanetCount;
    public int EnemyPlanetCount => enemyPlanetCount;
    public int FreePlanetCount => freePlanetCount;
    

}

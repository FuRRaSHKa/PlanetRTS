using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    [Header("Planet settings")]
    [SerializeField] private int playerPlanetCount;
    [SerializeField] private int enemyPlanetCount;
    [SerializeField] private int freePlanetCount;

    [Header("Ships settings")]
    [SerializeField] private int playerShipsCount;
    [SerializeField] private int enemyShipsCount;

    public int PlayerPlanetCount => playerPlanetCount;
    public int EnemyPlanetCount => enemyPlanetCount;
    public int FreePlanetCount => freePlanetCount;
    public int PlayerShipsCount => playerShipsCount;
    public int EnemyShipsCount => enemyShipsCount;

}

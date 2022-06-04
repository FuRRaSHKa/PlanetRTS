using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    [Header("Planet settings")]
    [SerializeField] private int planetCount;
    [SerializeField] private int maxShipsPerPlanet;

    [Header("Ships settings")]
    [SerializeField] private int playerShipsCount;
    [SerializeField] private int enemyShipsCount;

    public int PlanetCount => planetCount;
    public int PlayerShipsCount => playerShipsCount;
    public int EnemyShipsCount => enemyShipsCount;
    public int MaxShipsPerPlanet => maxShipsPerPlanet;

}

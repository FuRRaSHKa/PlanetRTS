using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlanetData", menuName = "Data/PlanetData")]
public class PlanetData : ScriptableObject
{
    [Header("Ship generation")]
    [SerializeField] private float shipsGenerationInterval = 5;
    [SerializeField] private int shipGenerationCount = 3;

    [Header("Ship contest")]
    [SerializeField] private float shipContestInterval = 1;
    [SerializeField] private float capturePointsPerShip = 1;
    [SerializeField] private float captureInterval = 1;

    public float ShipsGenerationInterval => shipsGenerationInterval;
    public int ShipGenerationCount => shipGenerationCount;
    public float ShipContestInterval => shipContestInterval;
    public float CapturePointsPerShip => capturePointsPerShip;
    public float CaptureInterval => captureInterval;
}

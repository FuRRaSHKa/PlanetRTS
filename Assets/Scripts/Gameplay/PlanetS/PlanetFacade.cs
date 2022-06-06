using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFacade : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private int planetId = 0;

    public float PlanetRadius => spriteRenderer.bounds.size.x;

    public int PlanetId => planetId;

    public event Action<int, int> OnShipValueUpdate;

    public event Action<ShipSide, float> OnPlanetCapture;

    private int enemyCount = 0;
    private int playerCount = 0;

    public int EnemyCount => enemyCount;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int planetId, ShipSide shipSide = ShipSide.None)
    {
        this.planetId = planetId;
        GetComponent<PlanetStateMachine>().InitPlanet(shipSide);
    }

    public bool IsHaveEnemy()
    {
        return enemyCount > 0;
    }

    public bool IsEnemyMoreThanPlayer(int enemy)
    {
        return playerCount < enemy;
    }

    public bool CheckPosCollusion(Vector3 pos)
    {
        Bounds bounds = spriteRenderer.bounds;
        return pos.x > bounds.min.x && pos.x < bounds.max.x && pos.z > bounds.min.z && pos.z < bounds.max.z;
    }

    public void AddShip(ShipSide shipSide, int count)
    {
        if (shipSide == ShipSide.Enemy)
        {
            enemyCount += count;
        }
        else if (shipSide == ShipSide.Player)
        {
            playerCount += count;
        }

        OnShipValueUpdate?.Invoke(playerCount, enemyCount);
    }

    public void RemoveShip(ShipSide shipSide, int count)
    {
        if (shipSide == ShipSide.Enemy)
        {
            enemyCount -= count;
            enemyCount = enemyCount < 0 ? 0 : enemyCount;
        }
        else if (shipSide == ShipSide.Player)
        {
            playerCount -= count;
            playerCount = playerCount < 0 ? 0 : playerCount;
        }

        OnShipValueUpdate?.Invoke(playerCount, enemyCount);
    }

    public void CapturePlanet(ShipSide shipSide, float value)
    {
        OnPlanetCapture?.Invoke(shipSide, value);
    }

    private void OnDisable()
    {
        OnShipValueUpdate = null;
    }
}

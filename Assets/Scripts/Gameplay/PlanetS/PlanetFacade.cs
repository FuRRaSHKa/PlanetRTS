using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFacade : MonoBehaviour
{
    [SerializeField] private PlanetEffectController planetEffectController;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private int planetId = 0;
    private int enemyCount = 0;
    private int playerCount = 0;

    public float PlanetRadius => spriteRenderer.bounds.size.x;
    public int PlanetId => planetId;
    public int EnemyCount => enemyCount;

    public event Action<int, int> OnShipValueUpdate;
    public event Action<ShipSide, float> OnPlanetCapture;

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

    public void PlayFightEffect(bool value)
    {
        planetEffectController.SetFightEffect(value);
    }

    private void OnDisable()
    {
        OnShipValueUpdate = null;
    }
}

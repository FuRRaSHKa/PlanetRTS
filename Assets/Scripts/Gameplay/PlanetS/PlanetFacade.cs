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

    public event Action<ShipSide, int> OnShipComing;
    public event Action<ShipSide, int> OnShipLeaving;
    public event Action<ShipSide, float> OnPlanetCapture;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int planetId, ShipSide shipSide = ShipSide.None)
    {
        this.planetId = planetId;
        GetComponent<PlanetStateMachine>().InitPlanet(shipSide);
    }

    public bool CheckPosCollusion(Vector3 pos)
    {
        Bounds bounds = spriteRenderer.bounds;
        return pos.x > bounds.min.x && pos.x < bounds.max.x && pos.z > bounds.min.z && pos.z < bounds.max.z;
    }

    public void AddShip(ShipSide shipSide, int count)
    {
        OnShipComing?.Invoke(shipSide, count);
    }

    public void RemoveShip(ShipSide shipSide, int count)
    {
        OnShipLeaving?.Invoke(shipSide, count);
    }

    public void CapturePlanet(ShipSide shipSide, float value)
    {
        OnPlanetCapture?.Invoke(shipSide, value); 
    }

    private void OnDisable()
    {
        OnShipComing = null;
        OnShipLeaving = null;
    }
}

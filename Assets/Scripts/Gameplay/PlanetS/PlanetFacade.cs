using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFacade : MonoBehaviour
{
    private PlanetCursor planetCursor;
    private SpriteRenderer spriteRenderer;

    private int planetId = 0;

    public float PlanetRadius => spriteRenderer.bounds.size.x;

    public int PlanetId => planetId;

    public event Action<ShipSide> OnShipComing;
    public event Action<ShipSide> OnShipLeaving;

    private void Awake()
    {
        planetCursor = GetComponent<PlanetCursor>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int planetId)
    {
        this.planetId = planetId;
    }

    public bool CheckPosCollusion(Vector3 pos)
    {
        Bounds bounds = spriteRenderer.bounds;
        return pos.x > bounds.min.x && pos.x < bounds.max.x && pos.z > bounds.min.z && pos.z < bounds.max.z;
    }

    public void ChoosePlanet(bool value)
    {
        if (value)
        {
            planetCursor.ShowCursor();
        }
        else
        {
            planetCursor.HideCursor();
        }
    }

    public void AddShip(ShipSide shipSide)
    {
        OnShipComing?.Invoke(shipSide);
    }

    public void RemoveShip(ShipSide shipSide)
    {
        OnShipLeaving?.Invoke(shipSide);
    }

    private void OnDisable()
    {
        OnShipComing = null;
        OnShipLeaving = null;
    }
}

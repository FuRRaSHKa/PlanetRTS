using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFacade : MonoBehaviour
{
    private PlanetShipInteractor planetShipCounter;
    private PlanetCursor planetCursor;
    private SpriteRenderer spriteRenderer;

    private int planetId = 0;

    public float PlanetRadius => spriteRenderer.bounds.size.x;

    public int PlanetId => planetId;

    private void Awake()
    {
        planetShipCounter = GetComponent<PlanetShipInteractor>();
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
        planetShipCounter.AddShip(shipSide);
    }

    public void RemoveShip(ShipSide shipSide)
    {
        planetShipCounter.RemoveShip(shipSide);
    }
}

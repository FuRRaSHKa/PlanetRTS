using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFacade : MonoBehaviour
{
    private PlanetShipHandler planetShipHandler;
    private PlanetCursor planetCursor;
    private SpriteRenderer spriteRenderer;

    public float PlanetRadius => spriteRenderer.bounds.size.x;

    private void Awake()
    {
        planetShipHandler = GetComponent<PlanetShipHandler>();
        planetCursor = GetComponent<PlanetCursor>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool CheckPosCollusion(Vector3 pos)
    {
        Bounds bounds = spriteRenderer.bounds;
        return pos.x > bounds.min.x && pos.x < bounds.max.x && pos.y > bounds.min.y && pos.y < bounds.max.y;
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

    public void SendShips(PlanetFacade targetPlanetFacade)
    {
        planetShipHandler.SendShips(targetPlanetFacade);
    }
}

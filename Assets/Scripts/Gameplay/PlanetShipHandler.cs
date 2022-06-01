using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShipHandler : MonoBehaviour
{
    private List<ShipMovement> ships;

    private Bounds bounds;

    private void Awake()
    {
        
    }

    public void SendShips(PlanetShipHandler targetPlanetShipHandler)
    {
        for (int i = 0; i < ships.Count; i++)
        {
            ships[i].MoveTo(targetPlanetShipHandler);
        }
    }

    public bool CheckPosCollusion(Vector2 pos)
    {
        return bounds.Contains(pos);
    }
}

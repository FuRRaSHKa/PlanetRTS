using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShipHandler : MonoBehaviour
{
    private List<ShipMovement> ships;

    public void SendShips(PlanetFacade planetFacade)
    {
        for (int i = 0; i < ships.Count; i++)
        {
            ships[i].MoveTo(planetFacade);
        }
    }
  
}

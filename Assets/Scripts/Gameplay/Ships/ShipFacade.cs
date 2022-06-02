using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFacade : MonoBehaviour
{
    [SerializeField] private ShipDataHandler shipDataHandler;
    [SerializeField] private ShipMovement shipMovement;

    public void Init(ShipSide shipSide, PlanetFacade planetFacade)
    {
        shipDataHandler.InitShip(shipSide);
        shipMovement.MoveToPlanet(planetFacade);
    }

    public void SendTo(PlanetFacade planet)
    {
        shipMovement.MoveToPlanet(planet);
    }

    public bool IsWithPlanet(int targetPlanetid)
    {
        return shipMovement.PlanetId == targetPlanetid;
    }
}

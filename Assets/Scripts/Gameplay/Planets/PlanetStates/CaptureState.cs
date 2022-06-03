using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureState : PlanetState
{
    private int shipCount;

    private ShipSide shipSide;
    private PlanetFacade planetFacade;

    public CaptureState(float updateInterval, PlanetFacade planetFacade, ShipSide shipSide, int shipCount, PlanetStateName planetStateName) : base(updateInterval, planetStateName) 
    {
        this.shipSide = shipSide;
        this.planetFacade = planetFacade;
        this.shipCount = shipCount;
    }

    public override void Update()
    {
        ShipHandler.Instance.IncreaseShipCount(planetFacade, shipCount, shipSide);
    }
}

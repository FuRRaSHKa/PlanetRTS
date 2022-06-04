using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContestState : PlanetState
{
    private PlanetFacade planetFacade;

    public ContestState(float updateInterval, PlanetFacade planetFacade, PlanetStateName planetStateName) : base(updateInterval, planetStateName) 
    {
        this.planetFacade = planetFacade;
    }

    public override void Update()
    {
        ShipHandler.Instance.TryDuelShips(planetFacade.PlanetId);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContestState : PlanetState
{
    private int planetId = -1;

    public ContestState(float updateInterval, int planetId, PlanetStateName planetStateName) : base(updateInterval, planetStateName) 
    {
        this.planetId = planetId;
    }

    public override void Update()
    {
        ShipHandler.Instance.TryDuelShips(planetId);
    }
}

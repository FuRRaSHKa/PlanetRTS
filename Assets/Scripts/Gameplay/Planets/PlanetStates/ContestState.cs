using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContestState : PlanetState
{
    private PlanetFacade planetFacade;

    public ContestState(PlanetStateMachine planetStateMachine, float updateInterval, PlanetFacade planetFacade, PlanetStateName planetStateName) : base(planetStateMachine, updateInterval, planetStateName)
    {
        this.planetFacade = planetFacade;
    }

    public override void Enter()
    {
        planetFacade.PlayFightEffect(true);
    }

    public override void Exit()
    {
        planetFacade.PlayFightEffect(false);
    }

    public override void ShipValueUpdate(int playerShips, int enemyShip)
    {
        if (playerShips == 0 || enemyShip == 0)
        {
            planetStateMachine.ChangeState(PlanetStateName.Capturing); 
        }
    }

    public override void Update()
    {
        ShipHandler.Instance.TryDuelShips(planetFacade.PlanetId);
    }
}

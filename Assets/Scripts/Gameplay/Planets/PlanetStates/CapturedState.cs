using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturedState : PlanetState
{
    private int shipCount;

    private ShipSide shipSide;
    private PlanetFacade planetFacade;

    public CapturedState(PlanetStateMachine planetStateMachine, float updateInterval, PlanetFacade planetFacade, ShipSide shipSide, int shipCount, PlanetStateName planetStateName)
        : base(planetStateMachine,updateInterval, planetStateName) 
    {
        this.shipSide = shipSide;
        this.planetFacade = planetFacade;
        this.shipCount = shipCount;
    }

    public override void Enter()
    {
       
    }

    public override void Exit()
    {
       
    }

    public override void ShipValueUpdate(int playerShips, int enemyShip)
    {
        if (playerShips != 0 && shipSide == ShipSide.Enemy)
        {
            if (enemyShip != 0)
            {
                planetStateMachine.ChangeState(PlanetStateName.Fighting);
            }
            else
            {
                planetStateMachine.ChangeState(PlanetStateName.Capturing);
            }
        }
        else if (enemyShip != 0 && shipSide == ShipSide.Player)
        {
            if (playerShips != 0)
            {
                planetStateMachine.ChangeState(PlanetStateName.Fighting);
            }
            else
            {
                planetStateMachine.ChangeState(PlanetStateName.Capturing);
            }
        }
    }

    public override void Update()
    {
        ShipHandler.Instance.IncreaseShipCount(planetFacade, shipCount, shipSide);
    }
}

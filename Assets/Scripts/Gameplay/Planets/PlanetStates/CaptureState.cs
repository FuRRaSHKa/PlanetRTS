using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureState : PlanetState
{
    private float playerPoints = 0;
    private float enemyPoints = 0;

    private int enemyShips = 0;
    private int playerShips = 0;

    private float capturingValuePerShip = 1;

    private ShipSide targetSide = ShipSide.None;

    private PlanetFacade planetFacade;
    private PlayerSoundController playerSoundController;

    public CaptureState(PlanetStateMachine planetStateMachine, PlanetFacade planetFacade, PlayerSoundController playerSoundController, float updateInterval, PlanetStateName planetStateName, float capturingValuePerShip) 
        : base(planetStateMachine, updateInterval, planetStateName)
    {
        this.playerSoundController = playerSoundController;
        this.capturingValuePerShip = capturingValuePerShip;
        this.planetFacade = planetFacade;     
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {
        planetFacade.CapturePlanet(ShipSide.Player, 0);
        planetFacade.CapturePlanet(ShipSide.Enemy, 0);

        playerSoundController.PlayCapturedCignal(targetSide);
    }

    public override void ShipValueUpdate(int playerShips, int enemyShips)
    {
        this.playerShips = playerShips;
        this.enemyShips = enemyShips;

        if (enemyShips != 0)
        {
            if (playerShips <= 0)
            {
                targetSide = ShipSide.Enemy;
            }
            else
            {
                targetSide = ShipSide.None;
                planetStateMachine.ChangeState(PlanetStateName.Fighting);
            }

            return;
        }

        if (playerShips != 0)
        {
            if (enemyShips <= 0)
            {
                targetSide = ShipSide.Player;
            }
            else
            {
                targetSide = ShipSide.None;
                planetStateMachine.ChangeState(PlanetStateName.Fighting);
            }

            return;
        }
    }

    public override void Update()
    {
        if (targetSide != ShipSide.None)
        {
            planetFacade.CapturePlanet(ShipSide.Player, playerPoints / 100f);
            planetFacade.CapturePlanet(ShipSide.Enemy, enemyPoints / 100f);
        }      

        if (targetSide == ShipSide.Player)
        {
            PlayerCapturing();
        }
        else if (targetSide == ShipSide.Enemy)
        {
            EnemyCapturing();
        }       
    }

    private void EnemyCapturing()
    {
        if (playerPoints > 0)
        {
            playerPoints -= enemyShips * capturingValuePerShip;
        }
        else
        {
            enemyPoints += capturingValuePerShip * enemyShips;
        }

        if (enemyPoints >= 100)
        {
            enemyPoints = 100;
            planetStateMachine.ChangeState(PlanetStateName.EnemyCaptured);
        }
    }

    private void PlayerCapturing()
    {
        if (enemyPoints > 0)
        {
            enemyPoints -= playerShips * capturingValuePerShip;
        }
        else
        {
            playerPoints += capturingValuePerShip * playerShips;
        }

        if (playerPoints >= 100)
        {
            playerPoints = 100;
            planetStateMachine.ChangeState(PlanetStateName.PlayerCaptured);
        }
    }
}

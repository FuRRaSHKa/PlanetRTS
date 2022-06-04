using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum PlanetStateName
{
    None = -1,
    Contesting,
    PlayerCapture,
    EnemyCapture
}

public class PlanetStateMachine : MonoBehaviour
{
    [SerializeField] private PlanetData planetData;

    [SerializeField] private int playerShipCount;
    [SerializeField] private int enemyShipCount;

    private PlanetFacade planetFacade;

    private PlanetStateName currentStateName;

    private PlanetState currentState;
    private PlanetState[] states;

    public void Awake()
    {
        planetFacade = GetComponent<PlanetFacade>();

        planetFacade.OnShipComing += AddShip;
        planetFacade.OnShipLeaving += RemoveShip;

        states = new PlanetState[3];
        states[0] = new ContestState(planetData.ShipContestInterval, planetFacade, PlanetStateName.Contesting);
        states[1] = new CaptureState(planetData.ShipsGenerationInterval, planetFacade, ShipSide.Enemy, planetData.ShipGenerationCount, PlanetStateName.EnemyCapture);
        states[2] = new CaptureState(planetData.ShipsGenerationInterval, planetFacade, ShipSide.Player, planetData.ShipGenerationCount, PlanetStateName.PlayerCapture);
    }

    public void RemoveShip(ShipSide shipSide, int count)
    {
        if (shipSide == ShipSide.Player)
        {
            playerShipCount -= count;
            playerShipCount = playerShipCount < 0 ? 0 : playerShipCount;
        }
        else
        {
            enemyShipCount -= count;
            enemyShipCount = enemyShipCount < 0 ? 0 : enemyShipCount;
        }

        CheckState();
    }

    public void AddShip(ShipSide shipSide, int count)
    {
        if (shipSide == ShipSide.Player)
        {
            playerShipCount += count;
        }
        else
        {
            enemyShipCount += count;
        }

        CheckState();
    }

    private void CheckState()
    {
        PlanetStateName nameNew = currentStateName;
        if (playerShipCount != 0 && enemyShipCount != 0)
        {
            nameNew = PlanetStateName.Contesting;
        }
        else
        {
            if (playerShipCount != 0)
            {
                nameNew = PlanetStateName.PlayerCapture;
            }
            else if (enemyShipCount != 0)
            {
                nameNew = PlanetStateName.EnemyCapture;
            }
        }

        if (nameNew != currentStateName)
        {
            if (currentStateName != PlanetStateName.None)
            {
                StopAllCoroutines();
            }

            currentStateName = nameNew;
            ChangeState();
        }
    }

    private void ChangeState()
    {
        currentState = states.DefaultIfEmpty(null).First(f => f.PlanetStateName == currentStateName);
        StartCoroutine(StateUpdate());
    }

    private IEnumerator StateUpdate()
    {
        if (currentStateName == PlanetStateName.None || currentState == null)
            yield break;

        yield return new WaitForSeconds(1);

        while (true)
        {
            currentState.Update();
            yield return new WaitForSeconds(currentState.UpdateInterval);
        }
    }

}

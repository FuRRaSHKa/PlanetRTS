using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum PlanetStateName
{
    None = -1,
    Fighting,
    Capturing,
    PlayerCaptured,
    EnemyCaptured
}

public class PlanetStateMachine : MonoBehaviour
{
    [SerializeField] private PlanetData planetData;

    [SerializeField] private PlayerSoundController playerSoundController;

    private PlanetFacade planetFacade;

    private PlanetStateName currentStateName;

    private PlanetState currentState;
    private PlanetState[] states;

    private bool isGameStarted = false;

    private int playerShipCount;
    private int enemyShipCount;

    public void Awake()
    {
        planetFacade = GetComponent<PlanetFacade>();

        planetFacade.OnShipValueUpdate += UpdateShipvalue;

        states = new PlanetState[4];
        states[0] = new ContestState(this, planetData.ShipContestInterval, planetFacade, PlanetStateName.Fighting);
        states[1] = new CapturedState(this, planetData.ShipsGenerationInterval, planetFacade, ShipSide.Enemy, planetData.ShipGenerationCount, PlanetStateName.EnemyCaptured);
        states[2] = new CapturedState(this, planetData.ShipsGenerationInterval, planetFacade, ShipSide.Player, planetData.ShipGenerationCount, PlanetStateName.PlayerCaptured);
        states[3] = new CaptureState(this, planetFacade, playerSoundController, planetData.CaptureInterval, PlanetStateName.Capturing, planetData.CapturePointsPerShip);

        EventManager.OnStartGame += () => isGameStarted = true;
        EventManager.OnEndGame += (value) => isGameStarted = false;
    }

    public void InitPlanet(ShipSide startShipSide)
    {
        if (startShipSide == ShipSide.None)
        {
            ChangeState(PlanetStateName.Capturing);
        }
        else if (startShipSide == ShipSide.Player)
        {
            ChangeState(PlanetStateName.PlayerCaptured);
        }
        else if (startShipSide == ShipSide.Enemy)
        {
            ChangeState(PlanetStateName.EnemyCaptured);
        }

        StartCoroutine(StateUpdate());
    }

    public void UpdateShipvalue(int playerShipCount, int enemyShipCount)
    {
        this.playerShipCount = playerShipCount;
        this.enemyShipCount = enemyShipCount;

        currentState?.ShipValueUpdate(playerShipCount, enemyShipCount);
    }

    public void ChangeState(PlanetStateName planetStateName)
    {
        currentState?.Exit();
        currentState = states.DefaultIfEmpty(null).FirstOrDefault(f => f.PlanetStateName == planetStateName);
        currentStateName = planetStateName;
        currentState?.Enter();

        currentState?.ShipValueUpdate(playerShipCount, enemyShipCount);
    }

    private IEnumerator StateUpdate()
    {
        if (currentStateName == PlanetStateName.None || currentState == null)
            yield break;

        yield return new WaitForSeconds(1);

        while (true)
        {
            if (!isGameStarted)
            {
                yield return new WaitForSeconds(currentState.UpdateInterval);
                continue;
            }

            currentState.Update();
            yield return new WaitForSeconds(currentState.UpdateInterval);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

}

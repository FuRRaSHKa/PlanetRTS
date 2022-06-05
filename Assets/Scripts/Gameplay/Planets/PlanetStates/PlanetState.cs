using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlanetState
{
    protected float updateInterval = 0;

    protected PlanetStateName planetStateName;

    protected PlanetStateMachine planetStateMachine;

    public float UpdateInterval => updateInterval;
    public PlanetStateName PlanetStateName => planetStateName;

    public PlanetState(PlanetStateMachine planetStateMachine, float updateInterval, PlanetStateName planetStateName)
    {
        this.planetStateName = planetStateName;
        this.updateInterval = updateInterval;
        this.planetStateMachine = planetStateMachine;
    }

    public abstract void Update();

    public abstract void Enter();

    public abstract void Exit();

    public abstract void ShipValueUpdate(int playerShips, int enemyShip);

}

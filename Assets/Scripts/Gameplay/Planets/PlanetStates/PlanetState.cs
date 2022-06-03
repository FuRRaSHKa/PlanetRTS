using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlanetState
{
    protected float updateInterval = 0;

    protected PlanetStateName planetStateName;

    public float UpdateInterval => updateInterval;
    public PlanetStateName PlanetStateName => planetStateName;

    public PlanetState(float updateInterval, PlanetStateName planetStateName)
    {
        this.planetStateName = planetStateName;
        this.updateInterval = updateInterval;
    }

    public abstract void Update();

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float threshold = .5f;

    private NavMeshAgent navMeshAgent;
    private PlanetFacade targetPlanet;
    private ShipDataHandler shipDataHandler;

    private int planetId = -1;

    public int PlanetId => planetId; 

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        shipDataHandler = GetComponent<ShipDataHandler>();
    }

    public void MoveToPlanet(PlanetFacade planet)
    {
        navMeshAgent.isStopped = false;
        planetId = -1; 
        navMeshAgent.SetDestination(planet.transform.position);
    }

    private void ReachedPlanet()
    {
        planetId = targetPlanet.PlanetId;
        targetPlanet.AddShip(shipDataHandler.CurrentShipSide);
    }

    private void Update()
    {
        if (navMeshAgent.isStopped)
        {
            return;
        }

        if (navMeshAgent.remainingDistance < threshold)
        {
            navMeshAgent.isStopped = true;
            ReachedPlanet();
        }
    }
}

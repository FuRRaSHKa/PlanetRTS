using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float threshold = .7f;

    private NavMeshAgent navMeshAgent;
    private PlanetFacade targetPlanet;
    private ShipDataHandler shipDataHandler;

    private int planetId = -1;

    private Vector3 targetPos;

    private bool isMoving = false;

    public int PlanetId => planetId;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        shipDataHandler = GetComponent<ShipDataHandler>();
    }

    public void MoveToPlanet(PlanetFacade planet)
    {
        planetId = -1;
        isMoving = true;

        Vector3 pos = UnityEngine.Random.insideUnitCircle.normalized / 3;
        pos.z = pos.y;
        pos.y = 0;

        LeavePlanet();
        targetPlanet = planet;
        targetPos = planet.transform.position + pos;
        targetPos.y = transform.position.y;

        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(targetPos);
    }

    private void ReachedPlanet()
    {
        isMoving = false;
        planetId = targetPlanet.PlanetId;
        targetPlanet.AddShip(shipDataHandler.CurrentShipSide);
    }

    private void Update()
    {
        if (!isMoving)
        {
            return;
        }

        if ((transform.position - targetPos).magnitude < threshold)
        {
            ReachedPlanet();
        }
    }

    private void LeavePlanet()
    {
        targetPlanet?.RemoveShip(shipDataHandler.CurrentShipSide);
        targetPlanet = null;
    }

    private void OnDisable()
    {
        LeavePlanet();
        navMeshAgent.enabled = false; 
    }
}

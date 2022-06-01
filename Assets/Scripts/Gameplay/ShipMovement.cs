using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float threshold = .5f;

    private NavMeshAgent navMeshAgent;

    private Action onWaypointReached;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(PlanetShipHandler planet, Action onWaypointReached = null)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(planet.transform.position);
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
            onWaypointReached?.Invoke();
        }
    }
}

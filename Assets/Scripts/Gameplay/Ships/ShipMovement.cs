using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float threshold = .7f;

    private NavMeshAgent navMeshAgent;

    private Vector3 targetPos;

    private bool isMoving = false;

    private Action onPlanetReached;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPlanet(PlanetFacade planet, Action callback = null)
    {
        isMoving = true;
        onPlanetReached = callback;

        Vector3 pos = UnityEngine.Random.insideUnitCircle.normalized / 3;
        pos.z = pos.y;
        pos.y = 0;

        targetPos = planet.transform.position + pos;
        targetPos.y = transform.position.y;

        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(targetPos);
    }

    private void ReachedPlanet()
    {
        isMoving = false;
        onPlanetReached?.Invoke();
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

    private void OnDisable()
    {
        navMeshAgent.enabled = false; 
    }
}

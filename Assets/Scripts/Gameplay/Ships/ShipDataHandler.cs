using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ShipSide
{
    Player,
    Enemy
}

public class ShipDataHandler : MonoBehaviour
{
    [SerializeField] private ShipData shipData;
    [SerializeField] private ShipColoring shipColoring;

    private NavMeshAgent navMeshAgent;

    private ShipSide shipSide;

    public ShipSide CurrentShipSide => shipSide;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = shipData.ShipSpeed;
    }

    public void InitShip(ShipSide shipSide)
    {
        this.shipSide = shipSide;
        shipColoring.ColorShip(shipSide);
    }
}

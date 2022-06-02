using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShipHandler : MonoBehaviour
{
    public static ShipHandler Instance;

    private List<ShipMovement> playerShips;
    private List<ShipMovement> enemyShips;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public bool TryDuelShips(int planetID)
    {
        ShipMovement playerShip = playerShips.DefaultIfEmpty(null).First(f => f.PlanetId == planetID);
        ShipMovement enemyShip = enemyShips.DefaultIfEmpty(null).First(f => f.PlanetId == planetID);

        if (playerShip != null && enemyShip != null)
        {
            return false;
        }

        enemyShips.Remove(enemyShip);
        playerShips.Remove(playerShip);

        playerShip.enabled = false;
        enemyShip.enabled = false;

        return true;
    }

    public void IncreaseShipCount()
    {

    }
}

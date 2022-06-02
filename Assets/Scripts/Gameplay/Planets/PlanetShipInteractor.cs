using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShipInteractor : MonoBehaviour
{
    private int playerShipCount;
    private int enemyShipCount;

    public void RemoveShip(ShipSide shipSide)
    {
        if (shipSide == ShipSide.Player)
        {
            playerShipCount--;
            playerShipCount = playerShipCount < 0 ? 0 : playerShipCount;
        }
        else 
        {
            enemyShipCount--;
            enemyShipCount = enemyShipCount < 0 ? 0 : enemyShipCount;
        } 
    }

    public void AddShip(ShipSide shipSide)
    {
        if (shipSide == ShipSide.Player)
        {
            playerShipCount++;
        }
        else
        {
            enemyShipCount++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipColorData : ScriptableObject
{
    [SerializeField] private Color playerShipColor;
    [SerializeField] private Color enemyShipColor;

    public Color GetColor(ShipSide shipSide)
    {
        return shipSide == ShipSide.Player ? playerShipColor : enemyShipColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipColorData", menuName = "Data/ShipColorData", order = 0)]
public class ShipColorData : ScriptableObject
{
    [SerializeField] private Color playerShipColor;
    [SerializeField] private Color enemyShipColor;

    public Color GetColor(ShipSide shipSide)
    {
        return shipSide == ShipSide.Player ? playerShipColor : enemyShipColor;
    }
}

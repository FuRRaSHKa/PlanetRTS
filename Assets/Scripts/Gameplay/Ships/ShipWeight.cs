using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeight : MonoBehaviour
{
    [SerializeField] private ShipData shipData;
    private int shipWeight = 1;

    public void InitShip()
    {
        shipWeight = 1;
        transform.localScale = Vector3.one;
    }

    public void AddWeight(PlanetFacade planetFacade, ShipSide shipSide, int weight)
    {
        shipWeight += weight;
        transform.localScale += new Vector3(1, 0, 1) * weight * shipData.ShipScaleMultiplier;
        planetFacade.AddShip(shipSide, weight);
    }

    public bool TakeDamage(PlanetFacade planetFacade, ShipSide shipSide, int damage)
    {
        shipWeight -= damage;
        planetFacade.RemoveShip(shipSide, damage);
        transform.localScale -= new Vector3(1, 0, 1) * damage * shipData.ShipScaleMultiplier;

        return shipWeight <= 0;
    }

    public void SendAdWeight(PlanetFacade planetFacade, ShipSide shipSide)
    {
        planetFacade.AddShip(shipSide, shipWeight);
    }

    public void SendRemoveWeight(PlanetFacade planetFacade, ShipSide shipSide)
    {
        planetFacade.RemoveShip(shipSide, shipWeight);
    }
}

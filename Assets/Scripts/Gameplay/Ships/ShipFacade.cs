using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFacade : MonoBehaviour
{
    [SerializeField] private ShipDataHandler shipDataHandler;
    [SerializeField] private ShipMovement shipMovement;
    [SerializeField] private ShipWeight shipWeight;

    private PlanetFacade targetPlanet;
    private ShipSide shipSide;

    private int currentPlanetId;

    public void Init(ShipSide shipSide, PlanetFacade planetFacade)
    {
        this.shipSide = shipSide;
        
        shipMovement.MoveToPlanet(planetFacade);
        shipWeight.InitShip();
        PlanetReached(planetFacade);
        shipDataHandler.InitShip(shipSide);
    }

    public void SendToPlanet(PlanetFacade planet)
    {
        LeavePlanet();
        shipMovement.MoveToPlanet(planet, () => PlanetReached(planet));
    }

    private void PlanetReached(PlanetFacade planet)
    {
        targetPlanet = planet;
        currentPlanetId = targetPlanet.PlanetId;
        shipWeight.SendAdWeight(targetPlanet, shipSide);
    }

    private void LeavePlanet()
    {
        shipWeight.SendRemoveWeight(targetPlanet, shipSide);
    }

    public bool IsWithPlanet(int targetPlanetid)
    {
        return currentPlanetId == targetPlanetid;
    }

    public bool GetDamage(int damage)
    {
        return shipWeight.TakeDamage(targetPlanet, shipSide, damage);
    }

    public void AddWeight(PlanetFacade planetFacade, ShipSide shipSide, int weight)
    {
        shipWeight.AddWeight(planetFacade, shipSide, weight);
    }
}

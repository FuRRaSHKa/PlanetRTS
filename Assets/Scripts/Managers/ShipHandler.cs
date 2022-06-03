using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ShipHandler : MonoBehaviour
{
    public static ShipHandler Instance;

    [SerializeField] private ShipPool shipPool;

    private List<ShipFacade> playerShips = new List<ShipFacade>();
    private List<ShipFacade> enemyShips = new List<ShipFacade>();

    public event Action<float> OnProgressChange; 

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
        ShipFacade playerShip = playerShips.DefaultIfEmpty(null).FirstOrDefault(f => f.IsWithPlanet(planetID));
        ShipFacade enemyShip = enemyShips.DefaultIfEmpty(null).FirstOrDefault(f => f.IsWithPlanet(planetID));

        if (playerShip == null || enemyShip == null)
        {
            return false;
        }

        enemyShips.Remove(enemyShip);
        playerShips.Remove(playerShip);

        SendProgress();
        playerShip.gameObject.SetActive(false);
        enemyShip.gameObject.SetActive(false);

        return true;
    }

    public void IncreaseShipCount(PlanetFacade planetFacade, int shipCount, ShipSide shipSide)
    {
        for (int i = 0; i < shipCount; i++)
        {
            ShipFacade ship = shipPool.SpawnShip().GetComponent<ShipFacade>();

            Vector3 pos = UnityEngine.Random.insideUnitCircle.normalized;
            pos.z = pos.y;
            pos.y = 0;

            ship.transform.position = planetFacade.transform.position + pos;

            if (shipSide == ShipSide.Player)
            {
                playerShips.Add(ship);
            }
            else
            {
                enemyShips.Add(ship);
            }

            SendProgress();
            ship.Init(shipSide, planetFacade);
        }
    }

    private void SendProgress()
    {
        OnProgressChange?.Invoke(playerShips.Count / (float)(playerShips.Count + enemyShips.Count));
    }

    public void SendPlayerShips(int planetID, PlanetFacade planetFacade)
    {
        IEnumerable enumerator = playerShips.Where(w => w.IsWithPlanet(planetID));

        foreach (ShipFacade ship in enumerator)
        {
            ship.SendTo(planetFacade);
        }
    }

}

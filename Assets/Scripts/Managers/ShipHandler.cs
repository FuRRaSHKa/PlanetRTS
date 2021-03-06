using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ShipHandler : MonoBehaviour
{
    public static ShipHandler Instance;

    [SerializeField] private LevelData levelData;
    [SerializeField] private ShipPool shipPool;

    private List<ShipFacade> playerShips = new List<ShipFacade>();
    private List<ShipFacade> enemyShips = new List<ShipFacade>();

    private int playershipCount;
    private int allshipCount;

    public event Action<float> OnProgressChange;

    private bool isGameStarted = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        EventManager.OnStartGame += () =>
        {
            isGameStarted = true;
            SendProgress();
        };
    }

    public bool TryDuelShips(int planetID)
    {
        ShipFacade playerShip = playerShips.DefaultIfEmpty(null).FirstOrDefault(f => f.IsWithPlanet(planetID));
        ShipFacade enemyShip = enemyShips.DefaultIfEmpty(null).FirstOrDefault(f => f.IsWithPlanet(planetID));

        if (playerShip == null || enemyShip == null)
        {
            return false;
        }

        if (enemyShip.GetDamage(1))
        {
            enemyShips.Remove(enemyShip);
            enemyShip.gameObject.SetActive(false);
        }

        if (playerShip.GetDamage(1))
        {
            playerShip.gameObject.SetActive(false);
            playerShips.Remove(playerShip);
        }

        playershipCount -= 1;
        allshipCount -= 2;
        SendProgress();
        return true;
    }

    public void IncreaseShipCount(PlanetFacade planetFacade, int shipCount, ShipSide shipSide)
    {
        int count = 0;
        if (shipSide == ShipSide.Player)
        {
            count = playerShips.Where(w => w.IsWithPlanet(planetFacade.PlanetId)).Sum(s => 1);
        }
        else if (shipSide == ShipSide.Enemy)
        {
            count = enemyShips.Where(w => w.IsWithPlanet(planetFacade.PlanetId)).Sum(s => 1);
        }

        if (count < levelData.MaxShipsPerPlanet)
        {
            SpawnNewShips(planetFacade, shipCount, shipSide);
        }

        if (count >= levelData.MaxShipsPerPlanet)
        {
            IncreaseShipsWeight(planetFacade, shipCount, shipSide);
        }

        SendProgress();
    }

    private void IncreaseShipsWeight(PlanetFacade planetFacade, int shipCount, ShipSide shipSide)
    {
        IEnumerable enumerable;
        if (shipSide == ShipSide.Player)
        {
            enumerable = playerShips.Where(w => w.IsWithPlanet(planetFacade.PlanetId));
            playershipCount += shipCount;
        }
        else
        {
            enumerable = enemyShips.Where(w => w.IsWithPlanet(planetFacade.PlanetId));
        }

        allshipCount += shipCount;
        foreach (ShipFacade shipFacade in enumerable)
        {
            shipCount -= 1;
            shipFacade.AddWeight(planetFacade, shipSide, 1);

            if (shipCount <= 0)
            {
                return;
            }
        }
    }

    private void SpawnNewShips(PlanetFacade planetFacade, int shipCount, ShipSide shipSide)
    {
        for (int i = 0; i < shipCount; i++)
        {
            ShipFacade ship = shipPool.SpawnShip().GetComponent<ShipFacade>();

            Vector3 pos = UnityEngine.Random.insideUnitCircle.normalized;
            pos.z = pos.y;
            pos += planetFacade.transform.position;
            pos.y = ship.transform.position.y;
            ship.transform.position = pos;

            if (shipSide == ShipSide.Player)
            {
                playershipCount++;
                playerShips.Add(ship);
            }
            else
            {
                enemyShips.Add(ship);
            }

            allshipCount++;
            ship.Init(shipSide, planetFacade);
        }
    }

    private void SendProgress()
    {
        OnProgressChange?.Invoke((float)playershipCount / allshipCount);

        if (!isGameStarted)
        {
            return;
        }

        if (playershipCount <= 0)
        {
            UIController.Open(typeof(EndGameLoseScreen).Name);
            EventManager.EndGame(false);
        }
        else if(playershipCount == allshipCount)
        {
            UIController.Open(typeof(EndGameWinScreen).Name);
            EventManager.EndGame(true);
        }
    }

    public void SendPlayerShips(int planetID, PlanetFacade planetFacade, float percent)
    {
        IEnumerable enumerator = playerShips.Where(w => w.IsWithPlanet(planetID));
        int count = Mathf.FloorToInt(playerShips.Where(w => w.IsWithPlanet(planetID)).Sum(s => 1) * percent);
        int id = 0;

        foreach (ShipFacade ship in enumerator)
        {
            id++;
            if (id > count)
            {
                return;
            }

            ship.SendToPlanet(planetFacade); 
        }
    }

    public void SendEnemyShips(int planetID, PlanetFacade planetFacade, float percent)
    {
        List<ShipFacade> enumerator = enemyShips.Where(w => w.IsWithPlanet(planetID)).ToList();
        int count = Mathf.FloorToInt(enemyShips.Where(w => w.IsWithPlanet(planetID)).Sum(s => 1) * percent);
        int id = 0;

        foreach (ShipFacade ship in enumerator)
        {
            id++;
            if (id > count)
            {
                return;
            }

            ship.SendToPlanet(planetFacade);
        }
    }

}

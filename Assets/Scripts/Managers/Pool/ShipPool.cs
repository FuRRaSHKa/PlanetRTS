using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShipPool : MonoBehaviour
{
    [SerializeField] private GameObject shipPrefab;

    [SerializeField] private int startCount = 10;

    private List<GameObject> ships;

    private void Awake()
    {
        ships = new List<GameObject>(startCount);
        FillList();
    }

    public GameObject SpawnShip()
    {
        GameObject ship = ships.DefaultIfEmpty(null).FirstOrDefault(f => !f.activeSelf);

        if (ship == null)
        {
            ship = AddShip();
        }
        
        ship.SetActive(true);
        return ship;
    }

    public GameObject AddShip()
    {
        GameObject ship = Instantiate(shipPrefab, transform);
        ship.SetActive(false);
        ships.Add(ship);
        return ship;
    }

    private void FillList()
    {
        for (int i = 0; i < ships.Capacity; i++)
        {
            AddShip().SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShipPool : MonoBehaviour
{
    [SerializeField] private GameObject shipPrefab;

    [SerializeField] private int startCount = 300;

    private List<GameObject> ships;

    private void Start()
    {
        ships = new List<GameObject>(startCount);
        FillList();
    }

    public GameObject SpawnShip()
    {
        GameObject ship = ships.DefaultIfEmpty(null).First(f => f.activeSelf);

        if (ship == null)
        {
            ship = AddShip();
        }

        return ship;
    }

    public GameObject AddShip()
    {
        GameObject ship = Instantiate(gameObject, transform);
        ships.Add(ship);
        return ship;
    }

    private void FillList()
    {
        for (int i = 0; i < ships.Capacity; i++)
        {
            AddShip();
        }
    }
}

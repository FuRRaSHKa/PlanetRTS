using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private PlanetInput planetInput;

    [SerializeField] private LevelData levelData;
    [SerializeField] private PlanetFacade[] planetPrefabs;

    [Header("Borders")]
    [SerializeField] private Transform center;
    [SerializeField] private Vector3 size;

    private List<PlanetFacade> planets;

    private Bounds screenBounds;

    private void OnDrawGizmosSelected()
    {
        if (center != null)
        {
            Gizmos.DrawWireCube(center.transform.position, size);
        }
    }

    private void Start()
    {
        InitBounds();
        SpawnPlanets();
        FillPlanetWithShips();

        planetInput.SetPlanets(planets);
    }

    private void InitBounds()
    {
        screenBounds = new Bounds(center.transform.position, size);
    }

    private void SpawnPlanets()
    {
        int count = levelData.PlanetCount;
        planets = new List<PlanetFacade>(count);

        for (int i = 0; i < count; i++)
        {
            PlanetFacade planet = Instantiate(planetPrefabs[Random.Range(0, planetPrefabs.Length)], transform);

            if (PlacePlanet(planet))
            {
                planets.Add(planet);
            }
            else
            {
                Destroy(planet.gameObject);
            }

            if (i == 0)
            {
                planet.Init(i, ShipSide.Player);
            }
            else if (i == 1)
            {
                planet.Init(i, ShipSide.Enemy);
            }
            else
            {
                planet.Init(i);
            }
        }
    }

    private bool PlacePlanet(PlanetFacade planetFacade, int tryCount = 5)
    {
        Vector3 pos = Vector3.zero;
        float planetRadius = planetFacade.PlanetRadius;

        for (int i = 0; i < tryCount; i++)
        {
            pos.x = Random.Range(screenBounds.min.x, screenBounds.max.x);
            pos.z = Random.Range(screenBounds.min.z, screenBounds.max.z);

            if (CheckPlanetCollusion(pos, planetRadius))
            {
                planetFacade.transform.localPosition = pos;
                return true;
            }
        }

        return false;
    }

    private bool CheckPlanetCollusion(Vector3 pos, float radius)
    {
        for (int i = 0; i < planets.Count; i++)
        {
            if (radius + planets[i].PlanetRadius > (pos - planets[i].transform.localPosition).magnitude)
            {
                return false;
            }
        }

        return true;
    }

    private void FillPlanetWithShips()
    {
        ShipHandler.Instance.IncreaseShipCount(planets[0], levelData.PlayerShipsCount, ShipSide.Player);
        ShipHandler.Instance.IncreaseShipCount(planets[1], levelData.EnemyShipsCount, ShipSide.Enemy);
    }
}

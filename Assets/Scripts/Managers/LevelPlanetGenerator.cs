using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlanetGenerator : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private SpriteRenderer[] planetPrefabs;

    private List<SpriteRenderer> planets;

    private Bounds screenBounds;

    private void Awake()
    {
        InitBounds();
        SpawnPlanets();
    }

    private void InitBounds()
    {
        screenBounds = new Bounds(transform.position, new Vector3(10, 10, 1));
    }

    private void SpawnPlanets()
    {
        int count = levelData.PlayerPlanetCount + levelData.PlayerPlanetCount + levelData.EnemyPlanetCount;
        planets = new List<SpriteRenderer>(count);

        for (int i = 0; i < count; i++)
        {
            SpriteRenderer planet = Instantiate(planetPrefabs[Random.Range(0, planetPrefabs.Length)], transform);

            if (PlacePlanet(planet))
            {
                planets.Add(planet);
            }
            else
            {
                Destroy(planet);
            }
        }
    }

    private bool PlacePlanet(SpriteRenderer planetSpriteRenderer, int tryCount = 5)
    {
        Vector3 pos = Vector3.zero;
        float planetRadius = planetSpriteRenderer.bounds.size.x;

        for (int i = 0; i < tryCount; i++)
        {
            pos.x = Random.Range(screenBounds.min.x, screenBounds.max.x);
            pos.y = Random.Range(screenBounds.min.y, screenBounds.max.y);
            pos.z = 0;

            if (CheckPlanetCollusion(pos, planetRadius))
            {
                planetSpriteRenderer.transform.position = pos;
                return true;
            }    
        }

        return false;
    }

    private bool CheckPlanetCollusion(Vector2 pos, float radius)
    {
        for (int i = 0; i < planets.Count; i++)
        {
            if (radius + planets[i].bounds.size.x > (pos - (Vector2)planets[i].transform.position).magnitude)
            {
                return false;
            }
        }

        return true;
    }
}

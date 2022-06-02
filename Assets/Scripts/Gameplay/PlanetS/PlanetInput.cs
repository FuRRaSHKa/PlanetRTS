using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInput : MonoBehaviour
{
    private List<PlanetFacade> planetFacades;

    private int chosenPlanetId = -1;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            CheckTouch(cam.ScreenToWorldPoint(pos));
        }
    }

    private void CheckTouch(Vector2 pos)
    {
        for (int i = 0; i < planetFacades.Count; i++)
        {
            if (!planetFacades[i].CheckPosCollusion(pos))
            {
                ResetChoose();
                continue;
            }

            if (chosenPlanetId == -1)
            {
                chosenPlanetId = i;
                planetFacades[chosenPlanetId].ChoosePlanet(true);
                return;
            }

            ShipHandler.Instance.SendPlayerShips(planetFacades[chosenPlanetId].PlanetId, planetFacades[i]);
            ResetChoose();
            return;
        }
    }

    private void ResetChoose()
    {
        if (chosenPlanetId == -1)
        {
            return;
        }
        planetFacades[chosenPlanetId].ChoosePlanet(false);
        chosenPlanetId = -1;
    }

    public void SetPlanets(List<PlanetFacade> planetFacades)
    {
        this.planetFacades = planetFacades;
    }
}

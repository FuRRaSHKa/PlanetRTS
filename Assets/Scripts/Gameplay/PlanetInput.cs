using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInput : MonoBehaviour
{
    private PlanetShipHandler[] planetShipHandlers;

    private int chosenPlanetId = -1;

    private void Update()
    {

    }

    private void CheckTouch(Vector2 pos)
    {
        for (int i = 0; i < planetShipHandlers.Length; i++)
        {
            if (!planetShipHandlers[i].CheckPosCollusion(pos))
            {
                continue;
            }

            if (chosenPlanetId == -1)
            {
                chosenPlanetId = i;
                return;
            }

            planetShipHandlers[i].SendShips(planetShipHandlers[i]);
            chosenPlanetId = 0;
            return;
        }
    }
}

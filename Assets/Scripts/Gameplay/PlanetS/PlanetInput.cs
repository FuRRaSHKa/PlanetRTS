using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlanetInput : MonoBehaviour
{
    [SerializeField] private PlayerCursors playerCursor;

    private List<PlanetFacade> planetFacades;

    private int chosenPlanetId = -1;

    private Camera cam;

    private Vector3 camPos;

    private void Start()
    {
        cam = Camera.main;
    }

    #region ScreenInput

    public void MousePos(InputAction.CallbackContext context)
    {
        camPos = context.ReadValue<Vector2>();
    }

    public void ScreenInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector3 pos = camPos;
            pos.z = 10;
            CheckTouch(cam.ScreenToWorldPoint(pos));
        }
    }

    private void CheckTouch(Vector3 pos)
    {
        for (int i = 0; i < planetFacades.Count; i++)
        {
            if (!planetFacades[i].CheckPosCollusion(pos))
            {
                continue;
            }

            if (chosenPlanetId == -1)
            {
                chosenPlanetId = i;
                playerCursor.SetActiveFirstCursor(true);
                playerCursor.PlaceFirstCursor(planetFacades[chosenPlanetId].transform.position);
                return;
            }

            ShipHandler.Instance.SendPlayerShips(planetFacades[chosenPlanetId].PlanetId, planetFacades[i]);
            ResetChoose();
            return;
        }

        ResetChoose();
    }

    #endregion ScreenInput

    public void PlanetNavigateInput(InputAction.CallbackContext context)
    {
        
    }

    private void ResetChoose()
    {
        if (chosenPlanetId == -1)
        {
            return;
        }

        playerCursor.SetActiveFirstCursor(false);
        playerCursor.SetActiveSecondCursor(false);
        planetFacades[chosenPlanetId].ChoosePlanet(false);
        chosenPlanetId = -1;
    }

    public void SetPlanets(List<PlanetFacade> planetFacades)
    {
        this.planetFacades = planetFacades;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlanetInput : MonoBehaviour
{
    [SerializeField] private PlayerCursors playerCursor;
    [SerializeField] private float sensivity;

    private List<PlanetFacade> planetFacades;

    private int chosenPlanetId = -1;

    private Camera cam;

    private Vector2 cursorPos;
    private Vector2 firstCursorPos;

    private CustomPlayerInput inputActions;

    private void Awake()
    {
        inputActions = new CustomPlayerInput();
        inputActions.Enable();

        inputActions.UI.Click.performed += ScreenInput;
        inputActions.UI.Submit.performed += Submit;
    }

    private void Update()
    {
        MoveCursor(inputActions.UI.Navigate.ReadValue<Vector2>() * sensivity);
    }

    private void Start()
    {
        cam = Camera.main;
    }

    #region ScreenInput

    private void ScreenInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector3 pos = inputActions.UI.Point.ReadValue<Vector2>();
            pos.z = 10;
            cursorPos = pos;
            CheckTouch(cam.ScreenToWorldPoint(pos));
        }
    }

    private void CheckTouch(Vector3 pos, bool resetIfMiss = true, bool resetFirstCursor = true)
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
                playerCursor.PlaceFirstCursor(planetFacades[chosenPlanetId].transform.position);
                playerCursor.SetActiveFirstCursor(true);
                return;
            }

            ShipHandler.Instance.SendPlayerShips(planetFacades[chosenPlanetId].PlanetId, planetFacades[i]);
            ResetChoose(resetFirstCursor);
            return;
        }

        if (resetIfMiss)
        {
            ResetChoose(resetFirstCursor);
        }
    }

    #endregion ScreenInput

    private void MoveCursor(Vector2 dir)
    {
        if (dir == Vector2.zero)
        {
            return;
        }

        cursorPos += dir;
        Vector3 pos = cursorPos;
        pos.z = 5;
        pos = cam.ScreenToWorldPoint(pos);

        if (chosenPlanetId == -1)
        {
            firstCursorPos = cursorPos;
            playerCursor.PlaceFirstCursor(pos);
            playerCursor.SetActiveFirstCursor(true);
        }
        else
        {
            playerCursor.PlaceSecondCursor(pos);
            playerCursor.SetActiveSecondCursor(true);
        }

        SnapCursor(pos);
    }

    private void SnapCursor(Vector3 pos)
    {
        for (int i = 0; i < planetFacades.Count; i++)
        {
            if (!planetFacades[i].CheckPosCollusion(pos))
            {
                continue;
            }

            if (chosenPlanetId == -1)
            {
                playerCursor.PlaceFirstCursor(planetFacades[i].transform.position);
            }

            playerCursor.PlaceSecondCursor(planetFacades[i].transform.position);
            return;
        }
    }

    private void Submit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector3 pos = cursorPos;
            pos.z = 5;
            pos = cam.ScreenToWorldPoint(pos);
            CheckTouch(pos, false, false);
        }
    }

    private void ResetChoose(bool resetFirstCursor = true)
    {
        if (chosenPlanetId == -1)
        {
            return;
        }

        if (resetFirstCursor)
        {
            playerCursor.SetActiveFirstCursor(false);
        }

        cursorPos = firstCursorPos;
        playerCursor.SetActiveSecondCursor(false);
        chosenPlanetId = -1;
    }

    public void SetPlanets(List<PlanetFacade> planetFacades)
    {
        this.planetFacades = planetFacades;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursors : MonoBehaviour
{
    [SerializeField] private GameObject firstCursor;
    [SerializeField] private GameObject secondCursor;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float cursorSpeed;
    [SerializeField] private float cursorRadius;

    private Vector3 firstCursorPos;
    private Vector3 secondCursorPos;

    private void Start()
    {
        lineRenderer.enabled = false;
        firstCursor.SetActive(false);
        secondCursor.SetActive(false);
    }

    public void PlaceFirstCursor(Vector3 pos)
    {
        firstCursorPos = pos;
        firstCursorPos.y = 5;

        if (!firstCursor.activeSelf)
        {
            firstCursor.transform.position = firstCursorPos; 
        }
    }

    public void PlaceSecondCursor(Vector3 pos)
    {
        secondCursorPos = pos;
        secondCursorPos.y = 5;

        if (!secondCursor.activeSelf)
        {
            secondCursor.transform.position = secondCursorPos;
        }
    }

    public void SetActiveFirstCursor(bool value)
    {
        firstCursor.SetActive(value);
    }

    public void SetActiveSecondCursor(bool value)
    {
        lineRenderer.enabled = value;
        secondCursor.SetActive(value);
    }

    public void Disable()
    {
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        firstCursor.transform.position = Vector3.Lerp(firstCursor.transform.position, firstCursorPos, cursorSpeed * Time.deltaTime);
        secondCursor.transform.position = Vector3.Lerp(secondCursor.transform.position, secondCursorPos, cursorSpeed * Time.deltaTime);

        Vector3 dir = (firstCursor.transform.position - secondCursor.transform.position);
        dir.y = 0;

        if (dir.magnitude < cursorRadius * 2)
        {
            lineRenderer.SetPosition(1, firstCursorPos);
            lineRenderer.SetPosition(0, firstCursorPos);
            return;
        }

        dir.Normalize();
        lineRenderer.SetPosition(0, firstCursor.transform.position - dir * cursorRadius);
        lineRenderer.SetPosition(1, secondCursor.transform.position + dir * cursorRadius);
    }
}

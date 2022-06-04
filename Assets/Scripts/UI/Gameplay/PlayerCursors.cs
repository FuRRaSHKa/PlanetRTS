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

    private Vector3 targetPosition;
    private Vector3 currentPosition;

    private bool enableToMove = false;

    private float moveDuration;
    private float curTime;

    private Action onMoveEnd;

    private void Start()
    {
        lineRenderer.enabled = false;
        firstCursor.SetActive(false);
        secondCursor.SetActive(false);
    }

    public void PlaceFirstCursor(Vector3 pos)
    {
        firstCursor.transform.position = pos;
    }

    public void PlaceSecondCursor(Vector3 pos)
    {
        secondCursor.transform.position = pos;
    }

    public void SetActiveFirstCursor(bool value)
    {
        firstCursor.SetActive(value);
    }

    public void SetActiveSecondCursor(bool value)
    {
        secondCursor.SetActive(value);
    }

    public void MoveLineTo(Vector3 targetPosition, Action callback = null)
    {
        moveDuration = (targetPosition - currentPosition).magnitude / cursorSpeed;
        curTime = 0;
        onMoveEnd = callback;
        this.targetPosition = targetPosition;
        enableToMove = true;

        lineRenderer.SetPosition(1, currentPosition);
        lineRenderer.enabled = true;
    }

    public void SetStartPosition(Vector3 startPosition)
    {
        lineRenderer.SetPosition(0, startPosition);
        currentPosition = startPosition;
    }

    public void Disable()
    {
        enableToMove = false;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (!enableToMove)
        {
            return;
        }

        curTime += Time.deltaTime;
        lineRenderer.SetPosition(1, Vector3.Lerp(currentPosition, targetPosition, curTime / moveDuration));

        if (curTime > moveDuration)
        {
            onMoveEnd?.Invoke();
            enableToMove = false;
        }
    }
}

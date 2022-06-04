using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
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
    }

    public void MoveCursosrTo(Vector3 targetPosition, Action callback = null)
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

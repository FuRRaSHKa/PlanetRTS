using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipColoring : MonoBehaviour
{
    [SerializeField] private ShipColorData shipColor;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TrailRenderer trailRenderer;

    public void ColorShip(ShipSide shipSide)
    {
        Color color = shipColor.GetColor(shipSide);
        spriteRenderer.color = color;

        trailRenderer.endColor = color;
        trailRenderer.startColor = color;

        trailRenderer.enabled = true;
    }

    private void OnDisable()
    {
        trailRenderer.Clear();
        trailRenderer.enabled = false;
    }
}

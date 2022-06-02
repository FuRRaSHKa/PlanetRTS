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

        Material material = trailRenderer.material;
        material.color = color;
        trailRenderer.material = material;
    }
}

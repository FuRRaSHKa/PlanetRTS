using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "Data/ShipData", order = 0)]
public class ShipData : ScriptableObject
{
    [SerializeField] private float shipSpeed;
    [SerializeField] private float shipScaleMultiplier;

    public float ShipSpeed => shipSpeed;
    public float ShipScaleMultiplier => shipScaleMultiplier;
}

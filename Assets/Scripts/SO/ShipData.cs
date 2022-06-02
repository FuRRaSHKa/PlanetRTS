using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "Data/ShipData", order = 0)]
public class ShipData : ScriptableObject
{
    [SerializeField] private float shipSpeed;

    public float ShipSpeed => shipSpeed;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : ScriptableObject
{
    [SerializeField] private float shipSpeed;

    public float ShipSpeed => shipSpeed;
}

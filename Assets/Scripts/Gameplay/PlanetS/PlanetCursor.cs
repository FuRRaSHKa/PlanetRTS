using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCursor : MonoBehaviour
{
    [SerializeField] private GameObject cursor;

    private void Start()
    {
        cursor.SetActive(false);
    }

    public void ShowCursor()
    {
        cursor.SetActive(true);
    }

    public void HideCursor()
    {
        cursor.SetActive(false);
    }
}

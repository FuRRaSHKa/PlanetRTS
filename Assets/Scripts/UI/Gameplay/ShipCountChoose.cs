using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipCountChoose : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI textMesh;

    private float value;

    public float Value => value;

    private void Start()
    {
        OnSliderValueChage();
    }

    public void OnSliderValueChage()
    {
        value = slider.value;

        textMesh.text = (Mathf.Floor(value * 10) * 10).ToString() + "%";
    }
}

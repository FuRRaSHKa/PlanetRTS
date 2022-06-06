using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ShipCountChoose : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI textMesh;

    ShipCountInputData inputActions;

    private float value = .5f;

    public float Value => Mathf.Floor(value * 10) / 10;

    private void Awake()
    {
        OnSliderValueChage();
        inputActions = new ShipCountInputData();
        inputActions.Enable();
    }

    public void OnSliderValueChage()
    {
        value = slider.value;
        textMesh.text = (Mathf.Floor(value * 10) * 10).ToString() + "%";
    }

    private void Update()
    {       
        float value = inputActions.UI.ChangeShipCount.ReadValue<float>() * Time.deltaTime;
        if (value == 0)
        {
            return;
        }

        value += slider.value;
        slider.value = value;
    }

}

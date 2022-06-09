using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlanetShipUi : MonoBehaviour
{
    [SerializeField] private ShipColorData shipColorData;

    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI enemyText;
    [SerializeField] private TextMeshProUGUI playerText;

    [SerializeField] private Image enemyProgressImage;
    [SerializeField] private Image playerProgressImage;

    public void Awake()
    {
        PlanetFacade planetFacade = GetComponent<PlanetFacade>();

        planetFacade.OnShipValueUpdate += UpdateUI;
        planetFacade.OnPlanetCapture += PlanetCapture;

        enemyText.color = shipColorData.GetColor(ShipSide.Enemy);
        playerText.color = shipColorData.GetColor(ShipSide.Player);
        enemyProgressImage.color = shipColorData.GetColor(ShipSide.Enemy);
        playerProgressImage.color = shipColorData.GetColor(ShipSide.Player);
        DisableCapturedUI();
        DisableContestUI();

        EventManager.OnEndGame += (value) =>
        {
            UseContestUI(0, 0);
            DisableCapturedUI();
            DisableContestUI();
        };
    }

    public void PlanetCapture(ShipSide shipSide, float value)
    {
        if (shipSide == ShipSide.Player)
        {
            playerProgressImage.enabled = true;
            playerProgressImage.fillAmount = value;
        }
        else
        {
            enemyProgressImage.enabled = true;
            enemyProgressImage.fillAmount = value;
        }
    }

    private void UpdateUI(int playerShipCount, int enemyShipCount)
    {
        if (playerShipCount == 0 && enemyShipCount == 0)
        {
            DisableContestUI();
            DisableCapturedUI();
            return;
        }

        if (playerShipCount != 0 && enemyShipCount != 0)
        {
            DisableCapturedUI();
            UseContestUI(playerShipCount, enemyShipCount);
            return;
        }

        DisableContestUI();
        UseCapturedUI(playerShipCount, enemyShipCount);
    }

    private void UseContestUI(int playerShipCount, int enemyShipCount)
    {
        enemyText.enabled = true;
        playerText.enabled = true;

        enemyText.text = enemyShipCount.ToString();
        playerText.text = playerShipCount.ToString();

        float allships = enemyShipCount + playerShipCount;
        enemyProgressImage.fillAmount = enemyShipCount / allships;
        playerProgressImage.fillAmount = playerShipCount / allships;
    }

    private void DisableContestUI()
    {
        enemyText.enabled = false;
        playerText.enabled = false;
    }

    private void UseCapturedUI(int playerShipCount, int enemyShipCount)
    {
        mainText.enabled = true;
        mainText.text = Mathf.Max(enemyShipCount, playerShipCount).ToString();
        mainText.color = shipColorData.GetColor(enemyShipCount > playerShipCount ? ShipSide.Enemy : ShipSide.Player);
    }

    private void DisableCapturedUI()
    {
        mainText.enabled = false;
    }

}

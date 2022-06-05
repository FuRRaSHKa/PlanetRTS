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

    private int enemyShipCount = 0;
    private int playerShipCount = 0;

    public void Awake()
    {
        PlanetFacade planetFacade = GetComponent<PlanetFacade>();

        planetFacade.OnShipComing += AddShip;
        planetFacade.OnShipLeaving += RemoveShip;
        planetFacade.OnPlanetCapture += PlanetCapture;

        enemyText.color = shipColorData.GetColor(ShipSide.Enemy);
        playerText.color = shipColorData.GetColor(ShipSide.Player);
        DisableCapturedUI();
        DisableContestUI();
    }

    public void RemoveShip(ShipSide shipSide, int count)
    {
        if (shipSide == ShipSide.Player)
        {
            playerShipCount -= count;
            playerShipCount = playerShipCount < 0 ? 0 : playerShipCount;
        }
        else
        {
            enemyShipCount -= count;
            enemyShipCount = enemyShipCount < 0 ? 0 : enemyShipCount;
        }

        UpdateUI();
    }

    public void AddShip(ShipSide shipSide, int count)
    {
        if (shipSide == ShipSide.Player)
        {
            playerShipCount += count;
        }
        else
        {
            enemyShipCount += count;
        }

        UpdateUI();
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

    private void UpdateUI()
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
            UseContestUI();
            return;
        }

        DisableContestUI();
        UseCapturedUI();
    }

    private void UseContestUI()
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

    private void UseCapturedUI()
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

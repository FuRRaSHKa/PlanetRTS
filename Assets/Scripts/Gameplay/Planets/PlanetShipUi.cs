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
        PlanetFacade planetFacafe = GetComponent<PlanetFacade>();

        planetFacafe.OnShipComing += AddShip;
        planetFacafe.OnShipLeaving += RemoveShip;

        enemyText.color = shipColorData.GetColor(ShipSide.Enemy);
        playerText.color = shipColorData.GetColor(ShipSide.Player);
        DisableUI();
    }

    public void RemoveShip(ShipSide shipSide)
    {
        if (shipSide == ShipSide.Player)
        {
            playerShipCount--;
            playerShipCount = playerShipCount < 0 ? 0 : playerShipCount;
        }
        else
        {
            enemyShipCount--;
            enemyShipCount = enemyShipCount < 0 ? 0 : enemyShipCount;
        }

        UpdateUI();
    }

    public void AddShip(ShipSide shipSide)
    {
        if (shipSide == ShipSide.Player)
        {
            playerShipCount++;
        }
        else
        {
            enemyShipCount++;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        DisableUI();
        if (playerShipCount == 0 && enemyShipCount == 0)
        {
            return;
        }

        if (playerShipCount != 0 && enemyShipCount != 0)
        {
            UseContestUI();
            return;
        }   

        UseCaptureUI();
    }

    private void UseContestUI()
    {
        enemyText.enabled = true;
        playerText.enabled = true;
        enemyProgressImage.enabled = true;
        playerProgressImage.enabled = true;

        enemyText.text = enemyShipCount.ToString();
        playerText.text = playerShipCount.ToString();

        float allships = enemyShipCount + playerShipCount;
        enemyProgressImage.fillAmount = enemyShipCount / allships;
        playerProgressImage.fillAmount = playerShipCount / allships;
    }

    private void UseCaptureUI()
    {
        mainText.enabled = true;
        mainText.text = Mathf.Max(enemyShipCount, playerShipCount).ToString();
        mainText.color = shipColorData.GetColor(enemyShipCount > playerShipCount ? ShipSide.Enemy : ShipSide.Player);
    }

    private void DisableUI()
    {
        mainText.enabled = false;
        enemyText.enabled = false;
        playerText.enabled = false;
        enemyProgressImage.enabled = false;
        playerProgressImage.enabled = false;
    }

}

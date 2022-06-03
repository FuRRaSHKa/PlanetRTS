using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlanetShipUi : MonoBehaviour
{
    [SerializeField] private ShipColorData shipColorData;

    [SerializeField] private TextMeshPro mainText;
    [SerializeField] private TextMeshPro enemyText;
    [SerializeField] private TextMeshPro playerText;

    [SerializeField] private Image enemyProgressImage;
    [SerializeField] private Image playerProgressImage;

    private int enemyShipCount = 0;
    private int playerShipCount = 0;

    public void Awake()
    {
        PlanetFacade planetFacafe = GetComponent<PlanetFacade>();

        planetFacafe.OnShipComing += AddShip;
        planetFacafe.OnShipLeaving += RemoveShip;

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
    }

    private void UpdateUI()
    {
        if (playerShipCount != 0 && enemyShipCount != 0)
        {
            UseContestUI();
        }

        UseContestUI();
    }

    private void FixedUpdate()
    {
        UpdateUI();
    }

    private void UseContestUI()
    {
        enemyText.enabled = true;
        playerText.enabled = true;
        enemyProgressImage.enabled = true;
        playerProgressImage.enabled = true;

        enemyText.text = enemyShipCount.ToString();
        playerText.text = playerShipCount.ToString();

        int allships = enemyShipCount + playerShipCount;
        enemyProgressImage.fillAmount = enemyShipCount / allships;
        playerProgressImage.fillAmount = playerShipCount / allships;
    }

    private void UseCaptureUI()
    {
        mainText.enabled = false;
        mainText.text = Mathf.Max(enemyShipCount, playerShipCount).ToString();
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

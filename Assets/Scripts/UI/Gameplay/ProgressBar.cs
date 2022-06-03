using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image playerProgressImage;
    [SerializeField] private Image enemyImage;
    [SerializeField] private ShipColorData shipColorData;

    private void Start()
    {
        ShipHandler.Instance.OnProgressChange += UpdateProgress;

        playerProgressImage.color = shipColorData.GetColor(ShipSide.Player);
        enemyImage.color = shipColorData.GetColor(ShipSide.Enemy);
    }

    private void UpdateProgress(float progress)
    {
        playerProgressImage.fillAmount = progress;
    }
}

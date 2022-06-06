using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField] private Button levelButton;
    [SerializeField] private TextMeshProUGUI textStatus;

    public void SetStatus(LevelStatus levelStatus)
    {
        switch (levelStatus)
        {
            case LevelStatus.Closed:
                levelButton.interactable = false;
                textStatus.text = "Closed";
                break;
            case LevelStatus.Open:
                levelButton.interactable = true;
                textStatus.text = "";
                break;
            case LevelStatus.Completed:
                levelButton.interactable = true;
                textStatus.text = "Completed";
                break;
        }
    }
}

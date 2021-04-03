using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPopup : MonoBehaviour
{
    public TextMeshProUGUI universeText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI descriptionText;
    public GameObject child;

    public void CloseButton()
    {
        child.SetActive(false);
    }

    public void SetContent(string time, string description, string universe)
    {
        universeText.text = universe;
        timeText.text = time;
        descriptionText.text = description;
        child.SetActive(true);
    }
}

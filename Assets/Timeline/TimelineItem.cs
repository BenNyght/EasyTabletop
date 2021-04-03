using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimelineItem : MonoBehaviour
{
    public TextMeshProUGUI time;
    public TextMeshProUGUI desciption;
    public string universe;
    public Image line;
    public Image marker;

    public void CreatePopup()
    {
        GameObject.Find("InfoPopup").GetComponent<InfoPopup>().SetContent(time.text, desciption.text, universe);
    }
}

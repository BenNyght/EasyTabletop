using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timeline : MonoBehaviour
{
    public GameObject contentPanel;
    public GameObject listItemPrefab;
    public TextMeshProUGUI universeText;
    public TextAsset json;

    private List<TimeModel> timemodels = new List<TimeModel>();
    TimeListModel timeline;

    void Start()
    {
        timeline = JsonUtility.FromJson<TimeListModel>(json.ToString());
        timemodels = timeline.times;

        universeText.text = timeline.universe;
    }

    public void LoadContent()
    {
        foreach (TimeModel time in timemodels)
        {
            GameObject newMap = Instantiate(listItemPrefab) as GameObject;
            TimelineItem listItem = newMap.GetComponent<TimelineItem>();
            listItem.desciption.text = time.description;
            listItem.time.text = time.date + " " + time.typetype;
            listItem.universe = timeline.universe;
            Color color = hexToColor(timeline.colour);
            listItem.line.color = color;
            listItem.marker.color = color;
            newMap.transform.parent = contentPanel.transform;
            newMap.transform.localScale = Vector3.one;
        }
    }

    public static Color hexToColor(string hex)
    {
        hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }
}


[Serializable]
public class TimeListModel
{
    public string type = "";
    public string colour = "";
    public string universe = "";
    public string description = "";
    public string originalsource = "";
    public List<TimeModel> times = new List<TimeModel>();
}

[Serializable]
public class TimeModel
{
    public string date;
    public string typetype;
    public string description;
}
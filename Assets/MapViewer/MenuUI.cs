using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using FrostweepGames.Plugins.WebGLFileBrowser;
using UnityEngine.UI;
using System.Linq;

public class MenuUI : MonoBehaviour
{
    public GameObject contentPanel;
    public GameObject listItemPrefab;

    public List<Sprite> sprites = new List<Sprite>();

    void Start()
    {
        foreach (Sprite sprite in sprites)
        {
            GameObject newMap = Instantiate(listItemPrefab) as GameObject;
            ListItem listItem = newMap.GetComponent<ListItem>();
            listItem.sprite = sprite;
            listItem.buttonText.text = sprite.name;
            newMap.transform.parent = contentPanel.transform;
            newMap.transform.localScale = Vector3.one;
        }
    }
}

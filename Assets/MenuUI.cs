using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject contentPanel;
    public GameObject listItemPrefab;
    public List<string> maps = new List<string>();

    void Start()
    {
        foreach (string map in maps)
        {
            GameObject newMap = Instantiate(listItemPrefab) as GameObject;
            ListItem listItem = newMap.GetComponent<ListItem>();
            listItem.sceneName = map;
            listItem.buttonText.text = map;
            newMap.transform.parent = contentPanel.transform;
            newMap.transform.localScale = Vector3.one;
        }
    }

    public void Button_Quit()
    {
        Application.Quit();
    }
}

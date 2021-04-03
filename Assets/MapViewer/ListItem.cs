using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ListItem : MonoBehaviour
{
    public Sprite sprite;
    public TextMeshProUGUI buttonText;

    public void LoadScene_Button()
    {
        GameObject imageSelection = GameObject.Find("SelectionData");
        imageSelection.GetComponent<SelectionData>().map = sprite;

        LevelLoader loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        loader.LoadLevel("Mapviewer");
    }

    public void HoverOver()
    {
        GameObject.Find("MapPreview").GetComponent<MapPreview>().ShowMap(sprite);
    }
}
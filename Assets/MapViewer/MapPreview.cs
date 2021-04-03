using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPreview : MonoBehaviour
{
    public Image image;
    public AspectRatioFitter aspectRatio;
    public GameObject child;

    public void ShowMap(Sprite sprite)
    {
        if (child.activeSelf)
        {
            float ar = sprite.rect.width / sprite.rect.height;
            aspectRatio.aspectRatio = ar;
            image.sprite = sprite;
        }
    }

    public void TogglePreview(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }
}

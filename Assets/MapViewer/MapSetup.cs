using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSetup : MonoBehaviour
{
    private GameObject imageSelection;

    void Start()
    {
        imageSelection = GameObject.Find("SelectionData");

        if (imageSelection != null)
        {
            Sprite image = imageSelection.GetComponent<SelectionData>().map;
            gameObject.GetComponent<SpriteRenderer>().sprite = image;

            transform.localScale = new Vector3(1, 1, 1);

            float height = image.bounds.size.y;
            float width = image.bounds.size.x;

            float worldScreenHeight = Camera.main.orthographicSize * 2;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            transform.localScale = Vector3.one * (worldScreenHeight / height);
        }
    }
}

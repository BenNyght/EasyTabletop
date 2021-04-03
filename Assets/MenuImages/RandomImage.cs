using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomImage : MonoBehaviour
{
    public Sprite[] images;

    void Start()
    {
        SetRandomImage();
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SetRandomImage();
        }
    }

    public void SetRandomImage()
    {
        int randomImage = (int)Random.Range(0, images.Length);
        Sprite image = images[randomImage];
        float aspectRatio = image.rect.width / image.rect.height;

        gameObject.GetComponent<AspectRatioFitter>().aspectRatio = aspectRatio;
        gameObject.GetComponent<Image>().sprite = image;
    }
}

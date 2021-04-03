using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightBubbleController : MonoBehaviour
{
    public GameObject SightBubble;
    public GameObject LargeSightBubble;
    public List<GameObject> Bubbles;

    public void SpawnButton()
    {
        Instantiate(SightBubble);
    }

    public void SpawnLargeButton()
    {
        Instantiate(LargeSightBubble);
    }

    public void RemoveBubbles()
    {
        foreach (GameObject go in Bubbles)
        {
            Destroy(go);
        }
    }
}

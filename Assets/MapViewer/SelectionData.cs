using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionData : MonoBehaviour
{
    public Sprite map;
    private static SelectionData selfIntance;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (selfIntance == null)
        {
            selfIntance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

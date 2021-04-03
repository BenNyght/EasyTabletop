using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAll : MonoBehaviour
{
    public Toggle[] toggles;
    public Toggle selfToggle;

    public void toggleToggles(bool value)
    {
        foreach (Toggle i in toggles)
        {
            if (selfToggle.isOn == true)
            {
                i.isOn = false;
            }
            else if (selfToggle.isOn == false)
            {
                i.isOn = true;
            }
        }
    }
}

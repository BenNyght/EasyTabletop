using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ListItem : MonoBehaviour
{
    public string sceneName;
    public TextMeshProUGUI buttonText;

    public void LoadScene_Button()
    {
        SceneManager.LoadScene(sceneName);
    }
}

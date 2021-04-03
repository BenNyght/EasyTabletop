using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator tranistion;
    public bool backButton = true;
    public GameObject backButtonObject;

    private void Start()
    {
        backButtonObject.SetActive(backButton);
    }

    public void LoadLevel(string level)
    {
        StartCoroutine("LoadScene", level);
    }
    IEnumerator LoadScene(string level)
    {
        tranistion.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(level);
    }
}

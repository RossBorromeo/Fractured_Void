using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public float creditsDuration = 30f; // how long credits scroll
    public string mainMenuSceneName = "MainMenu"; 

    private void Start()
    {
        StartCoroutine(WaitAndReturnToMenu());
    }

    IEnumerator WaitAndReturnToMenu()
    {
        yield return new WaitForSeconds(creditsDuration);
        SceneManager.LoadScene(mainMenuSceneName);
    }
}

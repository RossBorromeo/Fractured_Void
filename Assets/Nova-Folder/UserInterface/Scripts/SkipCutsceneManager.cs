using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SkipCutsceneManager : MonoBehaviour
{
    public GameObject skipButton;  // skip button 
    public float idleAfterMove = 2f;   // time passed before hiding the button

    private Vector3 lastMousePosition;

    private float idleTimer;
    void Start()
    {
        skipButton.SetActive(false);// button hidden at the start
        lastMousePosition = Input.mousePosition;// starting mouse positon
        idleTimer = 0f;
    }

    void Update()
    {
        // checks for mouse movement
        if (Input.mousePosition != lastMousePosition)
        {
            ShowSkipButton();
            idleTimer = 0f;
        }
        else 
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleAfterMove)
            {
                HideSkipButton();
            }
        }

        lastMousePosition = Input.mousePosition;// updates last mouse position
    }
    public void SkipCutScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void ShowSkipButton()
    {
        if (!skipButton.activeSelf)
            skipButton.SetActive(true);
    }

    void HideSkipButton()
    {
        if (skipButton.activeSelf)
            skipButton.SetActive(false);
        Debug.Log("Hiding Skip Button");
    }

}

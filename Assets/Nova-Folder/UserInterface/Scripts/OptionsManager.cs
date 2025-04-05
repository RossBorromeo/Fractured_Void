using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{

    public GameObject HUDOptions;  // options button
    public GameObject ClickedOptionsOpen; // full options menu UI
    public GameObject OptionsExitButton; // exit button in the options menu

    public JournalManager journalManager; // Reference to the JournalManager
    public HouseManager houseManager; // Reference to the HouseManager

    private PlayerMovement playerMovement; // reference to player movement script

    public static OptionsManager Instance; // singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // finds player and gets the PlayerMovement script
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }

    }

    // Shortcut to options menu 
    private void Update()
    {
        // check if ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Open options menu when ESC is pressed
            OnOptionsClick();
        }
    }

    public void OnOptionsClick()
    {
       
        // prevent Options from opening if journal or house UI is open
        if ((journalManager != null && journalManager.IsJournalOpen()) ||
            (houseManager != null && houseManager.IsHouseOpen()))
        {
            return;
        }

        HUDOptions.SetActive(false); // Hide the HUD button
        ClickedOptionsOpen.SetActive(true); // Show the options menu
        OptionsExitButton.SetActive(true);
        Time.timeScale = 0; // Pause the game
        PausePlayerMovement(); // Pause movement when dialogue starts
    }

    public void CloseOptions()
    {
        
        if (ClickedOptionsOpen != null)
        {

            ClickedOptionsOpen.SetActive(false); // Hide options menu
            OptionsExitButton.SetActive(false);
        }

        if (HUDOptions != null)
        {   ResumePlayerMovement(); // resumes movement when dialogue ends
            Time.timeScale = 1; // Resume game
            HUDOptions.SetActive(true); // Show the HUD button again
        }
    }

    // Called by other scripts to check if the Options menu is open
    public bool IsOptionsOpen()
    {
        return ClickedOptionsOpen.activeSelf;
    }
    public void RestartGame()
    {
        // close options menu first (if open)
        CloseOptions();

        // Reset time scale in case it's paused .
        Time.timeScale = 1;

        // Reload the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void GoToMenu()
    {
        // close options menu first (if open)
        CloseOptions();

        // Reset time scale in case it's paused .
        Time.timeScale = 1;

        // Reload the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
    }


    private void PausePlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.SetMovementEnabled(false); // freezes player
        }
    }

    private void ResumePlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.SetMovementEnabled(true); // unfreezes player
        }
    }
}


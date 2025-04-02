using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{

    public GameObject HUDOptions;  // options button
    public GameObject ClickedOptionsOpen; // full options menu UI
    public GameObject OptionsExitButton; // exit button in the options menu

    public JournalManager journalManager; // Reference to the JournalManager
    public HouseManager houseManager; // Reference to the HouseManager

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
    }

    public void CloseOptions()
    {
        if (ClickedOptionsOpen != null)
        {

            ClickedOptionsOpen.SetActive(false); // Hide options menu
            OptionsExitButton.SetActive(false);
        }

        if (HUDOptions != null)
        {
            Time.timeScale = 1; // Resume game
            HUDOptions.SetActive(true); // Show the HUD button again
        }
    }

    // Called by other scripts to check if the Options menu is open
    public bool IsOptionsOpen()
    {
        return ClickedOptionsOpen.activeSelf;
    }
}


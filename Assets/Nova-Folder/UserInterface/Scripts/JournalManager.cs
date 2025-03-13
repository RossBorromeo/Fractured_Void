using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JournalManager : MonoBehaviour
{
    public GameObject HUDJournal;  // the small journal icon in the HUD
    public GameObject ClickedJournalOpen; // the clicked journal UI image
    public GameObject JournalExitButton;

    public AudioSource audioSource; // Adam
    public AudioClip journalClickSound; // Adam

    //public GameObject HouseUI;
    public HouseManager houseManager; // Reference to HouseManager
    public void OnJournalClick()
    {
        if (audioSource != null && journalClickSound != null)
        {
            audioSource.PlayOneShot(journalClickSound);
        }

        // Prevent journal from opening if the house is open
        if (houseManager != null && houseManager.IsHouseOpen())
        {
            return;
        }

        HUDJournal.SetActive(false);// hides the HUDJournal button
        
        ClickedJournalOpen.SetActive(true);// shows the ClickedJournalOpen
        JournalExitButton.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    public void CloseJournal()
    {
        if (ClickedJournalOpen != null)
        {

            if (audioSource != null && journalClickSound != null)
            {
                audioSource.PlayOneShot(journalClickSound);
            }

            ClickedJournalOpen.SetActive(false); // hides the ClickedJournalOpen
            JournalExitButton.SetActive(false);
        }

        if (HUDJournal != null)
        {

            Time.timeScale =1;
            HUDJournal.SetActive(true); // shows the HUDJournal again
        }
    }
    // called by the house to check if the Journal is open
    public bool IsJournalOpen()
    {
        return ClickedJournalOpen.activeSelf;
    }


}

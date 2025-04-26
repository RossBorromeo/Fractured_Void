using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUIManager : MonoBehaviour
{
    public static OpenUIManager Instance;

    /*House elements*/
    public GameObject HUDHouse;
    public GameObject ClickedHouse;
    public GameObject HouseExitButton;
   
    /* Journal elements */
    public GameObject HUDJournal;
    public GameObject ClickedJournalOpen;
    public GameObject JournalExitButton;

    /* Options elements */
    public GameObject HUDOptions;
    public GameObject ClickedOptions;
    public GameObject OptionsExitButton;

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
    }

    public void OpenHouseUI()
    {
        if (ClickedJournalOpen.activeSelf) return; // Prevent opening if Journal is open

        HUDHouse.SetActive(false);
        ClickedHouse.SetActive(true);
        HouseExitButton.SetActive(true);
    }

    public void CloseHouseUI()
    {
        ClickedHouse.SetActive(false);
        HouseExitButton.SetActive(false);
        HUDHouse.SetActive(true);
    }

    public void OpenJournalUI()
    {
        if (ClickedHouse.activeSelf) return; // Prevent opening if House is open

        HUDJournal.SetActive(false);
        ClickedJournalOpen.SetActive(true);
        JournalExitButton.SetActive(true);
    }

    public void CloseJournalUI()
    {
        ClickedJournalOpen.SetActive(false);
        JournalExitButton.SetActive(false);
        HUDJournal.SetActive(true);
    }
    public void OpenOptionsUI()
    {
        if (ClickedOptions.activeSelf) return; // Prevent opening if Options is open

        HUDOptions.SetActive(false);
        ClickedOptions.SetActive(true);
        OptionsExitButton.SetActive(true);

        HUDJournal.SetActive(false);
    }

    public void CloseOptionsUI()
    {
        ClickedOptions.SetActive(false);
        OptionsExitButton.SetActive(false);
        HUDOptions.SetActive(true);
        HUDJournal.SetActive(true);
    }
}

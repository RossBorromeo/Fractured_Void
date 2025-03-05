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

    public void OnJournalClick()
    {
        if (HUDJournal != null)
        {
            HUDJournal.SetActive(false); // hides the HUDJournal
        }

        if (ClickedJournalOpen != null)
        {
            ClickedJournalOpen.SetActive(true); // shows the ClickedJournalOpen
            JournalExitButton.SetActive(true);
           
        }
       
    }

    public void CloseJournal()
    {
        if (ClickedJournalOpen != null)
        {
            ClickedJournalOpen.SetActive(false); // hides the ClickedJournalOpen
            JournalExitButton.SetActive(false);
        }

        if (HUDJournal != null)
        {
            HUDJournal.SetActive(true); // shows the HUDJournal again
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class HouseManager : MonoBehaviour
{
    public GameObject HUDHouse;  // the small house icon in the HUD
    public GameObject ClickedHouse; // the full-sized house UI image
    public GameObject HouseExitButton; // button to close the full-sized house Ui image
    public GameObject MabelWindow; // window with Mabel
    public GameObject RoseWindow; //window with Rose
    public GameObject MabelInfoBox; // information panel on Mabel
    public GameObject RoseInfoBox; // information panel on Rose
    private bool roseWindowTrigger = false; // trigger boolean to check if player has reached Rose princess -> then they can trigger window

   

    public JournalManager journalManager; // Reference to JournalManager

    public void OnHouseClick()
    {
        // Prevent house from opening if the journal is open
        if (journalManager != null && journalManager.IsJournalOpen())
        {
            return;
        }

        HUDHouse.SetActive(false);// hides the HUDHouse button
        ClickedHouse.SetActive(true);// shows the ClickedHouse
        HouseExitButton.SetActive(true);
        MabelWindow.SetActive(true);// shows Mabel's window
        Time.timeScale = 0; // pauses the game

        if (RoseWindow != null) RoseWindow.SetActive(roseWindowTrigger);
        /*

        if (HUDHouse != null)
        {
            HUDHouse.SetActive(false); 
        }

        if (ClickedHouse != null)
        {
            Time.timeScale = 0;

            ClickedHouse.SetActive(true); 
            HouseExitButton.SetActive(true);
            MabelWindow.SetActive(true);// shows Mabel's window
        }
        
        */
    }

    public void CloseBigHouse()
    {
        if (ClickedHouse != null)
        {
            ClickedHouse.SetActive(false); // hides the ClickedHouse
            HouseExitButton.SetActive(false);
        }

        if (HUDHouse != null)
        {
            Time.timeScale = 1;
            HUDHouse.SetActive(true); // shows the HUD house again
        }
    }

    public void RoseWindowTrigger()
    {
        roseWindowTrigger = true; // marks that the player reached the rose trigger
    }
    // this will be called to check if the the House is open
    public bool IsHouseOpen()
    {
        return ClickedHouse.activeSelf;
    }


}

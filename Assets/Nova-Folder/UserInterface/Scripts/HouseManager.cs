using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager.UI;
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


    public OptionsManager optionsManager; // reference to OptionsManager
    public JournalManager journalManager; // Reference to JournalManager

    private PlayerMovement playerMovement; // reference to player movement script


    public AudioSource audioSource; //Adam
    public AudioClip clickSound;    //Adam


    public static HouseManager Instance; // singleton
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





    public void OnHouseClick()
    {

        // Prevent house from opening if the journal is open
        if (journalManager != null && journalManager.IsJournalOpen() ||
            (optionsManager != null && optionsManager.IsOptionsOpen())) 
        {
            return;
        }

        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        HUDHouse.SetActive(false);// hides the HUDHouse button
        ClickedHouse.SetActive(true);// shows the ClickedHouse
        HouseExitButton.SetActive(true);
        MabelWindow.SetActive(true);// shows Mabel's window
        Time.timeScale = 0; // pauses the game
        PausePlayerMovement(); // Pause movement when dialogue starts
        if (RoseWindow != null) RoseWindow.SetActive(roseWindowTrigger);

        
    }

    public void CloseBigHouse()
    {
        ResumePlayerMovement(); // resumes movement when dialogue ends
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

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

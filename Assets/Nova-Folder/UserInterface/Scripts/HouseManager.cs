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
    public void OnHouseClick()
    {
        if (HUDHouse != null)
        {
            HUDHouse.SetActive(false); // hides the HUDHouse
        }

        if (ClickedHouse != null)
        {
            ClickedHouse.SetActive(true); // shows the ClickedHouse
            HouseExitButton.SetActive(true);
            MabelWindow.SetActive(true);// shows Mabel's window
        }
        if (RoseWindow != null) RoseWindow.SetActive(roseWindowTrigger);
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
            HUDHouse.SetActive(true); // shows the HUD house again
        }
    }

    public void RoseWindowTrigger()
    {
        roseWindowTrigger = true; // marks that the player reached the rose trigger
    }


    

}

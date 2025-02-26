using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image[] hearts; // Array of heart images
    private int currentHearts;

    private void Start()
    {
        currentHearts = hearts.Length; // Set max hearts at start
        UpdateHeartsUI();
    }

    public void ReduceHeart()
    {
        if (currentHearts > 0)
        {
            currentHearts--; // Remove a heart
            UpdateHeartsUI();
        }
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < currentHearts); // Hide extra hearts
        }
    }

    public bool IsOutOfHearts()
    {
        return currentHearts <= 0;
    }

    public int GetHeartCount() 
    { 
        return currentHearts;// will return the number of hesrts 
    }

    public void SetHearts(int heartCount) 
    {
        currentHearts = heartCount;
        UpdateHeartsUI ();// will refresh the ui with new hearts status 

    }
}
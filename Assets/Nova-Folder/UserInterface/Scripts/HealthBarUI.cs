using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image[] hearts; // Array of heart images
    private int currentHearts;
    private int maxHearts;

    private void Start()
    {
        maxHearts = hearts.Length;

        // Load hearts from PlayerPrefs (if available)
        if (PlayerPrefs.HasKey("RemainingHearts"))
        {
            currentHearts = PlayerPrefs.GetInt("RemainingHearts");

            if (currentHearts <= 0)
            {
                string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

                if (currentScene == "Bedroom_Scene_Ross_1st")
                {
                    currentHearts = maxHearts; // Reset if we are in the bedroom
                    PlayerPrefs.SetInt("RemainingHearts", maxHearts);
                    PlayerPrefs.Save();
                }
            }
        }
        else
        {
            currentHearts = maxHearts; // Default full hearts
        }

        UpdateHeartsUI();
    }

    public void ReduceHeart()
    {
        if (currentHearts > 0)
        {
            currentHearts--; // Remove a heart
            UpdateHeartsUI();

            if (currentHearts > 0)
            {
               PlayerPrefs.SetInt("RemainingHearts", currentHearts); // Only save if still alive
                PlayerPrefs.Save();
            }
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
        return currentHearts; // Returns the number of hearts 
    }

    public void SetHearts(int heartCount)
    {
        currentHearts = heartCount;
        PlayerPrefs.SetInt("RemainingHearts", heartCount); // Save new heart count
        PlayerPrefs.Save();
        UpdateHeartsUI();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIJ : MonoBehaviour
{
    public Image[] hearts; // Array of heart images
    private int currentHearts;
    private int maxHearts;

    private void Start()
    {
        maxHearts = hearts.Length;
        currentHearts = maxHearts; // Always start with full hearts
        UpdateHeartsUI();
    }

    public void ReduceHeart()
    {
        if (currentHearts > 0)
        {
            currentHearts--;
            UpdateHeartsUI();
        }
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < currentHearts);
        }
    }

    public bool IsOutOfHearts()
    {
        return currentHearts <= 0;
    }

    public int GetHeartCount()
    {
        return currentHearts;
    }

    public void SetHearts(int heartCount)
    {
        currentHearts = Mathf.Clamp(heartCount, 0, maxHearts);
        UpdateHeartsUI();
    }
}

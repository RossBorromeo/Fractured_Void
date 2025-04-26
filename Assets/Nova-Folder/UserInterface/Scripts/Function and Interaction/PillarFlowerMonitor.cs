using UnityEngine;

public class PillarFlowerMonitor : MonoBehaviour
{
    public string flowerName; // Flower name 
    private bool flowerPlaced = false;

    private InteractPromptArea promptArea;
    private PlayerFlowerInteration_Audio_UI playerFlowerInteraction; // reference to the player interaction script

    void Start()
    {
        // tries to find the InteractPromptArea on this same object
        promptArea = GetComponent<InteractPromptArea>();
        if (promptArea == null)
        {
            promptArea = GetComponentInChildren<InteractPromptArea>();
        }

        if (promptArea == null)
        {
            Debug.LogWarning("No InteractPromptArea found for PillarFlowerMonitor on " + gameObject.name);
        }

        // find the PlayerFlowerInteration_Audio_UI script on the player (or another appropriate object)
        playerFlowerInteraction = Object.FindFirstObjectByType<PlayerFlowerInteration_Audio_UI>();
    }

    void Update()
    {
        // checks if the flower has been placed based on the player's placement info
        if (!flowerPlaced)
        {
            if (flowerName == "Tulip" && playerFlowerInteraction != null && playerFlowerInteraction.placedTulip)
            {
                flowerPlaced = true;
                DisablePrompt();
            }
            else if (flowerName == "Marigold" && playerFlowerInteraction != null && playerFlowerInteraction.placedMarigold)
            {
                flowerPlaced = true;
                DisablePrompt();
            }
            else if (flowerName == "Aster" && playerFlowerInteraction != null && playerFlowerInteraction.placedAster)
            {
                flowerPlaced = true;
                DisablePrompt();
            }
            else if (flowerName == "Poinsettia" && playerFlowerInteraction != null && playerFlowerInteraction.placedPoinsettia)
            {
                flowerPlaced = true;
                DisablePrompt();
            }
        }
    }

    void DisablePrompt()
    {
        if (promptArea != null && promptArea.canvas != null)
        {
            promptArea.canvas.gameObject.SetActive(false);
        }

        //Debug.Log("Prompt disabled for " + gameObject.name + " because " + flowerName + " was placed.");
    }
}
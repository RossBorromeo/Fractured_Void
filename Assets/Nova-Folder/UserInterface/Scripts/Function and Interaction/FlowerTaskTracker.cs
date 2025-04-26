using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerTaskTracker : MonoBehaviour
{
    public static FlowerTaskTracker Instance; // singleton 

    private int collectedFlowers = 0;
    private int placedFlowers = 0;
    private int totalFlowers = 4; // total flowers required
    private int totalPillars = 4; // total pillars for placement

    private bool flowersCollectedComplete = false;
    private bool flowersPlacedComplete = false;

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

    // call this when a flower is picked up
    public void CollectFlower()
    {

        if (!flowersCollectedComplete)
        {
            collectedFlowers++;

            if (collectedFlowers >= totalFlowers)
            {
                flowersCollectedComplete = true;
                TaskCompletionManagerRoseGarden.Instance.flowersCollected = true;//  syncing with TaskCompletionManager
                TaskCompletionManagerRoseGarden.Instance.UpdateTaskText(TaskCompletionManagerRoseGarden.Instance.findFlowersTaskText, "Collect All Seasonal Flowers");
            }
        }
    }

    // call this when a flower is placed correctly
    public void PlaceFlower()
    {
        if (!flowersPlacedComplete)
        {
            placedFlowers++;

            if (placedFlowers >= totalPillars)
            {
                flowersPlacedComplete = true;
                TaskCompletionManagerRoseGarden.Instance.flowersPlaced = true;
                TaskCompletionManagerRoseGarden.Instance.UpdateTaskText(TaskCompletionManagerRoseGarden.Instance.placeFlowersTaskText, "Put Flowers in the Right Place");
            }
        }
    }
}
using System.Collections;
using UnityEngine;

public class PlayerFlowerInteraction : MonoBehaviour
{
    private bool hasFlower1 = false;
    private bool hasFlower2 = false;
    private bool placedFlower1 = false;
    private bool placedFlower2 = false;

    public Transform platform; // Assign the rising platform
    public float riseHeight = 5f; // How high the platform rises
    public float riseSpeed = 2f; // Speed of rising

    public float interactionRadius = 2f; // Range for detection

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickupFlower();
            TryPlaceFlower();
        }
    }

    void TryPickupFlower()
    {
        GameObject[] flowers = GameObject.FindGameObjectsWithTag("Flower");
        foreach (GameObject flower in flowers)
        {
            if (Vector3.Distance(transform.position, flower.transform.position) <= interactionRadius)
            {
                if (flower.name == "Flower1" && !hasFlower1)
                {
                    hasFlower1 = true;
                    Debug.Log("Collected Flower 1");
                    Destroy(flower);
                    return;
                }
                else if (flower.name == "Flower2" && !hasFlower2)
                {
                    hasFlower2 = true;
                    Debug.Log("Collected Flower 2");
                    Destroy(flower);
                    return;
                }
            }
        }
    }

    void TryPlaceFlower()
    {
        GameObject[] pillars = GameObject.FindGameObjectsWithTag("PillarTriggerZone"); // Detect trigger zones
        foreach (GameObject pillarZone in pillars)
        {
            if (Vector3.Distance(transform.position, pillarZone.transform.position) <= interactionRadius)
            {
                if (pillarZone.name == "Pillar1TriggerZone" && hasFlower1 && !placedFlower1)
                {
                    placedFlower1 = true;
                    hasFlower1 = false;
                    Debug.Log("Placed Flower 1 on Pillar 1");
                }
                else if (pillarZone.name == "Pillar2TriggerZone" && hasFlower2 && !placedFlower2)
                {
                    placedFlower2 = true;
                    hasFlower2 = false;
                    Debug.Log("Placed Flower 2 on Pillar 2");
                }

                if (placedFlower1 && placedFlower2)
                {
                    Debug.Log("Both flowers placed! Raising platform...");
                    StartCoroutine(RaisePlatform());
                }
                return; // Stop checking once valid placement is found
            }
        }
    }

    IEnumerator RaisePlatform()
    {
        Vector3 targetPosition = platform.position + new Vector3(0, riseHeight, 0);

        while (platform.position.y < targetPosition.y)
        {
            platform.position = Vector3.MoveTowards(platform.position, targetPosition, riseSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("Platform has risen!");
    }
}

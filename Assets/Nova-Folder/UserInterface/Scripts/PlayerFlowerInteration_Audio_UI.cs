using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerFlowerInteration_Audio_UI : MonoBehaviour
{
    private bool hasTulip = false;
    private bool hasMarigold = false;
    private bool hasAster = false;
    private bool hasPoinsettia = false;

    public bool placedTulip = false;
    public bool placedMarigold = false;
    public bool placedAster = false;
    public bool placedPoinsettia = false;

    public float interactionRadius = 4f; // Range for detection

    public AudioClip pickupSound; // Assign the pickup sound in the inspector
    private AudioSource audioSource;

    public GameObject SpringLight;
    public GameObject SummerLight;
    public GameObject AutumnLight;
    public GameObject WinterLight;

    public Animator gateAnimator; // Assign the gate animator in the inspector
    public GameObject MainGateBarrier; // Assign the barrier object to disable after opening the gate

    public GameObject Puzzle1PortalBack; // Activate when Aster is picked up
    public GameObject Vines; // De-Activate when Aster is picked up
    public GameObject BlockingGates; // De-Activate when Tulip is picked up


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
                if (flower.name == "Tulip" && !hasTulip)
                {
                    hasTulip = true;
                    Debug.Log("Collected Tulip");

                    //disables prompt before destroying the flower 
                    DisableNearbyPrompt(flower.transform.position);

                    Destroy(flower);
                    PlaySound(pickupSound);
                    FlowerTaskTracker.Instance.CollectFlower();
                    return;
                }
                else if (flower.name == "Marigold" && !hasMarigold)
                {
                    hasMarigold = true;
                    Debug.Log("Collected Marigold");

                    //disables prompt before destroying the flower 
                    DisableNearbyPrompt(flower.transform.position);

                    Destroy(flower);
                    PlaySound(pickupSound);
                    FlowerTaskTracker.Instance.CollectFlower();
                    
                    return;
                }
                else if (flower.name == "Aster" && !hasAster)
                {
                    hasAster = true;
                    Debug.Log("Collected Aster");


                    //disables prompt before destroying the flower 
                    DisableNearbyPrompt(flower.transform.position);

                    Destroy(flower);

                    PlaySound(pickupSound);
                    FlowerTaskTracker.Instance.CollectFlower();
                    
                    if (Puzzle1PortalBack != null) Puzzle1PortalBack.SetActive(true);
                    if (Vines != null) Vines.SetActive(false);
                    return;
                }
                else if (flower.name == "Poinsettia" && !hasPoinsettia)
                {
                    hasPoinsettia = true;
                    Debug.Log("Collected Poinsettia");

                    //disables prompt before destroying the flower 
                    DisableNearbyPrompt(flower.transform.position); 

                    Destroy(flower);
                    PlaySound(pickupSound);
                    FlowerTaskTracker.Instance.CollectFlower();
                    
                    return;
                }
            }
        }
    }

    void TryPlaceFlower()
    {
        GameObject[] pillars = GameObject.FindGameObjectsWithTag("PillarTriggerZone");
        foreach (GameObject pillarZone in pillars)
        {
            if (Vector3.Distance(transform.position, pillarZone.transform.position) <= interactionRadius)
            {
                if (pillarZone.name == "Pillar1TriggerZone" && hasTulip && !placedTulip)
                {
                    placedTulip = true;
                    hasTulip = false;
                    Debug.Log("Placed Tulip on Pillar 1");
                    if (SpringLight != null) SpringLight.SetActive(true);
                    FlowerTaskTracker.Instance.PlaceFlower();
                    
                    if (BlockingGates != null) BlockingGates.SetActive(false);
                }
                else if (pillarZone.name == "Pillar2TriggerZone" && hasMarigold && !placedMarigold)
                {
                    placedMarigold = true;
                    hasMarigold = false;
                    Debug.Log("Placed Marigold on Pillar 2");
                    if (SummerLight != null) SummerLight.SetActive(true);
                    FlowerTaskTracker.Instance.PlaceFlower();
                    
                }
                else if (pillarZone.name == "Pillar3TriggerZone" && hasAster && !placedAster)
                {
                    placedAster = true;
                    hasAster = false;
                    Debug.Log("Placed Aster on Pillar 3");
                    if (AutumnLight != null) AutumnLight.SetActive(true);
                    FlowerTaskTracker.Instance.PlaceFlower();
       
                }
                else if (pillarZone.name == "Pillar4TriggerZone" && hasPoinsettia && !placedPoinsettia)
                {
                    placedPoinsettia = true;
                    hasPoinsettia = false;
                    Debug.Log("Placed Poinsettia on Pillar 4");
                    if (WinterLight != null) WinterLight.SetActive(true);
                    FlowerTaskTracker.Instance.PlaceFlower();
                    
                }

                if (placedTulip && placedMarigold && placedAster && placedPoinsettia)
                {
                    //new updated text line here 
                    TaskCompletionManagerRoseGarden.Instance.UpdateTaskText(TaskCompletionManagerRoseGarden.Instance.placeFlowersTaskText, "Put Flowers in the Right Place");

                    Debug.Log("All flowers placed! Opening gate...");
                    if (gateAnimator != null)
                    {
                        gateAnimator.SetTrigger("GateOpen");
                    }
                    if (MainGateBarrier != null)
                    {
                        MainGateBarrier.SetActive(false);
                    }
                }
                return;
            }
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    void DisableNearbyPrompt(Vector3 flowerPosition)
    {
        Collider[] hitColliders = Physics.OverlapSphere(flowerPosition, 3f); // 3f = radius, tweak if needed
        foreach (var hit in hitColliders)
        {
            InteractPromptArea prompt = hit.GetComponent<InteractPromptArea>();
            if (prompt != null && prompt.canvas != null)
            {
                prompt.canvas.gameObject.SetActive(false);
            }
        }
    }
}

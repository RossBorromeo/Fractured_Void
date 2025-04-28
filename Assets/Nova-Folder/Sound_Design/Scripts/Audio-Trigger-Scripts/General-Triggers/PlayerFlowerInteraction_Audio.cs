using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlowerInteraction_Audio : MonoBehaviour
{
    private bool hasTulip = false;
    private bool hasMarigold = false;
    private bool hasAster = false;
    private bool hasPoinsettia = false;

    private bool placedTulip = false;
    private bool placedMarigold = false;
    private bool placedAster = false;
    private bool placedPoinsettia = false;

    public float interactionRadius = 2f; // Range for detection

    public AudioClip pickupSound; // Assign the pickup sound in the inspector
    private AudioSource audioSource;

    public GameObject SpringLight;
    public GameObject SummerLight;
    public GameObject AutumnLight;
    public GameObject WinterLight;

    public Animator gateAnimator; // Assign the gate animator in the inspector
    public GameObject MainGateBarrier; // Assign the barrier object to disable after opening the gate

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
                    Destroy(flower);
                    PlaySound(pickupSound);
                    return;
                }
                else if (flower.name == "Marigold" && !hasMarigold)
                {
                    hasMarigold = true;
                    Debug.Log("Collected Marigold");
                    Destroy(flower);
                    PlaySound(pickupSound);
                    return;
                }
                else if (flower.name == "Aster" && !hasAster)
                {
                    hasAster = true;
                    Debug.Log("Collected Aster");
                    Destroy(flower);
                    PlaySound(pickupSound);
                    return;
                }
                else if (flower.name == "Poinsettia" && !hasPoinsettia)
                {
                    hasPoinsettia = true;
                    Debug.Log("Collected Poinsettia");
                    Destroy(flower);
                    PlaySound(pickupSound);
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
                }
                else if (pillarZone.name == "Pillar2TriggerZone" && hasMarigold && !placedMarigold)
                {
                    placedMarigold = true;
                    hasMarigold = false;
                    Debug.Log("Placed Marigold on Pillar 2");
                    if (SummerLight != null) SummerLight.SetActive(true);
                }
                else if (pillarZone.name == "Pillar3TriggerZone" && hasAster && !placedAster)
                {
                    placedAster = true;
                    hasAster = false;
                    Debug.Log("Placed Aster on Pillar 3");
                    if (AutumnLight != null) AutumnLight.SetActive(true);
                }
                else if (pillarZone.name == "Pillar4TriggerZone" && hasPoinsettia && !placedPoinsettia)
                {
                    placedPoinsettia = true;
                    hasPoinsettia = false;
                    Debug.Log("Placed Poinsettia on Pillar 4");
                    if (WinterLight != null) WinterLight.SetActive(true);
                }

                if (placedTulip && placedMarigold && placedAster && placedPoinsettia)
                {
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
}

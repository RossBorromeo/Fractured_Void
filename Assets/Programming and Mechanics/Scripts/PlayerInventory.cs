using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance; // Singleton for easy access

    public HashSet<string> collectedKeys = new HashSet<string>(); // Stores keys

    public AudioClip KeyCollectedSFX;         //Adam - Plays when key is collected

    private AudioSource audioSource;         //Adam

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            audioSource = gameObject.GetComponent<AudioSource>();//Adam

            Debug.Log("PlayerInventory initialized.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddKey(string keyID)
    {
        if (!collectedKeys.Contains(keyID))
        {
            collectedKeys.Add(keyID);
            Debug.Log($"Key {keyID} added to inventory. Current keys: " + string.Join(", ", collectedKeys));

            PlayKeyCollectedSFX(); //Adam

        }
    }

    public bool HasKey(string keyID)
    {
        return collectedKeys.Contains(keyID);
    }

    private void PlayKeyCollectedSFX() //Adam
    {
        audioSource.PlayOneShot(KeyCollectedSFX);
    }
}

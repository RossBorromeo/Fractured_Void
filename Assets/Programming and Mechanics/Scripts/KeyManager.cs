using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public string keyID; // Unique identifier for the key
    public float pickupRange = 2f;
    private bool isNearPlayer = false;

    void Update()
    {
        if (isNearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            PickUpKey();
        }
    }

    private void PickUpKey()
    {
        Debug.Log($"Key {keyID} picked up!");
        PlayerInventory.Instance.AddKey(keyID); // Add key to player's inventory
        gameObject.SetActive(false); // Hide the key
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }
}

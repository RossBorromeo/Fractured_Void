using System.Collections.Generic;
using UnityEngine;

public class CreepingVines : MonoBehaviour
{
    public float riseSpeed = 1f;
    private Vector3 initialPosition;
    private bool isActive = false;

    // Track which GameObjects we've already damaged this frame
    private HashSet<GameObject> damagedThisFrame = new HashSet<GameObject>();

    private void Start()
    {
        initialPosition = transform.position;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isActive)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        }

        // Clear the set every frame so player can be damaged again next frame (if needed)
        damagedThisFrame.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;

            // Prevent double-hit due to multiple colliders
            if (damagedThisFrame.Contains(player)) return;

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("[Vines] Player touched vines, taking damage...");
                playerHealth.TakeDamage();
                damagedThisFrame.Add(player);
            }
        }
    }

    public void ActivateVines()
    {
        isActive = true;
        gameObject.SetActive(true);
    }

    public void ResetVines()
    {
        transform.position = initialPosition;
        isActive = false;
        gameObject.SetActive(false);
    }
}

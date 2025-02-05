using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage = 10f; // Damage dealt to the player
    public float lifetime = 2f; // Time before the projectile is automatically destroyed

    private void Start()
    {
        // Destroy the projectile after a certain time to avoid clutter
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore collision if it hits the enemy that fired it
        if (other.gameObject == transform.root.gameObject) return;

        Debug.Log($"[EnemyProjectile] Hit {other.name} (Layer: {LayerMask.LayerToName(other.gameObject.layer)})");

        // Check if the projectile hits the player
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.Damage(damage);
                Debug.Log($"[EnemyProjectile] Successfully dealt {damage} damage to {other.name}.");
            }
            else
            {
                Debug.LogError($"[EnemyProjectile] ERROR: {other.name} does not have a Health component!");
            }

            // Destroy the projectile upon impact
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Debug.Log("[EnemyProjectile] Destroyed on terrain impact.");
            Destroy(gameObject);
        }
    }

}
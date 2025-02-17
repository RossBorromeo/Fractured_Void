using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f; // Maximum health
    private float currentHealth;
    private PlayerRespawn playerRespawn;

    private void Start()
    {
        currentHealth = maxHealth; // Initialize health
        playerRespawn = GetComponent<PlayerRespawn>(); // Get PlayerRespawn component if available
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log($"{gameObject.name} healed {amount}. Current health: {currentHealth}");
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");

        if (gameObject.CompareTag("Player"))
        {
            if (playerRespawn != null)
            {
                playerRespawn.Respawn(true); // Reset to original spawn on death
                currentHealth = maxHealth; // Restore health
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the scene if no respawn system
            }
        }
        else
        {
            Destroy(gameObject); // Destroy non-player objects
        }
    }


    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}

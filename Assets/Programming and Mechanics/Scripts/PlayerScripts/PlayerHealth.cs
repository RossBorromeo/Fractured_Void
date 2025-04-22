using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private HealthBarUIJ healthBarUI;
    private PlayerRespawn_Audio playerRespawn;

    [Header("Health Settings")]
    [SerializeField] private int maxHearts = 6;
    [SerializeField] private float damageCooldown = 2f; // Immunity duration in seconds
    private float lastDamageTime = -Mathf.Infinity;     // Time of last damage taken

    [Header("Scene Settings")]
    [SerializeField] private string bedroomSceneName = "Bedroom_Elizabeth_6th";

    [Header("Screen Fader")]
    [SerializeField] private ScreenFader screenFader;

    private void Start()
    {
        healthBarUI = FindFirstObjectByType<HealthBarUIJ>();
        if (healthBarUI == null)
            Debug.LogError("HealthBarUI not found in the scene!");

        playerRespawn = GetComponent<PlayerRespawn_Audio>();
        if (playerRespawn == null)
            Debug.LogError("PlayerRespawn_Audio not found on player!");

        healthBarUI.SetHearts(maxHearts);
    }

    public void TakeDamage()
    {
        // Check if enough time has passed since last damage
        if (Time.time - lastDamageTime < damageCooldown)
        {
            Debug.Log("[PlayerHealth] Damage blocked due to cooldown.");
            return;
        }

        lastDamageTime = Time.time;

        if (healthBarUI == null || playerRespawn == null) return;

        healthBarUI.ReduceHeart();
        int remaining = healthBarUI.GetHeartCount();

        if (remaining <= 0)
        {
            Debug.Log($"[PlayerHealth] No hearts left. Loading scene: {bedroomSceneName}");
            SceneManager.LoadScene(bedroomSceneName);
        }
        else
        {
            bool hasCheckpoint = playerRespawn.HasCheckpoint();

            if (screenFader != null)
            {
                screenFader.FadeOutIn(() =>
                {
                    playerRespawn.Respawn(!hasCheckpoint);
                });
            }
            else
            {
                Debug.LogWarning("ScreenFader not assigned! Respawning without fade.");
                playerRespawn.Respawn(!hasCheckpoint);
            }
        }
    }
}

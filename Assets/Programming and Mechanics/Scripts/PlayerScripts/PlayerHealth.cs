using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private HealthBarUIJ healthBarUI;
    private PlayerRespawn_Audio playerRespawn;

    [Header("Health Settings")]
    [SerializeField] private int maxHearts = 6;

    [Header("Scene Settings")]
    [SerializeField] private string bedroomSceneName = "Bedroom_Elizabeth_6th"; // Editable in Inspector

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
        if (healthBarUI == null || playerRespawn == null) return;

        healthBarUI.ReduceHeart();
        int remaining = healthBarUI.GetHeartCount();

        if (remaining <= 0)
        {
            Debug.Log($"[PlayerHealth] No hearts left. Loading scene: {bedroomSceneName}");
            SceneManager.LoadScene(bedroomSceneName); // Full death = scene reload
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

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private HealthBarUIJ healthBarUI;
    private PlayerRespawn playerRespawn;
    [SerializeField] private int maxHearts = 6;

    private void Start()
    {
        healthBarUI = FindFirstObjectByType<HealthBarUIJ>();
        if (healthBarUI == null)
        {
            Debug.LogError("HealthBarUI not found in the scene!");
        }
        playerRespawn = GetComponent<PlayerRespawn>();

        healthBarUI.SetHearts(maxHearts); // Always reset on start
    }

    public void TakeDamage()
    {
        if (healthBarUI == null || playerRespawn == null) return;

        healthBarUI.ReduceHeart();
        int remaining = healthBarUI.GetHeartCount();

        if (remaining <= 0)
        {
            SceneManager.LoadScene("Bedroom_Elizabeth_6th"); // No heart persistence needed
        }
        else
        {
            bool hasCheckpoint = playerRespawn.HasCheckpoint();
            playerRespawn.Respawn(!hasCheckpoint);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class SceneTeleport : MonoBehaviour
{
    [SerializeField] private string sceneName = "NewScene"; // Name of the scene to load

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            SceneManager.LoadScene(sceneName); // Load the new scene
        }
    }
}
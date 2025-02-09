using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene management

public class ResetScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
        }
    }
}
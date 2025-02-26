using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using NUnit.Framework;

public class KillZoneManager : MonoBehaviour
{

    private HealthBarUI healthBarUI;
    [SerializeField] private int maxHearts = 6; // max heart count
    private void Start()
    {
        healthBarUI = FindFirstObjectByType<HealthBarUI>(); // Find the HealthBarUI script

        /***********          REPLACE SCENE NAME WITH THE MOST CURRENT SCENE NAME AND UPDATE BUILD SETTINGS                               *************/

        if (SceneManager.GetActiveScene().name == "Bedroom_Scene_Latest") // Replace with actual scene name
        {
            ResetHearts();
        }

        else { 
        // Code to restore previous hearts from last attempt 
            if (PlayerPrefs.HasKey("RemainingHearts") && healthBarUI != null)
            {
                int savedHearts = PlayerPrefs.GetInt("RemainingHearts");
              

                if (savedHearts <= 0)
                {
                    ResetHearts(); //reset if hearts were empty from last session
                }
                else {  healthBarUI.SetHearts(savedHearts); // Restore heart count

                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (healthBarUI != null)
            {
                healthBarUI.ReduceHeart(); // Reduce one heart

                // save the current heat count before reloading
                PlayerPrefs.SetInt("RemainingHearts", healthBarUI.GetHeartCount());
                PlayerPrefs.Save();

                if (healthBarUI.GetHeartCount() <= 0)
                {
                    SceneManager.LoadScene("Bedroom_Scene_Latest"); ; // Calls Bedroom scene from SceneLoader
                    PlayerPrefs.SetInt("RemainingHearts", maxHearts); // ensures reset before loading
                    PlayerPrefs.Save();
                    
                    return; 
                }
                
               
            }

          
            PlayerRespawnUI playerRespawnUI = other.GetComponent<PlayerRespawnUI>();

            if (playerRespawnUI != null)
            {
                if (HasCheckpoint(playerRespawnUI)) // Check if the player has a checkpoint
                {
                    playerRespawnUI.Respawn(); // Respawn at last checkpoint
                }
                else if (!HasCheckpoint(playerRespawnUI) || (healthBarUI != null && healthBarUI.GetHeartCount() <= 0)) // If no checkpoint or hearts are 0
                {
                    SceneManager.LoadScene("Bedroom_Scene_Latest"); // Load the bedroom scene
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the level
                }
            }
        }

        
    }
        public bool HasCheckpoint(PlayerRespawnUI playerRespawnUI)
        {
            // check if the player has reached a checkpoint
            // Vector3 spawnPositionUI = playerRespawnUI.transform.position;
            // return spawnPositionUI != playerRespawnUI.gameObject.transform.position;


            return playerRespawnUI.HasCheckpoint();
        }

    private void ResetHearts()
    {
        if (healthBarUI != null)
        {
            healthBarUI.SetHearts(maxHearts); // reset to full hearts
            PlayerPrefs.SetInt("RemainingHearts", maxHearts); // save full hearts
            PlayerPrefs.Save();
        }
    }
}
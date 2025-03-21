//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine;
//using NUnit.Framework;

//public class KillZoneManager : MonoBehaviour
//{

//    private HealthBarUI healthBarUI;
//    [SerializeField] private int maxHearts = 6; // max heart count
//    private void Start()
//    {
//        healthBarUI = FindFirstObjectByType<HealthBarUI>(); // find the HealthBarUI script
//        if (PlayerPrefs.HasKey("RemainingHearts"))
//        {
//            int savedHearts = PlayerPrefs.GetInt("RemainingHearts");

//            if (savedHearts <= 0)
//            {
//                ResetHearts(); // Reset hearts when entering Bedroom
//            }
//            else
//            {
//                healthBarUI.SetHearts(savedHearts);
//            }
//        }
//        else
//        {
//            ResetHearts(); // Default to max hearts if no data exists
//        }
//        /*
//        // Code to restore previous hearts from last attempt 
//            if (PlayerPrefs.HasKey("RemainingHearts") && healthBarUI != null)
//            {
//                int savedHearts = PlayerPrefs.GetInt("RemainingHearts");

//                if (savedHearts <= 0)
//                {
//                    ResetHearts(); //reset if hearts were empty from last session
//                }
//                else
//                {
//                    healthBarUI.SetHearts(savedHearts); // Restore heart count

//                }

//            }
//        */
//        /***********          REPLACE SCENE NAME WITH THE MOST CURRENT SCENE NAME AND UPDATE BUILD SETTINGS                               *************/
//        if (SceneManager.GetActiveScene().name == "Bedroom_Elizabeth_5th")
//        {
//            ResetHearts();
//        }

//    }

//    private void OnTriggerEnter(Collider other)
//    {
//         if (other.CompareTag("Player"))
//         {
//                if (healthBarUI != null)
//                {
//                    healthBarUI.ReduceHeart();
//                    int updatedHearts = healthBarUI.GetHeartCount();
                
//                if (updatedHearts <= 0)
//                    {
//                        PlayerPrefs.SetInt("RemainingHearts", maxHearts); // Reset hearts before loading bedroom
//                        PlayerPrefs.Save();
//                        SceneManager.LoadScene("Bedroom_Elizabeth_5th");
//                        return;
//                    }
//                }
//         }


//            PlayerRespawnUI playerRespawnUI = other.GetComponent<PlayerRespawnUI>();

//            if (playerRespawnUI != null)
//            {
//                if (HasCheckpoint(playerRespawnUI)) // Check if the player has a checkpoint
//                {
//                    playerRespawnUI.Respawn(); // Respawn at last checkpoint
//                }
                
//                else
//                {
//                    SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the level with no heart reset 
//                }
//            }
//    }

        

//        public bool HasCheckpoint(PlayerRespawnUI playerRespawnUI)
//        {
//            // check if the player has reached a checkpoint
//            // Vector3 spawnPositionUI = playerRespawnUI.transform.position;
//            // return spawnPositionUI != playerRespawnUI.gameObject.transform.position;


//            return playerRespawnUI.HasCheckpoint();
//        }

//    private void ResetHearts()
//    {
//        if (healthBarUI != null)
//        {
//            healthBarUI.SetHearts(maxHearts); // reset to full hearts
            
//        }
//        PlayerPrefs.SetInt("RemainingHearts", maxHearts); // save full hearts
//            PlayerPrefs.Save();
//    }
//}
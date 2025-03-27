using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAnimationINUI : MonoBehaviour
{
    
   

    

    [Header("Player")]
    [SerializeField] private Transform playerTransform;
    private PlayerMovement playerMovement;

    private bool hasTriggered = false;
    private bool isRunning = false;

    void Start()
    {
        if (playerTransform != null)
        {
            playerMovement = playerTransform.GetComponent<PlayerMovement>();
            if (playerMovement == null)
                Debug.LogError("[RunAwayTrigger] PlayerMovement not found!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (playerMovement != null)
                playerMovement.SetMovementEnabled(false); //  Freeze player
        }
    }

    void how()
    {
      

        //  Re-enable player movement
        if (playerMovement != null)
            playerMovement.SetMovementEnabled(true);
    }

   
}



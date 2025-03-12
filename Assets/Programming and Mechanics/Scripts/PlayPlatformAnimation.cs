using UnityEngine;

public class PlayPlatformAnimation : MonoBehaviour
{
    public Animator platformAnimator; // Reference to the Animator component

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player entered the trigger
        {
            Debug.Log("Player entered trigger - Playing platform animation");
            platformAnimator.Play("Platform Extension"); // Play the animation
        }
    }
}

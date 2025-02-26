using UnityEngine;

public class VineTrigger : MonoBehaviour
{
    public CreepingVines vines; // Assign the vine GameObject in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && vines != null)
        {
            vines.ActivateVines(); // Activate the vines when the player enters
        }
    }
}

using UnityEngine;

public class DisableObjectOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject objectToDisable;
    [SerializeField] private string triggeringTag = "Player"; // Set this to the tag of the object that should trigger the event

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggeringTag))
        {
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }
            else
            {
                Debug.LogWarning("No object assigned to disable in the inspector.");
            }
        }
    }
}

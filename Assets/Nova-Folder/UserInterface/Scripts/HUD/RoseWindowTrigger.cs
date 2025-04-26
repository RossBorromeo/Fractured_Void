using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// to be attached to a trigger box


public class RoseWindowTrigger : MonoBehaviour
{
    public HouseManager houseManager;//pass in the HouseManager script 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && houseManager != null)
        {
            houseManager.RoseWindowTrigger(); // notifies HouseManager when player enters the trigger
        }
    }
}

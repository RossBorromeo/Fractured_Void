using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Collider))]
public class CamZone : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;
    private static CinemachineVirtualCamera activeCamera = null;

    private void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("[CamZone] Virtual Camera not assigned in the Inspector!", this);
            return;
        }

        virtualCamera.enabled = false; // Start disabled
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other is SphereCollider)
        {
            Debug.Log("[CamZone] Player entered camera zone!");

            // Disable the previous camera if another is active
            if (activeCamera != null && activeCamera != virtualCamera)
            {
                activeCamera.enabled = false;
            }

            // Set the new active camera
            virtualCamera.enabled = true;
            activeCamera = virtualCamera;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other is SphereCollider)
        {
            Debug.Log("[CamZone] Player exited camera zone!");

            // Only disable if this was the active camera
            if (activeCamera == virtualCamera)
            {
                virtualCamera.enabled = false;
                activeCamera = null;
            }
        }
    }

    private void OnValidate()
    {
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogWarning("CamZone requires a Collider component!");
        }
        else
        {
            col.isTrigger = true;
        }
    }
}

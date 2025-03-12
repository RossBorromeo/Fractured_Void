using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Collider))]
public class CamZone : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;
    private static List<CinemachineVirtualCamera> activeCameras = new List<CinemachineVirtualCamera>();
    private static CinemachineVirtualCamera defaultCamera; // Reference to Vcam Main

    private void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("[CamZone] Virtual Camera not assigned in the Inspector!", this);
            return;
        }

        virtualCamera.enabled = false; // Start disabled

        // Find Vcam Main if not assigned
        if (defaultCamera == null)
        {
            GameObject vcamObject = GameObject.Find("Vcam Main");
            if (vcamObject != null)
            {
                defaultCamera = vcamObject.GetComponent<CinemachineVirtualCamera>();
            }

            if (defaultCamera == null)
            {
                Debug.LogError("Cinemachine Virtual Camera 'Vcam Main' not found in the scene!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other is SphereCollider)
        {
            Debug.Log("[CamZone] Player entered camera zone!");

            if (!activeCameras.Contains(virtualCamera))
            {
                activeCameras.Add(virtualCamera);
                virtualCamera.enabled = true;
            }

            // Disable the default camera while in a zone
            if (defaultCamera != null)
            {
                defaultCamera.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other is SphereCollider)
        {
            Debug.Log("[CamZone] Player exited camera zone!");

            if (activeCameras.Contains(virtualCamera))
            {
                activeCameras.Remove(virtualCamera);
                virtualCamera.enabled = false;
            }

            // If no more active cameras, switch back to default
            if (activeCameras.Count == 0 && defaultCamera != null)
            {
                Debug.Log("[CamZone] No active cameras left, switching back to Vcam Main.");
                defaultCamera.enabled = true;
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

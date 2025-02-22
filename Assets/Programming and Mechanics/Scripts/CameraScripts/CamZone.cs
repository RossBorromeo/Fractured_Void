using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Collider))]
public class CamZone : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    private void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("[CamZone] Virtual Camera not assigned in the Inspector!", this);
            return;
        }

        virtualCamera.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other is SphereCollider)
        {
            Debug.Log("[CamZone] Player entered camera zone!");
            virtualCamera.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other is SphereCollider)
        {
            Debug.Log("[CamZone] Player exited camera zone!");
            virtualCamera.enabled = false;
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

using UnityEngine;

public class Sensor_Bandit : MonoBehaviour
{
    private int m_ColCount = 0; // Tracks the number of colliders in contact
    private float m_DisableTimer; // Timer to temporarily disable the sensor

    private void OnEnable()
    {
        m_ColCount = 0; // Reset the collider count when enabled
    }

    public bool State()
    {
        // Return false if the sensor is disabled, true if any colliders are detected
        if (m_DisableTimer > 0)
            return false;
        return m_ColCount > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Increment the collider count when a trigger is entered
        m_ColCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        // Decrement the collider count when a trigger is exited
        m_ColCount--;
    }

    private void Update()
    {
        // Reduce the disable timer over time
        if (m_DisableTimer > 0)
            m_DisableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        // Disable the sensor for a specified duration
        m_DisableTimer = duration;
    }
}
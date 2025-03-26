using UnityEngine;

public class FoliageSway : MonoBehaviour
{
    public float swayStrength = 0.2f;
    public float swaySpeed = 2.0f;
    
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        float swayOffset = Mathf.Sin(Time.time * swaySpeed) * swayStrength;
        transform.position = originalPosition + new Vector3(0, swayOffset, 0);
    }
}

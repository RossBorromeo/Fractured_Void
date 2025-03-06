using UnityEngine;

public class BobbingFlower : MonoBehaviour
{
    public float bobSpeed = 2f;  // Speed of bobbing motion
    public float bobHeight = 0.2f;  // Maximum height difference
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Store the initial position
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}

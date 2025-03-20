using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float knockbackForce = 5f; // Amount of force applied to the player
    private Vector3 direction;
    private string targetTag;

    public void Initialize(Vector3 shootDirection)
    {
        direction = shootDirection.normalized;
        Destroy(gameObject, lifetime);
    }

    public void SetTargetTag(string tag)
    {
        targetTag = tag;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Rigidbody targetRb = other.GetComponent<Rigidbody>();
            if (targetRb != null)
            {
                Vector3 knockbackDirection = direction; // Apply force in the projectile's direction
                targetRb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                Debug.Log($"[Projectile] Hit {other.name}. Applied knockback force.");
            }
            Destroy(gameObject); // Destroy projectile on impact
        }
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 3f;
    private float damage;
    private Vector3 direction;
    private string targetTag;    

    public void Initialize(float damageAmount, Vector3 shootDirection)
    {
        damage = damageAmount;
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
        Health targetHealth = other.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.Damage(damage);
            Debug.Log($"[Projectile] Hit {other.name}. Dealt {damage} damage.");
            Destroy(gameObject); // Destroy projectile on impact with the player
        }
    }
}
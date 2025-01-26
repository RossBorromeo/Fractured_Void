using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damageAmount = 10; // Amount of damage dealt
    [SerializeField] private LayerMask enemyLayer; // Layer for detecting enemies

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is on the enemy layer
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            // Check if the object has a Health component
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.Damage(damageAmount); // Apply damage
                Debug.Log($"[AttackArea] Dealt {damageAmount} damage to {other.name}");
            }
            else
            {
                Debug.Log($"[AttackArea] {other.name} does not have a Health component.");
            }
        }
        else
        {
            Debug.Log($"[AttackArea] {other.name} is not on the enemy layer.");
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the attack area in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}

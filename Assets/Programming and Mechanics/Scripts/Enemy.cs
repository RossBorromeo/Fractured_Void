using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float stopDuration = 2f;
    private Transform currentTarget;
    private bool isMoving = true;

    [Header("Animation Settings")]
    [SerializeField] private Animator animator;

    [Header("Health Component")]
    private Health health;
    private bool isDead = false;

    private void Start()
    {
        // Initialize health
        health = GetComponent<Health>();
        if (health == null)
        {
            Debug.LogError("[Enemy] Missing Health component!");
        }

        // Initialize animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("[Enemy] Missing Animator component!");
        }

        // Set initial patrol target
        currentTarget = pointA;
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        // Patrol logic
        if (isMoving)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (currentTarget == null) return;

        // Move towards the current target
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        transform.position += direction * patrolSpeed * Time.deltaTime;

        // Check if close to the target
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.2f)
        {
            StartCoroutine(SwitchTargetWithDelay());
        }

        // Update Animator Speed
        if (animator != null)
        {
            animator.SetFloat("Speed", patrolSpeed);
        }
    }

    private IEnumerator SwitchTargetWithDelay()
    {
        isMoving = false;

        // Stop movement and play idle animation
        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
        }

        yield return new WaitForSeconds(stopDuration);

        // Switch to the next target
        currentTarget = currentTarget == pointA ? pointB : pointA;
        isMoving = true;
    }

    public void TakeDamage(float damage)
    {
        if (isDead || health == null) return;

        // Apply damage
        health.Damage(damage);

        // Trigger Hurt animation
        if (animator != null)
        {
            animator.SetBool("IsHurt", true);
            StartCoroutine(ResetHurtAnimation());
        }

        // Check if the enemy is dead
        if (health.GetCurrentHealth() <= 0)
        {
            HandleDeath();
        }
    }

    private IEnumerator ResetHurtAnimation()
    {
        yield return new WaitForSeconds(0.5f); // Adjust based on hurt animation duration
        if (animator != null)
        {
            animator.SetBool("IsHurt", false);
        }
    }

    private void HandleDeath()
    {
        if (isDead) return;
        isDead = true;

        // Trigger death animation
        if (animator != null)
        {
            animator.SetBool("IsDead", true);
        }

        // Disable movement
        isMoving = false;

        // Destroy enemy after death animation
        Destroy(gameObject, 2f);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw patrol points
        Gizmos.color = Color.green;
        if (pointA != null && pointB != null)
        {
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}

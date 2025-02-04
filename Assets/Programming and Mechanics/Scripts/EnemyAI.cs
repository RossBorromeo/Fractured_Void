using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Combat Settings")]
    public Transform player;
    public LayerMask whatIsPlayer;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float timeBetweenAttacks = 2f;
    private bool alreadyAttacked;

    [Header("Animation")]
    private Animator animator;

    [Header("Health Component")]
    private Health health;
    private bool isDead = false;

    private void Awake()
    {
        player = GameObject.Find("Oliver").transform;
        animator = GetComponent<Animator>();

        // Instantiate the Health component
        health = GetComponent<Health>();
        if (health == null)
        {
            Debug.LogError("[EnemyAI] Missing Health component!");
        }

        if (animator == null)
        {
            Debug.LogError("[EnemyAI] Missing Animator component!");
        }
    }

    private void Update()
    {
        if (isDead) return; // Prevent any actions if dead

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsIdle", true);
        }
    }

    private void AttackPlayer()
    {
        transform.LookAt(player);
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsAttacking", true);

        if (!alreadyAttacked)
        {
            Debug.Log("[EnemyAI] Attacking player with ranged attack!");
            ShootProjectile();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // Ensure the projectile has the correct layer
            int layerIndex = LayerMask.NameToLayer("EnemyProjectile");
            if (layerIndex != -1)
            {
                projectile.layer = layerIndex;
            }
            else
            {
                Debug.LogError("[EnemyAI] Layer 'EnemyProjectile' does not exist!");
            }

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Ensure the projectile moves toward the player
                Vector3 direction = (player.position - firePoint.position).normalized;
                rb.velocity = direction * projectileSpeed;

                // Disable gravity if not needed
                rb.useGravity = false;

                // Prevent rotation issues
                rb.constraints = RigidbodyConstraints.FreezeRotation;

                // Ignore collision with the enemy itself
                Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());

                Debug.Log("[EnemyAI] Fired a projectile at player.");
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsIdle", true);
    }

    public void TakeDamage(float damage)
    {
        if (health != null)
        {
            health.Damage(damage);

            if (health.GetCurrentHealth() > 0)
            {
                animator.SetBool("IsHurt",true);
                Debug.Log($"[EnemyAI] Took {damage} damage. Current Health: {health.GetCurrentHealth()}");
            }
            else
            {
                HandleDeath();
            }
        }
    }


    private void HandleDeath()
    {
        if (isDead) return;
        isDead = true;
        animator.SetBool("IsDead", true);
        Debug.Log("[EnemyAI] Death triggered. Playing death animation...");

        GetComponent<Collider>().enabled = false; // Disable interactions
        Destroy(gameObject, 2f); // Destroy after animation plays
    }
}
using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret Settings")]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float attackAngle = 45f; // Defines the cone angle for detection

    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileStorage;
    [SerializeField] private Transform attackTarget; // Target point on the player
    [SerializeField] private Health turretHealth; // Use Health component for turret

    private Transform player;
    private Animator animator;
    private bool canShoot = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.Play("Idle");
        }
    }

    private void Update()
    {
        if (player == null || attackTarget == null) return;

        Vector3 directionToTarget = attackTarget.position - transform.position;
        float distance = directionToTarget.magnitude;
        directionToTarget.Normalize();

        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        if (distance <= detectionRadius && angleToTarget <= attackAngle)
        {
            if (canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (turretHealth != null)
        {
            turretHealth.Damage(amount);
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;

        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }

        yield return new WaitForSeconds(0.5f); // Delay for animation sync

        if (projectilePrefab != null && firePoint != null && projectileStorage != null && attackTarget != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity, projectileStorage);
            projectile.GetComponent<Projectile>().Initialize((attackTarget.position - firePoint.position).normalized);
            projectile.GetComponent<Projectile>().SetTargetTag("Player"); // Ensure projectile only affects the player
        }

        yield return new WaitForSeconds(fireRate);

        if (animator != null)
        {
            animator.Play("Idle");
        }

        canShoot = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

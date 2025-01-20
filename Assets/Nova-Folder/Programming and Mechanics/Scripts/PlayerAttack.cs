using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private bool attacking = false;

    private float timeToAttack = 0.25f;
    private float attackTimer = 0;

    private Animator animator; // Reference to Animator

    void Start()
    {
        // Reference the child object for the attack area
        attackArea = transform.GetChild(0).gameObject;
        // Reference the Animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (attacking)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= timeToAttack)
            {
                attacking = false;
                attackTimer = 0;
                attackArea.SetActive(attacking);
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);

        // Trigger the attack animation
        animator.SetTrigger("Attack");
    }
}

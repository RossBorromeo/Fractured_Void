using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public GameObject attackArea; // Reference to the attack area
    public Animator animator;     // Reference to the Animator
    public float timeToAttack = 0.5f; // Time for the attack to complete

    private bool attacking = false;

    void Start()
    {
        if (attackArea != null)
        {
            attackArea.SetActive(false); // Ensure attack area starts disabled
        }
    }



    
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !attacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        attacking = true;

        // Trigger the attack animation
        animator.SetTrigger("Attack");

        // Enable the attack area during the attack
        if (attackArea != null)
        {
            attackArea.SetActive(true);
        }

        // Disable the attack area and reset after the attack is complete
        StartCoroutine(DisableAttackArea());
    }

    private IEnumerator DisableAttackArea()
    {
        yield return new WaitForSeconds(timeToAttack); // Wait for the attack to complete

        if (attackArea != null)
        {
            attackArea.SetActive(false); // Disable the attack area
        }

        attacking = false;
    }
}

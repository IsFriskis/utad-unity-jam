using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeScript : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int attackDamage;
    public AIPath aiPath;
    private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        maxHealth = 75;
        currentHealth = maxHealth;
        attackDamage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //Flip al sprite en función de la velocidad
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void Die()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            animator.SetBool("isDead", true);
            Transform parentTransform = transform.parent;
            Destroy(this);
            if (parentTransform != null)
            {
                Destroy(parentTransform.gameObject, 1f); // Puedes ajustar el tiempo de espera antes de destruirlo
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            animator.SetTrigger("isHurt");
            Die(); // Check if the enemy should die after taking damage
        }
    }
}

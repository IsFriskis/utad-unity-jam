using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ArcherScript : BasicEnemyScript
{
    public GameObject arrow;
    public Transform arrowPosition;
    private float attackDelay = 7f;
    private float attackTimer = 0f;
    private bool isAttacking = true;

    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        attackDamage = 15;
        deathTimer = 15f;
        detectionRange = 10;
        
    }

    void Update()
    {
        LookAtPlayer();
        if (!isStunned)
        {
            if (isAttacking)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackDelay)
                {
                    attackTimer = 0;
                    isAttacking = false;
                }
            }
            IALogic();
        }
    }

    public override void TakeDamage(int damage)
    {   
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        base.Die();
    }
    public override void IALogic()
    {
        float distanceToPlayer = Vector3.Distance(playableCharacter.transform.position, transform.position);
        if (distanceToPlayer <= detectionRange && !isAttacking)
        {
            isAttacking = true;
            anim.SetBool("isAttacking", true);
            Attack();
        }
        if(distanceToPlayer >= detectionRange && !isAttacking)
        {
            isAttacking= false;
            anim.SetBool("isAttacking", false);
        }
    }
    public override void Attack()
    {
        if(!isStunned && !isDead)
        {
            Instantiate(arrow, arrowPosition.position, Quaternion.identity);
        }
    }

    void LookAtPlayer()
    {
        Vector3 archerPos = transform.position;
        Vector3 knightPos = playableCharacter.transform.position;
        if(knightPos.x <= archerPos.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (knightPos.x >= archerPos.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ArcherScript : BasicEnemyScript
{
    //Almacenamos la posicion del npc y la del jugador
    private Vector2 myPosition;

    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        attackDamage = 15;
        deathTimer = 15f;
        detectionRange = 10;
    }

    void FixedUpdate()
    {
        FollowViewPlayer();
        Attack();
    }

    public override void TakeDamage(int damage)
    {   
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        base.Die();
    }

    public override void Attack()
    {
        myPosition = transform.position;
        float distanceToPlayer = Vector2.Distance(myPosition, GameObject.FindGameObjectWithTag("Player").transform.position);
        float distanceToGhostPlayer = Vector2.Distance(myPosition, GameObject.FindGameObjectWithTag("GhostPlayer").transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            anim.SetBool("isAttacking", true);
        }

        else if (distanceToGhostPlayer <= detectionRange)
        {
            anim.SetBool("isAttacking", true);

        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    public override void IALogic()
    {
        throw new System.NotImplementedException();
    }
}


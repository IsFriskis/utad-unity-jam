using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ArcherScript : BasicEnemyScript
{
    //Almacenamos la posicion del npc y la del jugador
    private Vector2 myPosition;
    private Vector2 playerPosition;

    //Hay que instanciar un gameobject arrow para que el proyectil pueda impactar al jugador
    public GameObject arrow;
    //Hay que comprobar el collider del arrow para realizar daño si impacta con el transform del player

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
        ArcherAttack();
    }

    public override void TakeDamage(int damage)
    {   
        base.TakeDamage(damage);
    }

    private void ArcherAttack()
    {
        myPosition = transform.position;
        float distanceToPlayer = Vector2.Distance(myPosition, GameObject.FindGameObjectWithTag("Player").transform.position);
        float distanceToGhostPlayer = Vector2.Distance(myPosition, GameObject.FindGameObjectWithTag("GhostPlayer").transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            anim.SetBool("isAttacking", true);
            AttackPlayer();
        }

        else if (distanceToGhostPlayer <= detectionRange)
        {
            anim.SetBool("isAttacking", true);
            AttackGhostPlayer();
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    private void AttackPlayer()
    {
        
    }

    private void AttackGhostPlayer()
    {
            
    }
    public override void Die()
    {
        capCol2D.offset = new Vector2(0, 0.07f);
        base.Die();
    }
}


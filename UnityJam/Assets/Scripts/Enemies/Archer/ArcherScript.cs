using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ArcherScript : BasicEnemyScript
{
    //[SerializeField]
    //public GameObject partner;
    //Almacenamos la posicion del npc y la del jugador
    private Vector2 myPosition;
    private Vector2 playerPosition;


    //Hay que instanciar un gameobject arrow para que el proyectil pueda impactar al jugador
    //public GameObject arrow;
    //Hay que comprobar el collider del arrow para realizar daño si impacta con el rigidbody del player
    //public GameObject arrowPosition;

    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        attackDamage = 15;
        deathTimer = 20f;
    }

    void FixedUpdate()
    {
        
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    private void ArcherAttack()
    {

    }
    public override void Die()
    {
        capCol2D.offset = new Vector2(0, 0.07f);
        base.Die();
    }
}


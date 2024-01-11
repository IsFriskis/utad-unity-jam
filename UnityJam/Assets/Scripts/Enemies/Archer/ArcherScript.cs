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
    //Hay que comprobar el collider del arrow para realizar daño si impacta con el rigidbody del player
    public GameObject arrowPosition;
    //Podemos modificar el rango del ataque a melee
    public float meleeAttackRange = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        FollowViewPlayer();
        myPosition = transform.position;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        float distance = Vector2.Distance(myPosition, playerPosition);
        //Debug.Log(distance);

        //Parametros de animaciones con respecto a la distancia del jugador. Podriamos realizar un behaviour tree para que intente mantener la distancia con el jugador
        //Una vez sea detectado para poder disparar a distancia de forma segura.
        //Implementar colliders
        if (distance <= meleeAttackRange)
        {
            //Debug.Log("Rango de ataque a melee");
            anim.SetBool("MeleeAttack", true);
            anim.SetBool("RangedAttack", false);
            anim.SetBool("Stop", false);
        }
        else if (distance > meleeAttackRange && distance <= 8f)
        {
           // Debug.Log("Rango de ataque a distancia");
            anim.SetBool("MeleeAttack", false);
            anim.SetBool("RangedAttack", true);
            anim.SetBool("Stop", false);
        }
        else
        {
            //Debug.Log("No hay enemigo");
            anim.SetBool("MeleeAttack", false);
            anim.SetBool("RangedAttack", false);
            anim.SetBool("Stop", true);
        }
    }
}

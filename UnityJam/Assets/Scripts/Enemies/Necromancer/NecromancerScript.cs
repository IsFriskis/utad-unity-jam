using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerScript : BasicEnemyScript
{
    private Vector2 myPosition;
    private Vector2 playerPosition;
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
       
        //Introduciremos aqui la lógica de spawn del necromancer porque este enemigo consiste en que cuando detecte al jugador cree una entidad de enemigo aleatorio
        //Que perseguira de forma indefinida al jugador. Si el jugador se acerca mucho a la posicion del necromancer este atacara a melee, mientras tanto realizara
        //Ataques con proyectiles. (Discutir con el grupo)
        //Introducir colliders
    }
    //El necromancer es capaz de invocar NPC enemigos que le ayudan en el combate
    private void SpawnEnemy()
    {

    }
}

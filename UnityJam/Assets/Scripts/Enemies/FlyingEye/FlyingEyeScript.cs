using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeScript : MonoBehaviour
{   
    private int maxHealth;
    private int currentHealth;
    private int attackDamage;
    private float deathTimer;
    public AIPath aiPath;
    void Start()
    {
        maxHealth = 75;
        currentHealth = maxHealth;
        attackDamage = 10;
        deathTimer = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        //Flip al sprite en función de la velocidad
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f,1f,1f);
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Logica de atacar cuando entre con el collider del player

    }



}

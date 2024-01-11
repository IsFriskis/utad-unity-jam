using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeScript : BasicEnemyScript
{
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Se deberia poder mover si se implementa el aStarProject para que haga un pathfinding del jugador y si se encuentra cerca que realize las animaciones correspondientes
        //Para generar daño en el jugador
    }
}

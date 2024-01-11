using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditScript : BasicEnemyScript
{
   
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        FollowViewPlayer();
        //Aqui introduciremos el comportamiento que va a tener el Bandido básico. Podría funcionar de forma en que si el jugador entra en su rango le persiga y cuando alcanze
        //al jugador ataque sin parar.
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : BasicEnemyScript
{
    [SerializeField]
    private float attackRange = 2.7f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IALogic();
    }
    public override void Attack()
    {
        float distanceToPlayer = Vector3.Distance(playableCharacter.transform.position, transform.position);
        anim.SetBool("Attack", true);

        if (distanceToPlayer <= attackRange)
        {
        Debug.Log("Attack");
            playableCharacter.GetComponent<Player>().TakeDamage(attackDamage, transform.position.x);
        }
    }

    public override void IALogic()
    {
        Vector3 movimiento = Vector3.zero;
        float distanceToPlayer = Vector3.Distance(playableCharacter.transform.position, transform.position);

        Debug.Log(distanceToPlayer);
        FollowViewPlayer();
        //Si el jugador esta en rango de ataque
        if (distanceToPlayer <= attackRange)
        {
            Attack();

        }
        //Si el jugador esta en rango de deteccion pero no de ataque
        //Muevete hacia él
        else if (lookingLeft)
        {
            movimiento -= transform.right * speed * Time.deltaTime;
        }
        else if (!lookingLeft)
        {
            movimiento += transform.right * speed * Time.deltaTime;
        }
    }
}

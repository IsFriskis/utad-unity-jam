using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.Enemies.Bandit
{
    public class BanditScript : BasicEnemyScript
    {

        [SerializeField]
        private bool isHeavy = false;

        protected float attackRange = 4.53f;
        // Use this for initialization
        void Start()
        {
            if (isHeavy)
            {
                attackRange = 5f;
            }
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
                if(playableCharacter.GetComponent<Player>())
                {
                    playableCharacter.GetComponent<Player>().TakeDamage(attackDamage, transform.position.x);
                }
                else if (playableCharacter.GetComponent<Spirit>())
                {
                    playableCharacter.GetComponent<Spirit>().TakeDamage(attackDamage, transform.position.x);
                }
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
            if(!isStunned)
            {
                Vector3 movimiento = Vector3.zero;
                float distanceToPlayer = Vector3.Distance(playableCharacter.transform.position, transform.position);

                //Si el jugador esta en rango de deteccion
                if (distanceToPlayer <= detectionRange)
                {
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

                //Si el jugador no esta en rango de deteccion MODO PATRULLA
                else
                {
                    //Si el enemigo esta mirando a la izquierda muevete a la izquierda

                    if (Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position) >= 1)
                    {
                        if (currentWaypoint == 0)
                        {
                            movimiento -= transform.right * speed * Time.deltaTime;
                            lookingLeft = true;
                        }
                        else
                        {
                            movimiento += transform.right * speed * Time.deltaTime;
                            lookingLeft = false;
                        }
                    }
                    else if (Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position) <= 1)
                    {
                        if (currentWaypoint == 0)
                        {
                            currentWaypoint = 1;
                        }
                        else
                        {
                            currentWaypoint = 0;
                        }
                    }
                }
                if (movimiento.magnitude > 1.0f)
                {
                    movimiento.Normalize();
                }
                anim.SetFloat("Speed", movimiento.magnitude);
                transform.Translate(movimiento);
            }
        }
    }
}

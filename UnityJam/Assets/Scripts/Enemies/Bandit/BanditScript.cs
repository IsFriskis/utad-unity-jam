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

        protected float attackRange = 4.35f;
        // Use this for initialization
        void Start()
        {
            if (isHeavy)
            {
                attackRange = 4.55f;
            }
            Attack();
        }

        // Update is called once per frame
        void Update()
        { 
            IALogic();
        }
        public override void Attack()
        {
            //playableCharacter.GetComponent<PlayableCharacterScript>().TakeDamage(attackDamage);
            anim.SetBool("Attack", true);
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

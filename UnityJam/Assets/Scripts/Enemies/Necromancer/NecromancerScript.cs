using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerScript : BasicEnemyScript
{
    [SerializeField]
    private GameObject[] staffPositionToShoot;

    [SerializeField]
    private GameObject skeletonPrefab;

    [SerializeField]
    private GameObject throwablePrefab;

    [SerializeField]
    private float summonDelay = 8f;
    [SerializeField]
    private float attackDelay = 4f;
    [SerializeField]
    private float strafeDelay = 2f;

    private float summonTimer = 0f;
    private float attackTimer = 0f;
    private float strafeTimer = 2f;

    private bool isSummoning = false;
    private bool isAttacking = false;
    private bool isStrafing = false;

    void Start()
    {
        maxHealth = 500;
        currentHealth = maxHealth;
        attackDamage = 30;
    }
    void Update()
    {
        if (!isDead)
        {
            if (isSummoning)
            {
                summonTimer += Time.deltaTime;
                if (summonTimer >= summonDelay)
                {
                    summonTimer = 0f;
                    isSummoning = false;
                }
            }

            if (isAttacking)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackDelay)
                {
                    attackTimer = 0f;
                    isAttacking = false;
                }
            }
            if (isStrafing)
            {
                strafeTimer += Time.deltaTime;
                if (strafeTimer >= strafeDelay)
                {
                    strafeTimer = 0f;
                    isStrafing = false;
                }
            }
            FollowViewPlayer();
            IALogic();
        }
    }

    public override void IALogic()
    {
        float distanceToPlayer = Vector3.Distance(playableCharacter.transform.position, transform.position);
        Vector3 movimiento = Vector3.zero;
        
        if (!isSummoning)
        {
 
            isSummoning = true;
            anim.SetTrigger("isSummoning");
            SummonSkeleton();
        } 
        else if (!isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("isAttacking");
            Attack();
        }
        if(!isStrafing || strafeTimer < 0.5f)
        {
            isStrafing = true;
            if (distanceToPlayer < 9)
            {
                movimiento = GoBack();
            }
            else if (distanceToPlayer > 12)
            {
                movimiento = Approach();
            }


            if (movimiento.magnitude > 1.0f)
            {
                movimiento.Normalize();
            }

        }
        if(strafeTimer < 1.5f)
        {
            anim.SetFloat("Speed", Math.Abs(movimiento.magnitude));
            transform.Translate(movimiento);
        }
    }
    private Vector3 Approach()
    {
        
        if (lookingLeft)
        {

            return transform.right * speed * Time.deltaTime;
        }
        else
        {
            return -transform.right * speed * Time.deltaTime;
        }
    }
    private Vector3 GoBack()
    {
        if (lookingLeft)
        {
            return -transform.right * speed * Time.deltaTime;
        }
        else
        {
            return transform.right * speed * Time.deltaTime;
        }
    }

    private void SummonSkeleton()
    {
        Vector3 spawnPosition = transform.position;

        if (lookingLeft)
        {
            spawnPosition.x += 1f;
            Instantiate(skeletonPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            spawnPosition.x -= 1f;
            Instantiate(skeletonPrefab, spawnPosition, Quaternion.identity);
        }
        
    }

    public override void Attack()
    {
        if(lookingLeft)
        {
            Instantiate(throwablePrefab, staffPositionToShoot[0].transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(throwablePrefab, staffPositionToShoot[1].transform.position, Quaternion.identity);
        }
    }

    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
        else
        {
            anim.SetTrigger("isHurt");
        }
    }

    public override void Die()
    {
        speed = 0;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        anim.SetBool("isDead",true);
        Destroy(gameObject, 4.4f);
    }
}

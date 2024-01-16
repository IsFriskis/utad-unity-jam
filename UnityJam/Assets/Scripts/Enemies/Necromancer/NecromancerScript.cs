using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerScript : BasicEnemyScript
{
    [SerializeField]
    private GameObject staffPositionToShoot;

    [SerializeField]
    private GameObject skeletonPrefab;

    [SerializeField]
    private GameObject throwablePrefab;

    [SerializeField]
    private float summonRange = 5f;

    [SerializeField]
    private float summonDelay = 8f;
    [SerializeField]
    private float attackDelay = 4f;

    private float summonTimer = 0f;
    private float attackTimer = 0f;

    private bool isSummoning = false;
    private bool isAttacking = true;

    void Update()
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
        IALogic();
    }

    public override void IALogic()
    {
        float distanceToPlayer = Vector3.Distance(playableCharacter.transform.position, transform.position);
        if (distanceToPlayer <= summonRange && !isSummoning)
        {
            isSummoning = true;
            SummonSkeleton();
        } 
        else if (!isAttacking)
        {
            isAttacking = true;
            Attack();
        }
    }

    private void SummonSkeleton()
    {
        Instantiate(skeletonPrefab, transform.position, Quaternion.identity);
    }

    public override void Attack()
    {
        Instantiate(throwablePrefab, staffPositionToShoot.transform.position, Quaternion.identity);
    }

    public override void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}

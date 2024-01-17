using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class BasicEnemyScript : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    [SerializeField]
    public int currentHealth;
    [SerializeField]
    public int attackDamage;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float detectionRange;
    [SerializeField]
    protected float deathTimer;
    [SerializeField]
    public GameObject enemyPartner;
    public bool isStunned;
    public bool isDead = false;

    protected bool lookingLeft;

    

    [SerializeField]
    public GameObject playableCharacter;

    [SerializeField]
    protected GameObject[] waypoints;

    protected int currentWaypoint = 0;

    private bool isInvulnerable = false;


    protected Animator anim;
    protected CapsuleCollider2D capCol2D;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;

    void Awake()
    {
        anim = GetComponent<Animator>();
        capCol2D = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if(lookingLeft)
        {
            spriteRenderer.flipX = false;
        } 
        else if (!lookingLeft)
        {
            spriteRenderer.flipX = true;
        }
    }

    public abstract void Attack();

    public abstract void IALogic();
    
    
    public virtual void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            anim.SetTrigger("isHurt");

            if (currentHealth <= 0)
            {   
               GetStunned();
            }
        }
    }

    protected void GetStunned()
    {
        isStunned = true;

        if (anim != null)
        {
            anim.SetBool("isStunned", true);
        }
        
        if (isStunned && enemyPartner.GetComponent<BasicEnemyScript>().isStunned)
        {
            enemyPartner.GetComponent<BasicEnemyScript>().Die();
            Die();
        }

        StartCoroutine(StunTimer());
    }

    private IEnumerator StunTimer()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(deathTimer);
        isStunned = false;
        isInvulnerable = false;

        if (anim != null)
        {
            anim.SetBool("isStunned", false);
            RegenerateHealth();
        }
    }
    protected virtual void RegenerateHealth()
    {
        currentHealth = maxHealth / 2;
    }
    public virtual void Die()
    {
        isDead = true;
        anim.SetBool("isDead",true);
        Destroy(gameObject, 10f);
    }

    protected void FollowViewPlayer()
    {
        Vector2 npcPosition = transform.position;
        Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 playerGhostPosition = GameObject.FindGameObjectWithTag("GhostPlayer").transform.position;

        float distance = Vector2.Distance(npcPosition, playerPosition);
        float distanceGhost = Vector2.Distance(npcPosition, playerGhostPosition);

        if(distance <= detectionRange)
        {
            lookingLeft = (playerPosition.x < npcPosition.x);
        }
    }
}
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
    //La mecanica de la armadura podriamos hacer que el espiritu pueda quitar la armadura y asi quita la reduccion de da�o. Pudiendo ahorrar asi ciertos enemigos
    [SerializeField]
    public int currentHealth;

    //La creacion de espiritus
    [SerializeField]
    public int attackDamage;

    [SerializeField]
    protected float velocity;
    [SerializeField]
    protected float detectionRange;
    [SerializeField]
    protected float deathTimer;
    [SerializeField]
    public GameObject enemyPartner;
    public bool isStunned;

    protected bool lookingLeft;

    [SerializeField]
    public float speed;

    [SerializeField]
    public GameObject playableCharacter;

    //Waypoint logic for patrolling
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
    void Start()
    {
       
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
    //Se ajusta la posicion del enemigo para que siga la direccion del jugador cuando entre dentro de su rang
    
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
            Debug.Log("Se murieron");
        }

        StartCoroutine(StunTimer());
        

    }

    private IEnumerator StunTimer()
    {
        isInvulnerable = true;
        Debug.Log("Estuneado");
        yield return new WaitForSeconds(deathTimer);
        Debug.Log("Normal");
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
        anim.SetBool("isDead",true);
        Destroy(gameObject, 10f);
    }

    protected void FollowViewPlayer()
    {
        Vector2 npcPosition = transform.position;
        Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 playerGhostPosition = GameObject.FindGameObjectWithTag("GhostPlayer").transform.position;
 
        float distance = Vector2.Distance(npcPosition,playerPosition);
        float distanceGhost = Vector2.Distance(npcPosition, playerGhostPosition);
 
        if(distance <= detectionRange) 
        {
            bool knightIsToTheRight = (playerPosition.x > npcPosition.x);
 
            if (knightIsToTheRight)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        if(distanceGhost <= detectionRange)
        {
 
            bool knightIsToTheRight = (playerGhostPosition.x > npcPosition.x);
 
            if (knightIsToTheRight)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
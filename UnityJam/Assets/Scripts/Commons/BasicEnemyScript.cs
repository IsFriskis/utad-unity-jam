using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicEnemyScript : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    //La mecanica de la armadura podriamos hacer que el espiritu pueda quitar la armadura y asi quita la reduccion de daño. Pudiendo ahorrar asi ciertos enemigos
    [SerializeField]
    public int currentHealth;

    //La creacion de espiritus
    [SerializeField]
    public int attackDamage;

    [SerializeField]
    public float deathTimer;
    //Si la vida de algun enemigo es igual a 0 quedara stunned y tendra un deathTimer, si ambas formas estan stunned dentro del periodo del deathTimer el enemigo morira
    public bool isStunned;

    protected bool lookingLeft;

    [SerializeField]
    public float detectionRange;

    [SerializeField]
    public float speed;

    [SerializeField]
    public GameObject playableCharacter;

    //Waypoint logic for patrolling
    [SerializeField]
    protected GameObject[] waypoints;

    protected int currentWaypoint = 0;

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
        currentHealth = maxHealth;
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

    public abstract void TakeDamage(int damage);

    protected abstract void Die();
        //Comprobar si ambas formas estan stunneadas y entran dentro del deathTimer para poder morir
        //Pondremos la animacion de morir de cada uno de los NP


    public abstract void IALogic();
    //Se ajusta la posicion del enemigo para que siga la direccion del jugador cuando entre dentro de su rango
    protected void FollowViewPlayer()
    {
        Vector2 npcPosition = transform.position;

        bool knightIsToTheRight = (playableCharacter.transform.position.x > npcPosition.x);

        if (knightIsToTheRight)
        {
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            lookingLeft = false;
        }
        else
        {
            //transform.localS1cale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);1
            lookingLeft = true;
        }
    }
}

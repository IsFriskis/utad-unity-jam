using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyScript : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    //La mecanica de la armadura podriamos hacer que el espiritu pueda quitar la armadura y asi quita la reduccion de daño. Pudiendo ahorrar asi ciertos enemigos
    //La creacion de espiritus.
    [SerializeField] 
    public int armorPoints;
    [SerializeField]
    public int dmgDealt;
    [SerializeField]
    public float deathTimer;
    //Si la vida de algun enemigo es igual a 0 quedara stunned y tendra un deathTimer, si ambas formas estan stunned dentro del periodo del deathTimer el enemigo morira
    public bool isStunned;

    protected int currentHealth;

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
        
    }

    public virtual void MeleeAttack()
    {
        //Reproducir animaciones y establecer los colliders correspondientes para que encuentre el TAG "PLAYER" y le cause daño
    }
    public virtual void RangedAttack()
    {
        //Reproducir animaciones y establecer los colliders correspondientes para que encuentre el TAG "PLAYER" y le cause daño.
        //Ademas de tener que crear e instaciar los proyectiles propios de cada clase.
    }

    public void TakeDamage(int damage)
    {
        //Introducir la lógica de que el collider de cuando el TAG "Player" entre en colision con el del TAG "Enemy" se reste la vida
        //Causada por el daño establecido por el jugador
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        //Comprobar si ambas formas estan stunneadas y entran dentro del deathTimer para poder morir
        //Pondremos la animacion de morir de cada uno de los NPC
        Destroy(gameObject);
    }

    //Se ajusta la posicion del enemigo para que siga la direccion del jugador cuando entre dentro de su rango
    protected void FollowViewPlayer()
    {
        Vector2 npcPosition = transform.position;
        Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

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
}

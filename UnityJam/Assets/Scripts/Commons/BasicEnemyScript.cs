using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BasicEnemyScript : MonoBehaviour
{
    [SerializeField]
    protected int maxHealth;
    //La mecanica de la armadura podriamos hacer que el espiritu pueda quitar la armadura y asi quita la reduccion de daño. Pudiendo ahorrar asi ciertos enemigos
    //La creacion de espiritus.
    [SerializeField]
    protected int attackDamage;
    [SerializeField]
    protected float velocity;
    [SerializeField]
    protected float detectionRange;
    [SerializeField]
    protected float deathTimer;
    //Si la vida de algun enemigo es igual a 0 quedara stunned y tendra un deathTimer, si ambas formas estan stunned dentro del periodo del deathTimer el enemigo morira
    protected bool isStunned;
    public int currentHealth;


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
        
    }
 

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    protected void GetStunned()
    {
        isStunned = true;

        if (anim != null)
        {
            anim.SetBool("IsStunned", true);
        }

        StartCoroutine(StunTimer());
    }

    private IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(deathTimer);

        isStunned = false;

        if (anim != null)
        {
            anim.SetBool("IsStunned", false);
        }
        else
        {
            RegenerateHealth();
        }
    }
    protected virtual void RegenerateHealth()
    {
        currentHealth = maxHealth / 2;
    }
    public virtual void Die()
    {
        anim.SetTrigger("isDead");
        
    }

    //Se ajusta la posicion del enemigo para que siga la direccion del jugador cuando entre dentro de su rango
    /*
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
    */
    

}

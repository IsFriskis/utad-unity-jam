using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Spirit : MonoBehaviour
{


    [SerializeField] float life;
    [SerializeField] float mana;
    [SerializeField] float sprint = 1f; //Multiplicador de la velocidad base cuando corre
    [SerializeField] float jumpForce = 5f; //Fuerza del salto
    [SerializeField] float rayDistance = 0.2f; //Distancia m�xima del suelo que habilita poder saltar
    [SerializeField] float initialSpeed = 3.0f; //Velocidad base del personaje
    private Rigidbody2D rb;
    private Animator animator;
    private bool isSprinting; //Se activa cuando esta corriendo
    bool isOnGround;   //Habilita la opcion de salto
    public LayerMask solidLayer; //Define la capa que se utilizara para saber si esta tocando un objeto solido y puede saltar
    private bool isAlive;
    public bool leftLimit = false;
    public bool rightLimit = false;
    public Transform spawnPoint;
    public GameObject bullet;
    private GameObject fireBullet;
    [SerializeField]private int bulletSpeed;
    [SerializeField] private float hurtForce = 2.0f;
    [SerializeField] private string enemyHitboxTag;
    private bool receiveDamage = false;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private bool isCoyoteTime = false;
    private float timeInCoyoteTime;
    [Header("Jump Buffer And Jump Time")]
    [SerializeField] private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.gravityScale = 0.5f;
        isAlive = true;


    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()

    {
        animator.SetFloat("Speed_Y", rb.velocity.y);
        //Detectamos si el Player esta vivo
        if (isAlive && !receiveDamage)
        {
            isOnGround = true;
            isSprinting = false;
            Vector3 movimiento = Vector3.zero;


            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (!leftLimit)
                {
                    movimiento -= transform.right;
                    animator.SetBool("Is_Moving", true);
                    RotateSpirit(false);
                }


            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!rightLimit)
                {
                    movimiento += transform.right;
                    animator.SetBool("Is_Moving", true);
                    RotateSpirit(true);
                }

            }

            //Normalizamos el vector movimiento para mantener la misma velocidad en todas direcciones
            if (movimiento.magnitude > 1.0f)
            { movimiento.Normalize(); }

            //Desplazamos el personaje a una velocidad
            float speed = initialSpeed;

            if (Input.GetKey(KeyCode.LeftControl) && (isOnGround))
            {
                speed = initialSpeed * sprint;
                isSprinting = true;
            }

            transform.Translate(movimiento * speed * Time.deltaTime);
            if (movimiento.x == 0)
            {
                animator.SetBool("Is_Moving", false);
            }

            //--------------------SALTO----------------------------------------------
            //Detección de Coyote Time
            if(!isOnGround && isCoyoteTime){
                timeInCoyoteTime += Time.deltaTime;
                if(timeInCoyoteTime > coyoteTime){
                    isCoyoteTime = false;
                }
            }
            //Jump Buffer
            JumpBufferControl();
            //Llamamos a la funci�n de salto y en el caso que este tocando el suelo le permitiremos saltar
            if(jumpBufferCounter >= 0)
            {
                Jump();
            }
            // Variable Jump
            if (Input.GetKeyUp(KeyCode.UpArrow) && !isOnGround && (rb.velocity.y > 0)){
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y/2f);
            }
            //--------------------------------------------------------------------

            //Llamamos a la funci�n de ataque
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                Fire();

            }
            if (movimiento.x > 0)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    animator.SetTrigger("Is_Rolling");

                }
            }
            //Detectamos si la vida del personaje es 0 y llamamos a la funci�n de muerte
            if (mana <= 0)
            {
                Death();
                isAlive = false;

            }

            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                Fire();
            }
        }
    }

    void FixedUpdate(){
        //Lanzamos un RayCast para detectar si el Player esta tocando el suelo
        if (Physics2D.Raycast(this.transform.position, Vector2.down, rayDistance, solidLayer))
        {
            isOnGround = true;

        }
        else
        {
            isOnGround = false;
        }
        animator.SetBool("Is_Grounded", isOnGround);
    }
    void Jump()
    {
        if (isOnGround || isCoyoteTime)
        {
            if (isSprinting){
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * sprint);
                jumpBufferCounter = 0;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpBufferCounter = 0;
            }

        }
    }

    void JumpBufferControl(){
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            jumpBufferCounter = jumpBufferTime;
        }
        else{
            jumpBufferCounter -= Time.deltaTime;
        }
        
    }

    void Death()
    {
        animator.SetTrigger("Is_Death");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si estamos colisionando con un objeto que tiene el tag especificado
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D colisionado = collision.collider;
            if (colisionado.CompareTag("rightLimit"))
            {
                // Establecer la bandera de colisi�n en true
                rightLimit = true;

            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D colisionado = collision.collider;
            if (colisionado.CompareTag("leftLimit"))
            {
                // Establecer la bandera de colisi�n en true
                leftLimit = true;
            }
        }

        if (collision.collider.CompareTag (enemyHitboxTag) && !receiveDamage) 
        {
            receiveDamage = true;
            animator.SetTrigger("Damage");
            if(collision.gameObject.transform.position.x > transform.position.x){ //Si enemigo esta a la derecha
                rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
            }
            else{
                rb.velocity = new Vector2(hurtForce, rb.velocity.y);
            }
            
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Verificar si estamos colisionando con un objeto que tiene el tag especificado
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D colisionado = collision.collider;
            if (colisionado.CompareTag("rightLimit"))
            {
                // Establecer la bandera de colisi�n en true
                rightLimit = false;

            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D colisionado = collision.collider;
            if (colisionado.CompareTag("leftLimit"))
            {
                // Establecer la bandera de colisi�n en true
                leftLimit = false;

            }
        }
    }

    public void RotateSpirit(bool dir)
    {
        if (dir)
        {
            gameObject.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);

        }
        else
        {
            gameObject.transform.localScale = new Vector3(-2.0f, 2.0f, 2.0f);
        }
    }

    public void Fire()
    {
        GameObject fireBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

        if (transform.localScale.x == 1.5)
        {
            fireBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * bulletSpeed, ForceMode2D.Impulse);
        }

        else
        {
            fireBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * bulletSpeed, ForceMode2D.Impulse);
        }
    
    }

    public void FinishHitAnimation()
    {
        receiveDamage = false;
    }

}



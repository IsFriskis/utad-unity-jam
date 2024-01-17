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
    [SerializeField] float rayDistance = 0.2f; //Distancia máxima del suelo que habilita poder saltar
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
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    private void Update()

    {
        //Detectamos si el Player esta vivo
        if (isAlive)
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

            //Lanzamos un RayCast para detectar si el Player esta tocando el suelo
            if (Physics2D.Raycast(this.transform.position, Vector2.down, rayDistance, solidLayer))
            {
                isOnGround = true;

            }
            else
            {
                isOnGround = false;
                animator.SetTrigger("Is_Jumping");
            }

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


            //Llamamos a la función de salto y en el caso que este tocando el suelo le permitiremos saltar
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }

            //Llamamos a la función de ataque
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
            //Detectamos si la vida del personaje es 0 y llamamos a la función de muerte
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

    void Jump()
    {

        if (isOnGround)
        {
            if (isSprinting)
                rb.AddForce(Vector2.up * (jumpForce * sprint), ForceMode2D.Impulse);
            else
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

        }
    }
    //void Attack()
    //{
    //    animator.SetTrigger("Is_OnAttack");
    //}

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
                // Establecer la bandera de colisión en true
                rightLimit = true;

            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D colisionado = collision.collider;
            if (colisionado.CompareTag("leftLimit"))
            {
                // Establecer la bandera de colisión en true
                leftLimit = true;


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
                // Establecer la bandera de colisión en true
                rightLimit = false;

            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D colisionado = collision.collider;
            if (colisionado.CompareTag("leftLimit"))
            {
                // Establecer la bandera de colisión en true
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

}



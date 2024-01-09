using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    [SerializeField] float life;
    [SerializeField] float mana;
    [SerializeField] float sprint = 1f; //Multiplicador de la velocidad base cuando corre
    [SerializeField] float jumpForce = 5f; //Fuerza del salto
    [SerializeField] float rayDistance = 0.2f; //Distancia máxima del suelo que habilita poder saltar
    [SerializeField] float initialSpeed = 3.0f; //Velocidad base del personaje
    private Rigidbody2D rb;
    private Animator animator;
    private bool canSprint; //Habilita la posibilidad de correr
    bool isOnGround;   //Habilita la opcion de salto
    public LayerMask groundLayer; //Define la capa que se utilizara para saber si esta o no en el suelo
    private bool isAlive;
  

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isAlive = true;
     

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Detectamos si el Player esta vivo
        if (isAlive)
        {
            Vector3 movimiento = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
            {
                movimiento -= transform.right;
                animator.SetBool("Is_Moving", true);
            }
            if (Input.GetKey(KeyCode.D))
            {
                movimiento += transform.right;
                animator.SetBool("Is_Moving", true);
            }
            //Normallizamos el vector movimiento para mantener la misma velocidad en todas direcciones
            if (movimiento.magnitude > 1.0f)
            { movimiento.Normalize(); }


            //Desplazamos el personaje a una velocidad
            float speed = initialSpeed;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                speed = initialSpeed * sprint;

            }

            transform.Translate(movimiento * speed * Time.deltaTime);
            if (movimiento.x == 0)
            {
                animator.SetBool("Is_Moving", false);
            }

            //Lanzamos un RayCast para detectar si el Player esta tocando el suelo
            if (Physics2D.Raycast(this.transform.position, Vector2.down, rayDistance, groundLayer))
            {
                isOnGround = true;

            }
            else
            {
                isOnGround = false;
                animator.SetTrigger("Is_Jumping");
            }
            //Llamamos a la función de salto y en el caso que este tocando el suelo le permitiremos saltar
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }

            //Llamamos a la función de ataque
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();

            }
            if (movimiento.x > 0)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    animator.SetTrigger("Is_Rolling");
                }
            }
            //Detectamos si la vida del personaje es 0 y llamamos a la función de muerte
            if (life <= 0)
            {
                Death();
                isAlive = false;
                
            }
        }
    }
 
    void Jump()
    {
       
        if (isOnGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
        }
    }
    void Attack()
    {
        animator.SetTrigger("Is_OnAttack");
    }

    void Death()
    {
        animator.SetTrigger("Is_Death");
    }

}

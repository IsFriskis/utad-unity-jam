using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    [SerializeField] float life;
    [SerializeField] float mana;
    [SerializeField] float sprint = 1f; //Multiplicador de la velocidad base cuando corre
    [SerializeField] float jumpForce = 5f; //Fuerza del salto
    [SerializeField] float rayDistance = 0.2f; //Distancia máxima del suelo que habilita poder saltar
    [SerializeField] float initialSpeed = 3.0f; //Velocidad base del personaje
    [SerializeField] private GameObject Spirit;
    private Rigidbody2D rb;
    private bool dir;
    private Animator animator;
    private bool isSprinting; //Esta corriendo
    bool isOnGround;   //Habilita la opcion de salto
    public LayerMask solidLayer; //Define la capa que se utilizara para saber si esta tocando un objeto solido y puede saltar
    public LayerMask playerLayer;
    private bool isAlive;
    private bool reciveDamage = false;





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
    void Update()
    {
        print(reciveDamage);
        animator.SetFloat("Speed_Y", rb.velocity.y);
        //Detectamos si el Player esta vivo
        if (isAlive && !reciveDamage)
        {

            isOnGround = true;
            isSprinting = false;
            Vector3 movimiento = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
            {
                if (!Spirit.GetComponent<Spirit>().rightLimit)
                {
                    movimiento -= transform.right;
                    animator.SetBool("Is_Moving", true);
                    RotatePlayer(false);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (!Spirit.GetComponent<Spirit>().leftLimit)
                {
                    movimiento += transform.right;
                    animator.SetBool("Is_Moving", true);
                    RotatePlayer(true);
                }
            }
            //Normallizamos el vector movimiento para mantener la misma velocidad en todas direcciones
            if (movimiento.magnitude > 1.0f)
            { movimiento.Normalize(); }


            //Lanzamos un RayCast para detectar si el Player esta tocando el suelo
            if (Physics2D.Raycast(this.transform.position, Vector2.down, rayDistance, solidLayer))
            {
                isOnGround = true;
                animator.SetBool("Is_Grounded", true);
            }
            else
            {
                isOnGround = false;
                animator.SetBool("Is_Grounded",false);
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
                    gameObject.layer = 12;
                    //Cambiar el layer del player a rodando
                    
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
 
    public void Jump()
    {

        if (isOnGround)
        {
            if (isSprinting)
            {
                //rb.AddForce(Vector2.up * (jumpForce * sprint), ForceMode2D.Impulse);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * sprint);
               // animator.SetFloat("Speed_Y", rb.velocity.y);
                
            }
            else
            {
                //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                //animator.SetFloat("Speed_Y", rb.velocity.y);
                
            }
            
        }
    }
    public void Attack()
    {
        animator.SetTrigger("Is_OnAttack");
    }

    public void Death()
    {
        animator.SetTrigger("Is_Death");
    }

    public void EndRoll()
    {
        gameObject.layer = 3;
    }

    public void RotatePlayer(bool dir)
    {
        if (dir)        {
            gameObject.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            

        }
        else 
        {
            gameObject.transform.localScale = new Vector3(-2.0f, 2.0f, 2.0f);
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.collider.CompareTag ("Sword") ) 
        {
            animator.SetTrigger("Damage");
            reciveDamage = true;
        }
    }
    public void FinishHitAnimation()
    {
        reciveDamage = false;
      
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 1f;
    public float rollForce = 15f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 interactiveObject;
    private bool isGrounded;
    private int comboCount = 0;
    private float lastAttackTime;
    public float comboWindow = 4f; // Ventana de tiempo para el combo en segundos

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Comprobar si el jugador está en el suelo
        isGrounded = IsGrounded();

        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = movement;

        // Cambiar la dirección del sprite
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if(rb.velocity == Vector2.zero)
        {
            anim.SetBool("Is_Moving", false);
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("Is_Moving",true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("Is_Moving", true);
        }
        // Saltar
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("Is_Jumping",true);
        }

        // Rodar
        if (Input.GetKeyDown(KeyCode.S) && Mathf.Abs(rb.velocity.x) > 0.1f && isGrounded)
        {
            rb.AddForce(new Vector2((transform.localScale.x > 0 ? 1 : -1) * rollForce, 0f), ForceMode2D.Impulse);
            anim.SetTrigger("Is_Rolling");
        }

        // Atacar con combos
        //La idea principal es que el jugador tenga que presionar 3 veces el boton espacio para realizar el combo completo de ataque que inflingira mucho daño a los enemigos
        //Para ello deberiamos realizar un contador que lleve la cuenta por que combo va en un periodo de tiempo, si no se realiza en el tiempo especificado el combo se
        //Cortara y volvera a resetearse el combo a la animacion principal.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastAttackTime <= comboWindow)
            {
                comboCount++;

                anim.SetTrigger("Is_Attacking" + comboCount);

                if(comboCount > 3)
                {
                    comboCount = 1;
                }


                lastAttackTime = Time.time;
            }
            else
            {
                comboCount = 1;
                anim.SetTrigger("Is_Attacking1");
                lastAttackTime = Time.time;
            }
        }

        // Interactuar con objetos
       
        InteractWithObjects();
        
    }
    private bool IsGrounded()
    {
        float raycastLength = 1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, groundLayer);

        return hit.collider != null;
    }
    private void InteractWithObjects()
    {
        //Encontramos el objeto cercano para poder interactuar con el 
        interactiveObject = GameObject.FindGameObjectWithTag("Prop").transform.position;
        //Tenemos que introducir la lógica aqui para recoger consumibles con los layers o con las TAGS
        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }
    }
}

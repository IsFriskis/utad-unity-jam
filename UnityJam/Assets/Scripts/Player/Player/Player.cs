using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] public float hp, maxHP;
    [SerializeField] float sprint = 1f; //Multiplicador de la velocidad base cuando corre
    [SerializeField] float jumpForce = 5f; //Fuerza del salto
    [SerializeField] float rayDistance = 0.2f; //Distancia m�xima del suelo que habilita poder saltar
    [SerializeField] float initialSpeed = 3.0f; //Velocidad base del personaje
    [SerializeField] private GameObject Spirit;
    [SerializeField] private float hurtForce = 2.0f;
    [SerializeField] private string enemyHitboxTag;
    [SerializeField] ParticleSystem particles;
    private Rigidbody2D rb;
    private bool dir;
    private Animator animator;
    private bool isSprinting; //Esta corriendo
    private bool isRolling; //Esta rodando
    bool isOnGround;   //Habilita la opcion de salto
    public LayerMask solidLayer; //Define la capa que se utilizara para saber si esta tocando un objeto solido y puede saltar
    public LayerMask playerLayer;
    public bool defeatedByFinalBoss=false;  //Para saber si murio en el jefe final
    private bool isAlive;
    private bool receiveDamage = false;
    [SerializeField] public HUDScript hud;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSound, jumpSound;
    [SerializeField] private AudioClip[] attacksSounds, footstepsSounds ;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private bool isCoyoteTime = false;
    private float timeInCoyoteTime;
    [Header("Jump Buffer And Jump Time")]
    [SerializeField] private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    public int attackDamage;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isAlive = true;
        hud.maxHealth = maxHP;
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //GameObject.DontDestroyOnLoad(this.gameObject);
        attackDamage = 20;
        attackRange = 0.35f;
        //Para que no se vea el ratón
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed_Y", rb.velocity.y);
        //Detectamos si el Player esta vivo
        if (isAlive && !receiveDamage)
        {
            isSprinting = false;
            Vector3 movimiento = Vector3.zero;
            print("Hey: "+ Input.GetAxis("Horizontal"));
            if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < 0.0f) //Move Left
            {
                
                if (!Spirit.GetComponent<Spirit>().rightLimit)
                {
                    movimiento -= transform.right;
                    animator.SetBool("Is_Moving", true);
                    RotatePlayer(false);
                }
            }
            if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0.0f) //Move Right
            {
                if (!Spirit.GetComponent<Spirit>().leftLimit )
                {
                    movimiento += transform.right;
                    animator.SetBool("Is_Moving", true);
                    RotatePlayer(true);
                }
            }
            //Normallizamos el vector movimiento para mantener la misma velocidad en todas direcciones
            if (movimiento.magnitude > 1.0f)
            { movimiento.Normalize(); }

            //Desplazamos el personaje a una velocidad
            float speed = initialSpeed;

            if (Input.GetKey(KeyCode.LeftShift) && (isOnGround)) 
            {
                speed = initialSpeed * sprint;
                PlayParticles();
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
            if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyDown(KeyCode.Joystick1Button0))&& !isOnGround && (rb.velocity.y > 0)){
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y/2f);
            }
            //--------------------------------------------------------------------
            //Llamamos a la funci�n de ataque
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                Attack();

            }
            //if (movimiento.x > 0)
            //{
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Joystick1Button1)) //ROLL
            {
                animator.SetTrigger("Is_Rolling");
                PlayParticles();
                gameObject.layer = 12;
                isRolling = true;
                //Cambiar el layer del player a rodando
            }
            //}
            //Detectamos si la vida del personaje es 0 y llamamos a la funci�n de muerte
            if (hp <= 0)
            {
                Death();
                isAlive = false;   
            }
        }
    }

    void FixedUpdate(){
        //Lanzamos un RayCast para detectar si el Player esta tocando el suelo
        if (Physics2D.Raycast(this.transform.position, Vector2.down, rayDistance, solidLayer))
        {
            isOnGround = true;
            isCoyoteTime = true;
            timeInCoyoteTime = 0;
        }
        else
        {
            isOnGround = false;
        }
        animator.SetBool("Is_Grounded", isOnGround);
    }
 
    public void Jump()
    {
        if (isOnGround || isCoyoteTime)
        {
            audioSource.PlayOneShot(jumpSound);
            PlayParticles();
            if (isSprinting)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * 1.1f);
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
        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && !isRolling){
            jumpBufferCounter = jumpBufferTime;
        }
        else{
            jumpBufferCounter -= Time.deltaTime;
        }
        
    }
    public void Attack()
    {
        animator.SetTrigger("Is_OnAttack");
        audioSource.PlayOneShot(attacksSounds[Random.Range(0, attacksSounds.Length)]);
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemy)
        {
            if(enemy.GetComponent<BasicEnemyScript>() != null && enemy.CompareTag("Physical"))
            {
                enemy.GetComponent<BasicEnemyScript>().TakeDamage(attackDamage);
                Debug.Log("Ha golpeado");
            }
            else if (enemy.GetComponentInChildren<FlyingEyeScript>() != null)
            {
                enemy.GetComponentInChildren<FlyingEyeScript>().TakeDamage(attackDamage);
                Debug.Log("Ha golpeado a un flyingObject");
            }
        }
    }

    public void Death()
    {
        animator.SetTrigger("Is_Death");
        hud.PlayGameOver();
    }

    public void EndRoll()
    {
        gameObject.layer = 3;
        isRolling = false;
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
   /*  private void OnCollisionEnter2D(Collision2D collision)
    {
        //*************DAÑO***********************
        if (collision.collider.CompareTag (enemyHitboxTag) && !receiveDamage) //Si es ataque enemigo y no esta en animación de dolor
        {
            receiveDamage = true;
            animator.SetTrigger("Damage");
            //hp -= collision.collider.gameObject.GetComponent<BasicEnemyScript>().attackDamage;
            hud.ChangeHealth(hp);
            if(collision.gameObject.transform.position.x > transform.position.x){ //Si enemigo esta a la derecha
                rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
            }
            else{
                rb.velocity = new Vector2(hurtForce, rb.velocity.y);
            }
            
        }
    } */
    public void FinishHitAnimation()
    {
        receiveDamage = false;
    }

    private void PlayParticles(){
        particles.Play();
    }

    public void TakeDamage(float damage, float positionEnemy, bool finalBoss = false){
        if(!receiveDamage){
            defeatedByFinalBoss = finalBoss; //Falta crear un script que nunca muera
            receiveDamage = true;
            animator.SetTrigger("Damage");
            audioSource.PlayOneShot(hurtSound);
            hp -= damage;
            hud.ChangeHealth(hp);
            if(positionEnemy > transform.position.x){ //Si enemigo esta a la derecha
                rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
            }
            else{
                rb.velocity = new Vector2(hurtForce, rb.velocity.y);
            }
        }
    }

    public void PlayRandomFootStep(){
        audioSource.PlayOneShot(footstepsSounds[Random.Range(0, footstepsSounds.Length)]);
    }
}

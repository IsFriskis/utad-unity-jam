using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public Transform player;
    [Header("**Changable Parameters**")]
    public float walkSpeed = 2.0f;
    [SerializeField] float chaseDistance;
    [SerializeField] float attackDistance;
    [SerializeField] float attackDelay;
  
    private bool isAttacking = false;

    private float direction = -1.0f;

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer sprite;
    private Animator animator;

    [Header("**Wall Detection**")]
    [SerializeField] Transform wallDetection;
    [SerializeField] LayerMask wallLayerMask;
    [SerializeField] float distance;
    
    Vector3 walkAmount;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            return;
        } 

        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        //Attack Player
        if (distanceToPlayer <= attackDistance)
        {
            if (!isAttacking)
            {
                animator.SetTrigger("Attack");
                isAttacking = true;
                Invoke("TimeAttackDelay", attackDelay);
            }
        }
        //Movement
        else if(!isAttacking)
        {
            //Chase Player
            if (distanceToPlayer <= chaseDistance)
            {
                if (player.position.x < transform.position.x)
                {
                    direction = -1;
                    sprite.flipX = false;
                }
                else
                {
                    direction = 1;
                    sprite.flipX = true;
                }
                transform.position = Vector2.MoveTowards(transform.position, player.position, walkSpeed * Time.deltaTime);
                animator.SetFloat("Speed", walkSpeed);
            }
            //Walk around
            else
            {
                walkAmount.x = direction * walkSpeed * Time.deltaTime;
                transform.Translate(walkAmount);
                animator.SetFloat("Speed", walkSpeed);
            }
        }
    }
    private void TimeAttackDelay()
    {
        isAttacking = false;
    }
    private void FixedUpdate()
    {
        if (direction == 1.0f)
        {
            Vector3 origin = wallDetection.position;
            Vector3 dir = Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(origin, dir, distance, wallLayerMask);
            if (hit.collider != null)
            {
                direction = -1;
                sprite.flipX = false;
                Debug.DrawLine(origin, hit.point, Color.red);
            }
            else Debug.DrawLine(origin, origin + dir * distance, Color.white, 0.01f);
        }
        else
        {
            Vector3 origin = wallDetection.position;
            Vector3 dir = -Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(origin, dir, distance, wallLayerMask);
            if (hit.collider != null)
            {
                direction = 1;
                sprite.flipX = true;
                Debug.DrawLine(origin, hit.point, Color.red);
            }
            else Debug.DrawLine(origin, origin + dir * distance, Color.white, 0.01f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(chaseDistance,0,0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-chaseDistance, 0, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(attackDistance, 0, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-attackDistance, 0, 0));
    }
}

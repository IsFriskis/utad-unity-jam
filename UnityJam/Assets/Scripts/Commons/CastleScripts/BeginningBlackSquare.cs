using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningBlackSquare : MonoBehaviour
{
    Animator animator;
    BoxCollider2D boxCollider2D;
    void Awake(){
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();

    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player") || collider.CompareTag("PlayerSpirit")){
            animator.SetTrigger("Dissapear");
            boxCollider2D.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour
{
    private Animator animator;
    public AnimationClip anim;
    float waitTime;
    private void Awake(){
        animator = GetComponent<Animator>();
    }
    void Start () {
        waitTime = anim.length + 6f;
        InvokeRepeating ("PlayAnim", 6f, waitTime);
    }
    
    void PlayAnim () {
        animator.SetTrigger("Beat");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritColumn : MonoBehaviour
{
    //Script used to detect when all enemies, of a certain zone, 
    //have been defeated. When enemies == 0, the black squares
    //dissapear and the column rises
    [SerializeField] GameObject[] enemies;
    bool isInAnimation = false; //Animation of column going up
    [SerializeField]Animator animator;

    void Update()
    {
        if(enemies.Length == 0 && !isInAnimation){
            animator.SetTrigger("RiseColumn");
        }
    }

    //Destroy gameobject when animation is finished
    void EndColumn(){
        Destroy(this.gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpiritColumn : MonoBehaviour
{
    //Script used to detect when all enemies, of a certain zone, 
    //have been defeated. When enemies == 0, the black squares
    //dissapear and the column rises
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject canvasCastleText;
    bool isInAnimation = false; //Animation of column going up
    private AudioSource audioSource;
    [SerializeField] AudioClip nopeSound;
    [SerializeField]Animator animator;

    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(enemies.Length == 0 && !isInAnimation){
            animator.SetTrigger("RiseColumn");
        }
    }

    //Destroy gameobject when animation is finished
    void EndColumn(){
        canvasCastleText.SetActive(false);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player") || collider.CompareTag("Spirit")){
            if (enemies.Length != 0){
                canvasCastleText.SetActive(true);
                audioSource.PlayOneShot(nopeSound);
            }
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("Player") || collider.CompareTag("Spirit")){
            if(canvasCastleText.activeSelf){
                canvasCastleText.SetActive(false);
            }
        }
    }

}

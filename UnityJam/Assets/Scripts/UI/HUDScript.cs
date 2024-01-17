using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDScript : MonoBehaviour
{
    public float maxHealth, maxMana;
    [SerializeField] Image heartImage, spiritImage;
    [SerializeField] Color normalColor;
    [SerializeField] Animator heartAnimator1,heartAnimator2,heartAnimator3;
    [SerializeField] AudioClip gameOverSound, heartBeatSound;
    private Animator animator;
    private Image hudImage;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        hudImage = GetComponent<Image>();
    }

    public void ChangeHealth(float value){
        heartImage.fillAmount = value / maxHealth;
        if (heartImage.fillAmount <= 0.1){ //******Low HP***
            GetComponentInParent<AudioSource>().PlayOneShot(heartBeatSound);
            animator.SetBool("LowHP",true);
            heartAnimator1.SetBool("LowHP",true);
            heartAnimator2.SetBool("LowHP",true);
            heartAnimator3.SetBool("LowHP",true);
        }
        else{                               //***Good HP***
            if(GetComponentInParent<AudioSource>().isPlaying){
                GetComponentInParent<AudioSource>().Stop();
            }
            animator.SetBool("LowHP",false);
            heartAnimator1.SetBool("LowHP",false);
            heartAnimator2.SetBool("LowHP",false);
            heartAnimator3.SetBool("LowHP",false);
            hudImage.color = normalColor;
        }
    }
    public void ChangeMana(float value){
        spiritImage.fillAmount = value / maxMana;
    }

    public void PlayGameOver(){
        animator.Play("GameOver");
        GetComponentInParent<AudioSource>().PlayOneShot(gameOverSound);
    }
    private void GameOver(){  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //****TESTING******************************************
    //float health = 100;
    //float mana = 100;
    
    /* void Update(){
        if(Input.GetKeyDown(KeyCode.L)){
            print("Heal Up");
            health += 5;
            ChangeHealth(health);
            mana += 5;
            ChangeMana(mana);
        }
        else if(Input.GetKeyDown(KeyCode.Minus)){
            print("Heal Down");
            health -= 5;
            ChangeHealth(health);
            mana -= 5;
            ChangeMana(mana);
        }
    } */
    //***********************************************************
}

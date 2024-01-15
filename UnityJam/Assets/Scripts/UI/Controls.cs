using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public GameObject pauseMenu, controlsMenu;
    [SerializeField] ButtonBehaviour buttonBehaviour;
    [Header("Audio Clips")]
    AudioSource audioSource;
    [SerializeField] AudioClip closeMenuSound, openMenuSound;
    [SerializeField] Button firstSelectedButton;
    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        audioSource.PlayOneShot(openMenuSound);
        firstSelectedButton.Select();
    }

    public void ReturnToPauseMenu(){
        audioSource.PlayOneShot(closeMenuSound);
        controlsMenu.SetActive(false);
        if(pauseMenu != null){
            pauseMenu.SetActive(true);
           buttonBehaviour.firstSelectedButton.Select();
        }
    }
}

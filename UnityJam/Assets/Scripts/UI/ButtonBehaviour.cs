using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//****************************************
// NOTA MENTAL- El Canvas es "Screen Space - Camera",
// para que se vea el Shader del Espiritu
// ya que ShaderGraph no es compatible con "Screen Space - Overlay"
//****************************************

public class ButtonBehaviour : MonoBehaviour
{
    private bool isGamePaused = false;
    public Animator animator;
    public Sprite normalImage, hoveredImage;
    public GameObject pauseMenu, optionsMenu, controlsMenu;
    public Button firstSelectedButton;
    
    [Header("Audio Clips")]
    AudioSource audioSource;
    [SerializeField] AudioClip hoverSound, clickSound, closeMenuSound, openMenuSound;
    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerClick(int action)
    {
        audioSource.PlayOneShot(clickSound);
        switch(action){
            case 0: //RESUME
                Resume();
            break;
            case 1: //OPTIONS MENU
                pauseMenu.SetActive(false);
                optionsMenu.SetActive(true);
            break;
            case 2: //CONTROLS MENU
                pauseMenu.SetActive(false);
                controlsMenu.SetActive(true);
            break;
            case 3: //EXIT
                Application.Quit();                             //In Game
                UnityEditor.EditorApplication.isPlaying = false; //In Editor
            break;
        }
    }

    public void OnPointerEnter(Image buttonImage)
    {
        buttonImage.sprite = hoveredImage;
        audioSource.PlayOneShot(hoverSound);
    }

    public void OnPointerExit(Image buttonImage)
    {
        buttonImage.sprite = normalImage;
    }
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7)){
            if(!isGamePaused){
                firstSelectedButton.Select();
                animator.Play("OpenMenu");
                audioSource.PlayOneShot(openMenuSound);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isGamePaused = true; 
            }
            else{
                Resume();
            }
        }
    }
    private void Resume(){
        animator.Play("CloseMenu"); 
        audioSource.PlayOneShot(closeMenuSound);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isGamePaused = false;
    }
}

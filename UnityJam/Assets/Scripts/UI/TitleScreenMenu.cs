using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenMenu : MonoBehaviour
{
    public Sprite normalImage, hoveredImage;
    public GameObject optionsMenu, controlsMenu;
    public Button firstSelectedButton;
    [Header("Audio Clips")]
    AudioSource audioSource;
    [SerializeField] AudioClip hoverSound, clickSound, closeMenuSound, openMenuSound, hornSound;

    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }
    void Start(){
        firstSelectedButton.Select();
    }

    public void OnPointerClick(int action)
    {
        switch(action){
            case 0: //PLAY
                StartCoroutine(Play());
            break;
            case 1: //OPTIONS MENU
                audioSource.PlayOneShot(clickSound);
                optionsMenu.SetActive(true);
            break;
            case 2: //CONTROLS MENU
                audioSource.PlayOneShot(clickSound);
                controlsMenu.SetActive(true);
            break;
            case 3: //EXIT
                Application.Quit();                             //In Game
                //UnityEditor.EditorApplication.isPlaying = false; //In Editor
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

    IEnumerator Play(){
        audioSource.PlayOneShot(hornSound);
        yield return new WaitWhile (()=> audioSource.isPlaying);
        SceneManager.LoadScene("Lore");
    }
}

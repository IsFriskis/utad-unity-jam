using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenMenu : MonoBehaviour
{
    public Sprite normalImage, hoveredImage;
    public GameObject optionsMenu, controlsMenu;

    public void OnPointerClick(int action)
    {
        switch(action){
            case 0: //PLAY
                SceneManager.LoadScene("TitleScreen");
            break;
            case 1: //OPTIONS MENU
                //pauseMenu.SetActive(false);
                optionsMenu.SetActive(true);
            break;
            case 2: //CONTROLS MENU
                //pauseMenu.SetActive(false);
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
    }

    public void OnPointerExit(Image buttonImage)
    {
        buttonImage.sprite = normalImage;
    }
}

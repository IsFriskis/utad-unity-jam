using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject pauseMenu, controlsMenu;
    void Start()
    {
        
    }

    public void ReturnToPauseMenu(){
        controlsMenu.SetActive(false);
        if(pauseMenu != null){
            pauseMenu.SetActive(true);
        }
    }
}

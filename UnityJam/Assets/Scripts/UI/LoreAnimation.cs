using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoreAnimation : MonoBehaviour
{
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("Village");
    }
}

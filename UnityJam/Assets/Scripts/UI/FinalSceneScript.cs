using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneScript : MonoBehaviour
{
    AudioSource audioSource;

    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PlaySound(){
        audioSource.Play();
    }
}

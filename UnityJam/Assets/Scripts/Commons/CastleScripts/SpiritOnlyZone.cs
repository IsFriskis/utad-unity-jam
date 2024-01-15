using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritOnlyZone : MonoBehaviour
{
    [SerializeField] public GameObject tilemapSpiritOnly;
    [HideInInspector]public bool inZone = false, isEntering = false;
    [Header("Layer of portals")]
    public int layer2;
    void Awake(){
    
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("PlayerSpirit")){
            if(isEntering){
                inZone = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("PlayerSpirit")){
            inZone = false;
            isEntering = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]SpiritOnlyZone spiritOnlyZone;
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("PlayerSpirit")){
            spiritOnlyZone.tilemapSpiritOnly.SetActive(false);
            spiritOnlyZone.isEntering = true;
            int layer1 = collider.gameObject.layer;
            Physics2D.IgnoreLayerCollision(layer1, spiritOnlyZone.layer2, true);
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("PlayerSpirit") && !spiritOnlyZone.inZone){
            spiritOnlyZone.tilemapSpiritOnly.SetActive(true);
            int layer1 = collider.gameObject.layer;
            Physics2D.IgnoreLayerCollision(layer1, spiritOnlyZone.layer2, false);
        }
    }
}

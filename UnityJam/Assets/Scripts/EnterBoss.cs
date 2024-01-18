using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBoss : MonoBehaviour
{
    [SerializeField] GameObject player, playerSpirit;
    [SerializeField] Transform playerPos, playerSpiritPos;
    [SerializeField] GameObject colliderBoss;
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player") || collider.CompareTag("PlayerSpirit")){
            player.transform.position = playerPos.position;
            playerSpirit.transform.position = playerSpiritPos.position;
            colliderBoss.SetActive(true);
            Destroy(this.gameObject);
        }
    }

}

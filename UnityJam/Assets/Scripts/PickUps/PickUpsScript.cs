using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpsScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().hp += 30;
            Destroy(gameObject);
        }
        else if(collision.tag == "PlayerSpirit")
        {
            collision.gameObject.GetComponent<Spirit>().mana += 30;
            Destroy(gameObject);
        }
    }
}

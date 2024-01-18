using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpsScript : MonoBehaviour
{
    [SerializeField]
    protected bool isApple = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && isApple)
        {
            collision.gameObject.GetComponent<Player>().hp += 30;
            collision.gameObject.GetComponent<Player>().hud.ChangeHealth(collision.gameObject.GetComponent<Player>().hp);
            Destroy(gameObject);
        }
        else if(collision.tag == "PlayerSpirit" && !isApple)
        {
            collision.gameObject.GetComponent<Spirit>().mana += 30;
            collision.gameObject.GetComponent<Spirit>().hud.ChangeMana(collision.gameObject.GetComponent<Spirit>().mana);
            Destroy(gameObject);
        }
    }
}

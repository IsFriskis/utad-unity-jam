using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiritBullet : MonoBehaviour
{
    Rigidbody2D rb;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {

        

    }
    void Update(){
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            if(collision.collider.gameObject.GetComponent<BasicEnemyScript>() && collision.collider.gameObject.CompareTag("Ghost"))
            {
                collision.gameObject.GetComponent<BasicEnemyScript>().TakeDamage(40);
            }
            Destroy(gameObject);
        }
    }

}

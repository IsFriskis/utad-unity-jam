using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerProyectile : MonoBehaviour
{
    private GameObject playableCharacter;

    private float proyectileSpeed = 5f;

    void Awake()
    {
        playableCharacter = GameObject.Find("Player");     
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = playableCharacter.transform.position;
        Vector2 staffPos = transform.position;

        Vector2 direction = playerPos - staffPos;

        GetComponent<Rigidbody2D>().velocity = direction.normalized * proyectileSpeed;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(gameObject);
            
        }
    }
}


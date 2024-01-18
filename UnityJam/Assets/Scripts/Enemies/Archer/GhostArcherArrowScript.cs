using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostArcherArrowScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("PlayerSpirit");
        Vector3 playerTransform = player.transform.position;
        playerTransform.y += 0.4f;

        Vector3 direction = playerTransform - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotation + 180);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerSpirit"))
        {
            Debug.Log("Ha golpeado al jugador fantasmal");
            //collision.gameObject.GetComponent<Spirit>().mana -= 15;
            collision.gameObject.GetComponent<Spirit>().TakeDamage(15, transform.position.x);
            Destroy(gameObject);
        }
    }
}

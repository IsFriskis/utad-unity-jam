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
        player = GameObject.FindGameObjectWithTag("GhostPlayer");
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
        if (collision.gameObject.CompareTag("GhostPlayer"))
        {
            Debug.Log("Ha golpeado al jugador fantasmal");
            collision.gameObject.GetComponent<Spirit>();
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherArrowScript : MonoBehaviour
{
    private GameObject player;
    private GameObject ghostPlayer;
    private Rigidbody  rb;
    public float force;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        ghostPlayer = GameObject.FindGameObjectWithTag("GhostPlayer");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x,direction.y).normalized * force;
    }

    void Update()
    {
        
    }
}

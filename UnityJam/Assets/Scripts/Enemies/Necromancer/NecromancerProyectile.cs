using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Assets.Scripts.Enemies.Necromancer
{
    public class NecromancerProyectile : MonoBehaviour
    {
        private GameObject playableCharacter;

        private float proyectileSpeed = 7f;

        private Animator anim;
        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            playableCharacter = GameObject.Find("Player");
            anim = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {

            Vector2 playerPos = playableCharacter.transform.position;
            Vector2 staffPos = transform.position;

            Vector2 direction = playerPos - staffPos;

            GetComponent<Rigidbody2D>().velocity = direction.normalized * proyectileSpeed;
            GetComponent<Rigidbody2D>().MoveRotation(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                //DAñito
                //playableCharacter.GetComponent<Player>().;
            }
                proyectileSpeed = 0;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().freezeRotation = true;
                anim.SetBool("Hit", true);
                Destroy(gameObject, .8f);
        }
    }
}

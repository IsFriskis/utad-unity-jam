using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Dummy : MonoBehaviour
{
    [SerializeField] float life;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            life--;
            if (life <= 0)
            { Destroy(gameObject); }
            print(life);
        }
    }
}

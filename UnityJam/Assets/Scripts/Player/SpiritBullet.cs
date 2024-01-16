using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiritBullet : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {

        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision!= null)
        {
            Destroy(gameObject);
        }
    }

}

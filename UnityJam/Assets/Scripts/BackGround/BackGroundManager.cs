using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    // Referencias a los bloques y sus colliders
    public GameObject bloque1;
    public GameObject bloque2;
    public GameObject bloque3;

    // Variable para rastrear el bloque actual
    private GameObject bloqueActual;

    // Start is called before the first frame update
    void Start()
    {
        bloqueActual = bloque1;
        
    }

    void OnTriggerEnter2D(Collider2D ultimoTrigger)
    {
        //Checo si cualquiera de los dos jugadores pasa por el trigger
        if(ultimoTrigger.CompareTag("Player") || ultimoTrigger.CompareTag("Spirit"))
        {
            if(ultimoTrigger.gameObject == bloque1.GetComponentInChildren<BoxCollider2D>().gameObject)
            {
                bloqueActual = bloque1;
                print(bloqueActual);
            }
            else if(ultimoTrigger.gameObject == bloque2.GetComponentInChildren<BoxCollider2D>().gameObject)
            {
                bloqueActual = bloque2;
                print(bloqueActual);
            }
            else if (ultimoTrigger.gameObject == bloque3.GetComponentInChildren<BoxCollider2D>().gameObject)
            {
                bloqueActual = bloque3;
                print(bloqueActual);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

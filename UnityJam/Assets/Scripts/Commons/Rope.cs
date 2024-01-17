using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] Color normalColor, maxDistanceColor;
    [SerializeField] Material chainMaterial;
    [SerializeField] float max_Distance; //Distancia apartir de la cual consideramos que los personajes estan muy separados.
    public LineRenderer line; //El Objeto debe tener un componente LineRender 
    public Transform posWarrior; // Posici�n desde donde nace la cuerda
    public Transform posSpirit; //Posici�n hasta donde llega la cuerda
    private float ropeLenght; // Distancia entre ambos puntos de la cuerda
    // Start is called before the first frame update
    void Start()
    {
        //Numero de vertices de la cuerda
        line.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Define la posicion del primer vertice (0)
        line.SetPosition(0, posWarrior.position);
        //Define la posicion del segundo vertice (1)
        line.SetPosition(1, posSpirit.position);
        //Calcula la tama�o actual de la cuerda
        ropeLenght = (posSpirit.position - posWarrior.position).magnitude;
       //Bucle que define que ocurre cuando se supera la distancia m�xima entre personajes
        if (ropeLenght > max_Distance)
        {
            chainMaterial.color = maxDistanceColor;
        }
        else
        {
            chainMaterial.color = normalColor;
        }
    }
}

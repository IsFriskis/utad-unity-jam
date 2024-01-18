using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float limitLeft = -20f;
    [SerializeField] Rope rope;
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector2 locationOffset;
    public float rotationOffset;
    public bool canMove;
    private GameObject background;
    [SerializeField] private GameObject cameraLimitRight;
    [SerializeField] private GameObject cameraLimitLeft;

    void Awake(){
        //background = GetComponentInChildren<SpriteRenderer>().gameObject;
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 desiredPosition = new Vector3(target.position.x + locationOffset.x, target.position.y + locationOffset.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            float desiredRotation = target.rotation.eulerAngles.z + rotationOffset;
            Quaternion desiredQuaternion = Quaternion.Euler(0f, 0f, desiredRotation);
            Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredQuaternion, smoothSpeed);
            transform.rotation = smoothedRotation;
        }
    }

    private void Update()
    {
        if (target.position.x < limitLeft)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
        RopeDistance();
    }

    void RopeDistance(){
        float backgroundSize = 1;
        /* if(rope.ropeLenght <= 17){//Camara más pequeña
            Camera.main.orthographicSize = 10;
        } */
        //Mates
        //Si 10 cuando ropeLenght == 17 y 42 cuando ropeLenght == 32
        float desiredSize = 10; //= rope.ropeLenght * 42f / 32f;
                           //-------
        if (rope.ropeLenght < 30)
        {
            desiredSize = 10;
            //cameraLimitLeft.transform.localPosition = new Vector3(-13, 0, 0);
            //cameraLimitRight.transform.localPosition = new Vector3(13, 0, 0);
        }


        if ((rope.ropeLenght > 17 )&& (rope.ropeLenght<30))
        {
            desiredSize = 20;
            //cameraLimitLeft.transform.localPosition = new Vector3(-27, 0, 0);
            //cameraLimitRight.transform.localPosition = new Vector3(27, 0, 0);
        }
        if (rope.ropeLenght > 30) 
        {
            desiredSize = 42;
        //    cameraLimitLeft.transform.localPosition = new Vector3(-56, 0, 0);
        //    cameraLimitRight.transform.localPosition = new Vector3(56, 0, 0);
        }


        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, desiredSize, 0.01f);
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 10, 42);
        backgroundSize = Mathf.Clamp(backgroundSize, 1, 4.5f);
        background.transform.localScale = new Vector3(backgroundSize, backgroundSize, backgroundSize);

    }
}


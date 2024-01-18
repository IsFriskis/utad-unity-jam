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
    [SerializeField] bool cameraHasChildBackground = true;
    [SerializeField] private GameObject cameraLimitRight;
    [SerializeField] private GameObject cameraLimitLeft;
    [SerializeField] Transform secondTarget;
    float backgroundSize = 1;


    void Awake(){
        if(cameraHasChildBackground){
            background = GetComponentInChildren<SpriteRenderer>().gameObject;
        }
        
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

    void RopeDistance()
    {
        float desiredSize = 10; //= rope.ropeLenght * 42f / 32f;

        float targetY =target.localPosition.y;
        float secondTargetY = secondTarget.localPosition.y;

        float distance = targetY - secondTargetY;
        distance = Mathf.Abs(distance);
        //-------
        if (distance < 7)
        {
            desiredSize = 10;
            backgroundSize = 1;
        }


        if ((distance >8.5) && (distance <15))
        {
            desiredSize = 20;
            backgroundSize = 2;
        }
        if (distance > 15)
        {
            desiredSize = 42;
            backgroundSize = 4.5f;
        }


        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, desiredSize, 0.01f);
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 10, 42);
        if (background != null)
        {
            float currentBackgroundSize = background.transform.localScale.x;
            currentBackgroundSize = Mathf.Lerp(currentBackgroundSize, backgroundSize, 0.01f);
            currentBackgroundSize = Mathf.Clamp(currentBackgroundSize, 1, 4.5f);
            background.transform.localScale = new Vector3(currentBackgroundSize, currentBackgroundSize, currentBackgroundSize);
        }
    }
}


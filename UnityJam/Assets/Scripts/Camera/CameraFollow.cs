using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector2 locationOffset;
    public float rotationOffset;
    public bool canMove;

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
        if (target.position.x<-20)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }
}


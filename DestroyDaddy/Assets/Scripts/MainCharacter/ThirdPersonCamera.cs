using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    float yaw;
    float pitch;
    public float mouseSensitivity = 10;
    public Transform target;
    public float distanceFromTarget = 10;
    public Vector2 pitchMinMax = new Vector2(20, 85);
    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    // Start is called before the first frame update

    // Update is called once per frame
    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);


        //Vector3 targetRotation = new Vector3(pitch, yaw);
        transform.localEulerAngles = currentRotation;

        transform.position = target.position - (transform.forward * distanceFromTarget); 
    }
}

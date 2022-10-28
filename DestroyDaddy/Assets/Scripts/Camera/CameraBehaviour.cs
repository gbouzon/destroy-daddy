using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity;
    
    private float rotationY;
    private float rotationX;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distanceFromTarget;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel") * 8.0f;

        rotationY += mouseX;
        rotationX += mouseY;
        distanceFromTarget += mouseScroll;

        rotationX = Mathf.Clamp(rotationX, -60, 60);
        distanceFromTarget = Mathf.Clamp(distanceFromTarget, 3, 15);

        transform.localEulerAngles = new Vector3(rotationX * -1, rotationY, 0);
        transform.position = target.position - transform.forward * distanceFromTarget;
    }
}
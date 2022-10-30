using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("Reference")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public Transform combatLookAt;
    public CameraStyle currentStyle;
    public GameObject thirdPersonCam;
    public float rotationSpeed;
    public GameObject combatCam;
     public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }

    public void Start(){
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    public void Update()
    {
            if (Input.GetKeyDown("Fire1")){
                SwitchCameraStyle(CameraStyle.Combat);
            } 

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

         if(currentStyle == CameraStyle.Basic){
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if(inputDir != Vector3.zero){
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

         }
        

        
    }

     private void SwitchCameraStyle(CameraStyle newStyle)
    {
        combatCam.SetActive(false);
        thirdPersonCam.SetActive(false);

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Combat) combatCam.SetActive(true);

        currentStyle = newStyle;
    }
}

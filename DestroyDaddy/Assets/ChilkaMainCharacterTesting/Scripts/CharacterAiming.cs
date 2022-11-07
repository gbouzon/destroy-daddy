using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

// getting rotation of camera in the y-axis and making player blends towards that rotation
// not the x and y axes of the player
public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15;
    public float aimDuration = 0.3f;

    Camera mainCamera;
    [SerializeField] Rig aimLayer;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        // turn off the cursor in the game
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Rotation of the camera in the y-axis
        float cameraRotationInYAxis = mainCamera.transform.rotation.eulerAngles.y; 
        // using the Slerp, we are blending from the current player's rotation towards the camera's rotation ONLY in the y-axis
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, cameraRotationInYAxis, 0), turnSpeed * Time.fixedDeltaTime);
    }

    private void Update() {
        if (Input.GetMouseButton(1)) {
            aimLayer.weight += Time.deltaTime / aimDuration;
        } 
        else {
            aimLayer.weight -= Time.deltaTime / aimDuration;
        }
    }
}
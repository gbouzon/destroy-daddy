using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations.Rigging;


public class MovementController : MonoBehaviour
{
    private CharacterController mainCharacterController;
    Animator animator;
    PlayerInput playerInput;
    Vector3 currentMovement;
    [SerializeField] private Rig aimRig;   
    

    // rotation with Camera Variable
    [SerializeField]
    Camera followCamera;
    public GameObject cinemachineCameraTarget;
    Vector2 mouseInput;
    float rotationVelocity;
    //If the character is grounded or not. Not part of the CharacterController built in grounded check
    public bool Grounded = true;
    
    private float terminalVelocity = 53.0f;

    
    
    // Movement variable  
    private Vector2 movementInput;
    float runSpeed = 5.0f;
    float walkSpeed = 2.0f;
    private float playerSpeed;
    float targetRotation;
    private bool rotateOnMove = true;
    private float aimRigWeight;
    
    private float speed;

    // if user input Bool
    bool isMovementPressed;
    bool isRunPressed;
    bool isJumpPressed;
    bool isAimPressed;
    bool isMouseMove; 
    [Tooltip("What layers the character uses as ground")]
    

    
    
    

    // cinemachine
    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;
      


    // gravity variable 
    float gravity = -9.8f; 

    // jumping variable
    public float maxJumpHeight = 2.0f;
    private float verticalVelocity;
    public float SpeedChangeRate = 2;
    // timeout deltatime
    private float jumpTimeoutDelta;
    private float fallTimeoutDelta;
    public float GroundedOffset = -0.14f;
    private float GroundedRadius = 0.28f;
  
    private float FallTimeout = 0.15f;

    private float JumpTimeout = 0.50f;
    public LayerMask GroundLayers;


    // Animation State
    private float animationBlend;
    private int animSpeed;
    private int animGrounded;
    private int animJump;
    private int animFreeFall;
    private int animMotionSpeed;
    private int aimAnimationLayer = 2; 
        

    void Awake(){
        //setupJumpVariable();
        AssignAnimationIDs();
        Cursor.lockState = CursorLockMode.Locked;
        // initially set reference variable
        playerInput = new PlayerInput();
        mainCharacterController = this.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Set the player input callBack
        // return Vector 2
        playerInput.MainCharacterControls.Move.started += onMovementInput;
        playerInput.MainCharacterControls.Move.canceled += onMovementInput;
        playerInput.MainCharacterControls.Move.performed += onMovementInput;
        
        // return true or false 
        playerInput.MainCharacterControls.Run.started+= onRun;
        playerInput.MainCharacterControls.Run.canceled+= onRun;

        // return true or false 
        playerInput.MainCharacterControls.Jump.started+= onJump;
        playerInput.MainCharacterControls.Jump.canceled+= onJump;

        // return true or false 
        playerInput.MainCharacterControls.Aim.performed+= onAim;
        playerInput.MainCharacterControls.Aim.canceled+= onAim;

        // return true or false 
        playerInput.MainCharacterControls.Mouse.performed+= onMouse;
        playerInput.MainCharacterControls.Mouse.canceled+= onMouse;


        if (MainMenu.pd != null) {
            transform.position = new Vector3(MainMenu.pd.playerPosition[0], MainMenu.pd.playerPosition[1], MainMenu.pd.playerPosition[2]);
            transform.rotation = new Quaternion(MainMenu.pd.playerRotation[0], MainMenu.pd.playerRotation[1], MainMenu.pd.playerRotation[2], MainMenu.pd.playerRotation[3]);
            MainMenu.pd = null;
        }
    }


    // Update is called once per frame
    void Update(){
        
        if (Time.timeScale == 0 && PlayerUI.isSaving == false) {
            Cursor.lockState = CursorLockMode.None;
            followCamera.GetComponent<CinemachineBrain>().enabled = false;
            Cursor.visible = true;
            return;
        }
        else if (Time.timeScale == 0) {
            Cursor.lockState = CursorLockMode.Locked;
            followCamera.GetComponent<CinemachineBrain>().enabled = false;
            Cursor.visible = false;
            return;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
            followCamera.GetComponent<CinemachineBrain>().enabled = true;
            Cursor.visible = false;
        }
       
        
        
        handleAimingAnimation();
        handleJump();
        GroundedCheck();
        move();
        handleMovementAnimation();
        aimRig.weight = Mathf.Lerp(aimRig.weight, aimRigWeight, Time.deltaTime * 20f);
        
    }

    void LateUpdate(){
       CameraRotation();
    }

    //=================================================Handle functions for the playerInput CallBack==================================================================//
    
    // callBack handler function for aim button
    void onMouse(InputAction.CallbackContext context){
       mouseInput = context.ReadValue<Vector2>();
       isMouseMove = mouseInput != Vector2.zero; 
    }


    // callBack handler function for aim button
    void onAim(InputAction.CallbackContext context){
        isAimPressed = context.ReadValueAsButton();
    }

    // callBack handler function for Jump button
    void onJump(InputAction.CallbackContext context){
        isJumpPressed = context.ReadValueAsButton();
    }

    // callBack handler function for run button
    void onRun(InputAction.CallbackContext context){
        isRunPressed = context.ReadValueAsButton();
    }

     // callBack handler function to set the player input values
    void onMovementInput(InputAction.CallbackContext context){
        movementInput = context.ReadValue<Vector2>();

        // Vector 3 = {x, y, z};    
        currentMovement = new Vector3(movementInput.x, 0, movementInput.y);
        isMovementPressed = currentMovement != Vector3.zero; 
    }

    void OnEnable(){
        // enable the charater controls action map
        playerInput.MainCharacterControls.Enable();
    }

    void OnDisable(){
        // disable the charater controls action map
        playerInput.MainCharacterControls.Disable();
    }
//============================================================Handle Movement==================================================================//    
  
    private void GroundedCheck()
     {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        animator.SetBool(animGrounded, Grounded);
    }

    void move(){
        playerSpeed = !isRunPressed ? walkSpeed :runSpeed;
        
        if(isMovementPressed){
            targetRotation = Mathf.Atan2(currentMovement.x, currentMovement.z) * Mathf.Rad2Deg +
                followCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,
                     0.3f);
            if(rotateOnMove){
             // rotate to face input direction relative to camera position
              transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f); 
            }
             
            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            //Add movement to the Character
            mainCharacterController.Move(targetDirection.normalized * (playerSpeed * Time.deltaTime) +
                            new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
        }
    }

        
    void CameraRotation()
        {
            // if there is an input 
            if (mouseInput.sqrMagnitude >= 0.01f)
            {
                cinemachineTargetYaw += mouseInput.x ;
                cinemachineTargetPitch += mouseInput.y ;

                // clamp our rotations so our values are limited 360 degrees
                cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
                cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, -40.0f, 50.0f);
                
            }
            // Cinemachine will follow this target
                cinemachineCameraTarget.transform.rotation = Quaternion.Euler(cinemachineTargetPitch + 0.0f,
                cinemachineTargetYaw, 0.0f);
        }    

     private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

//===========================================================Handle Jumping============================================================================//

    void handleJump(){
        if(Grounded){
            fallTimeoutDelta = FallTimeout;
            animator.SetBool(animJump, false);
            animator.SetBool(animFreeFall, false);
            if(verticalVelocity < 0.0f) {
                verticalVelocity = -2;
            }

            if(isJumpPressed && jumpTimeoutDelta <= 0.0f){
                verticalVelocity = Mathf.Sqrt(maxJumpHeight * -2f * gravity);
                animator.SetBool(animJump, true);
            }

            if (jumpTimeoutDelta >= 0.0f) jumpTimeoutDelta -= Time.deltaTime;
        }else{
            jumpTimeoutDelta = JumpTimeout;
            if (fallTimeoutDelta >= 0.0f)
                {
                    fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    animator.SetBool(animFreeFall, true);
                }
        }
        if (verticalVelocity < terminalVelocity)
            {
                verticalVelocity += gravity * Time.deltaTime;
            }
    }


//========================================================Handle Animation============================================================================//
    private void AssignAnimationIDs()
    {
        animSpeed = Animator.StringToHash("Speed");
        animGrounded = Animator.StringToHash("Grounded");
        animJump = Animator.StringToHash("Jump");
        animFreeFall = Animator.StringToHash("FreeFall");
        animMotionSpeed = Animator.StringToHash("MotionSpeed");
    }
    
    public void handleMovementAnimation(){
        float currentMainCharacterSpeed = new Vector3(mainCharacterController.velocity.x, 0.0f, mainCharacterController.velocity.z).magnitude;
        float speedOffset = 0.1f;


        if (currentMainCharacterSpeed  < playerSpeed - speedOffset ||
            currentMainCharacterSpeed  > playerSpeed  + speedOffset){
            // creates curved result rather than a linear one giving a more organic speed change
                speed = Mathf.Lerp(currentMainCharacterSpeed, playerSpeed,
                Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                speed = Mathf.Round(speed * 1000f) / 1000f;
        }else{
            speed = playerSpeed;
        }
                
                
        animationBlend = Mathf.Lerp(animationBlend, playerSpeed * currentMovement.magnitude, Time.deltaTime * SpeedChangeRate);
        if (animationBlend < 0.01f) 
            animationBlend = 0f;

        animator.SetFloat(animSpeed, animationBlend);
        animator.SetFloat(animMotionSpeed, currentMovement.magnitude);
    }

    public void handleAimingAnimation(){
        bool isFirePressed = Input.GetMouseButton(0); // get the fire Input 

         // set isAiming parameter in Animtor to true if aim or fire pressed   
        if(isAimPressed || isFirePressed){
            
           animator.SetLayerWeight(aimAnimationLayer, 
            Mathf.Lerp(animator.GetLayerWeight(aimAnimationLayer), 1f, Time.deltaTime * 10f));
            aimRigWeight = 1f;
        } else {
            animator.SetLayerWeight(aimAnimationLayer, 
            Mathf.Lerp(animator.GetLayerWeight(aimAnimationLayer), 0f, Time.deltaTime * 10f));
            aimRigWeight = 0f;
            
        }
    }    
//==================================================GET and SET methods======================================================//

    public bool getIsAim(){
        return isAimPressed;
    }

    public void setRotateOnMove(bool newRotateOnMove){
        rotateOnMove = newRotateOnMove;
    }
}

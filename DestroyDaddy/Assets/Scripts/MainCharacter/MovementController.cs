using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.SceneManagement;


public class MovementController : MonoBehaviour
{
    private CharacterController mainCharacterController;
    Animator animator;
    PlayerInput playerInput;
    Vector3 currentMovement;  
    

    // rotation with Camera Variable
    [SerializeField]
    Camera followCamera;
    public GameObject cinemachineCameraTarget;
    Vector2 mouseInput;
    float rotationVelocity;
    
    
    
    
    // Movement variable  
    private Vector2 movementInput;
    float runSpeed = 5.0f;
    float walkSpeed = 2.0f;
    private float playerSpeed;
    float targetRotation;
    private bool rotateOnMove = true;
    

    // if user input Bool
    bool isMovementPressed;
    bool isRunPressed;
    bool isJumpPressed;
    bool isAimPressed;
    bool isMouseMove; 

    

    // cinemachine
    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;
      


    // gravity variable 
    float gravity = -9.8f; 
    float groundedGravity = -.05f;

    // jumping variable
    bool isJumpAnimating;
    float initialJumpVelocity;
    float maxJumpHeight = 2.0f;
    float maxJumpTime = 0.57f;
    float fallMultiplier = 3.0f;
    bool isJumping = false;
    private float verticalVelocity;


    // Animation State
    int nullState = 0;
    int idleState = 1;
    int walkingState = 2;
    int runningState = 3;

    void Awake(){

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

        setupJumpVariable();
    }


    // Update is called once per frame
    void Update(){
        
        move();
        handleMovementAnimation();
        handleAimingAnimation();
        handleGravity();
        handleJump();
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
                if(isAimPressed){
                    cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, -40.0f, 50.0f);
                } else{
                    cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, -0.0f, 1.0f);
                }
                
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

    // set the initial velocity and gravity using jump height and duration
    void setupJumpVariable(){
        float timeToAplex = maxJumpTime / 2; 
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToAplex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToAplex;
    }

    // handle Jump animation and set the initialJumpVelocity to the y axic
    void handleJump(){
        if(!isJumping && mainCharacterController.isGrounded && isJumpPressed){
            isJumpAnimating = true;
            animator.SetBool("isJumping", true);
            
            if(!isRunPressed && !isMovementPressed){
                animator.SetInteger("JumpState", idleState);
            } else if(!isRunPressed && isMovementPressed) {
                animator.SetInteger("JumpState", walkingState);
            } else if(isRunPressed && isMovementPressed){
                 animator.SetInteger("JumpState", runningState);
            } else {
                 animator.SetInteger("JumpState", idleState);
            }
            
            isJumping = true;
            verticalVelocity = initialJumpVelocity;
            currentMovement.y = initialJumpVelocity;
        } else if(!isJumpPressed && isJumping && mainCharacterController.isGrounded){
            isJumping = false;
            isJumpAnimating = false;
            animator.SetInteger("JumpState", nullState);
        }
    }

    void handleGravity(){
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        
        //apply proper gravity if the player is grounded or not
        if(mainCharacterController.isGrounded){
            if(isJumpAnimating){
                animator.SetBool("isJumping", false);
                animator.SetInteger("JumpState", nullState);
                isJumpAnimating = false;
            }
            currentMovement.y = groundedGravity;
            verticalVelocity = groundedGravity;
        } else if(isFalling){
            // Velocity Verlet Integration
            float prevYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + ( gravity * fallMultiplier *  Time.deltaTime);
            verticalVelocity = Mathf.Max((prevYVelocity + currentMovement.y) * .5f, -20.0f);

        }
        else {
            float prevYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + ( gravity * Time.deltaTime);
            verticalVelocity = (prevYVelocity + currentMovement.y) * .5f;
        }
    }


//========================================================Handle Animation============================================================================//
    public void handleMovementAnimation(){
        // get parameter values from animator
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");
        
        //start walking if movement pressed is true and not already walking
        if(isMovementPressed && !isWalking){
            animator.SetBool("isWalking", true);
        //stop walking if movement pressed is false and currently walking    
        }else if(!isMovementPressed && isWalking){
            animator.SetBool("isWalking", false);
        }
        //start running if movement and run pressed are true and not currently running   
        if((isMovementPressed && isRunPressed) && !isRunning){
            animator.SetBool("isRunning", true);
        //stop running if movement or run pressed are false and currently running 
        }else if((!isMovementPressed || !isRunPressed) && isRunning){
            animator.SetBool("isRunning", false);
        }
    }

    public void handleAimingAnimation(){
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");
        bool isAiming = animator.GetBool("isAiming");
        bool isFirePressed = Input.GetButton("Fire1"); // get the fire Input 

         // set isAiming parameter in Animtor to true if aim or fire pressed   
         if(isAimPressed || isFirePressed){
            animator.SetBool("isAiming", true);
            
            
            //set AimeState to idle if run and movement press is false and is not already running or walking
            if(!isRunPressed && !isMovementPressed && !isWalking && !isRunning){
                animator.SetInteger("AimState", idleState);
            //set AimeState to walking if run press is false and movement press is true and is not already running
            } else if(!isRunPressed && isMovementPressed && isWalking && !isRunning) {
                animator.SetInteger("AimState", walkingState);
            //set AimeState to Running if run press  movement press is true and is currently running     
            } else if((isMovementPressed && isRunPressed) && isRunning){
                 animator.SetInteger("AimState", runningState);
            } else {
                animator.SetInteger("AimState", nullState);
            }
         // set isAiming parameter in Animtor to false if aim or fire pressed    
        } else if ((!isAimPressed && isAiming) || isFirePressed){
            animator.SetBool("isAiming", false);
            animator.SetInteger("AimState", nullState);
            
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

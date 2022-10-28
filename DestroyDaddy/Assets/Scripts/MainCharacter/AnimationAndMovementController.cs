using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController mainCharacterController; 
    Animator animator;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 appliedMovement;
    bool isMovementPressed;
    bool isRunPressed;
    bool isJumpPressed;

    bool isAimPressed;
    bool isAim;
    float rotationFactorPerframe = 15.0f;

    float gravity = -9.8f;
    float groundedGravity = -.05f;
    bool isJumpAnimating;
    float initialJumpVelocity;
    float maxJumpHeight = 1.5f;
    float maxJumpTime = 0.75f;
    bool isJumping = false;

    int nullState = 0;
    int idleState = 1;
    int walkingState = 2;
    int runningState = 3;
    void Awake(){
        playerInput = new PlayerInput();
        mainCharacterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        

        playerInput.MainCharacterControls.Move.started += onMovementInput;
        playerInput.MainCharacterControls.Move.canceled += onMovementInput;
        playerInput.MainCharacterControls.Move.performed += onMovementInput;
        playerInput.MainCharacterControls.Run.started+= onRun;
        playerInput.MainCharacterControls.Run.canceled+= onRun;
        playerInput.MainCharacterControls.Jump.started+= onJump;
        playerInput.MainCharacterControls.Jump.canceled+= onJump;
        playerInput.MainCharacterControls.Aim.performed+= onAim;
        playerInput.MainCharacterControls.Aim.canceled+= onAim;

        setupJumpVariable();
    }

    // Update is called once per frame
    void Update()
    {
        handleAnimation();
        handleRotation();
        if(isRunPressed){
            appliedMovement.x = currentRunMovement.x; 
            appliedMovement.z = currentRunMovement.z; 
        } else {
            appliedMovement.x = currentMovement.x; 
            appliedMovement.z = currentMovement.z; 
        }

        mainCharacterController.Move(appliedMovement * Time.deltaTime);
        handleGravity();
        handleJump();
        
    }
    
    void OnEnable(){
        // enable the charater controls action map
        playerInput.MainCharacterControls.Enable();
    }

    void OnDisable(){
        // disable the charater controls action map
        playerInput.MainCharacterControls.Disable();
    }

    void onAim(InputAction.CallbackContext context){
        isAimPressed = context.ReadValueAsButton();
    }

    void onJump(InputAction.CallbackContext context){
        isJumpPressed = context.ReadValueAsButton();
    }

    void setupJumpVariable(){
        float timeToAplex = maxJumpTime / 2; 
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToAplex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToAplex;
    }

    void handleJump(){
        if(!isJumping && mainCharacterController.isGrounded && isJumpPressed){
            isJumpAnimating = true;
            animator.SetBool("isJumping", true);
            Debug.Log(animator.GetBool("isJumping"));
            
            
            if(!isRunPressed && !isMovementPressed){
                animator.SetInteger("JumpState", idleState);
            } else if(!isRunPressed && isMovementPressed) {
                animator.SetInteger("JumpState", walkingState);
            } else if(isRunPressed && isMovementPressed){
                 animator.SetInteger("JumpState", runningState);
            } else {
                 animator.SetInteger("JumpState", nullState);
            }
            Debug.Log(animator.GetInteger("JumpState"));
            
            isJumping = true;
            currentMovement.y = initialJumpVelocity ;
            appliedMovement.y = initialJumpVelocity;
        } else if(!isJumpPressed && isJumping && mainCharacterController.isGrounded){
            isJumping = false;
            animator.SetInteger("JumpState", nullState);
            Debug.Log(animator.GetBool("isJumping"));
            Debug.Log(animator.GetInteger("JumpState"));
        }
    }


    void onRun(InputAction.CallbackContext context){
        isRunPressed = context.ReadValueAsButton();
    }

    void onMovementInput(InputAction.CallbackContext context){
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * 5.0f;
        currentRunMovement.z = currentMovementInput.y  * 5.0f;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void handleAnimation(){
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");
        bool isAiming = animator.GetBool("isAiming");

        if(isMovementPressed && !isWalking){
            animator.SetBool("isWalking", true);
        }else if(!isMovementPressed && isWalking){
            animator.SetBool("isWalking", false);
        }

        if((isMovementPressed && isRunPressed) && !isRunning){
            animator.SetBool("isRunning", true);
        }else if((!isMovementPressed || !isRunPressed) && isRunning){
            animator.SetBool("isRunning", false);
        }

        if(isAimPressed){
            animator.SetBool("isAiming", true);
            Debug.Log(animator.GetBool("isAiming"));
            Debug.Log("IsAimPressed" + isAimPressed);

            if(!isRunPressed && !isMovementPressed && !isWalking && !isRunning){
                animator.SetInteger("AimState", idleState);
            } else if(!isRunPressed && isMovementPressed && isWalking && !isRunning) {
                animator.SetInteger("AimState", walkingState);
            } else if((isMovementPressed && isRunPressed) && isRunning){
                 animator.SetInteger("AimState", runningState);
            } else {
                animator.SetInteger("AimState", nullState);
            }


        } else if (!isAimPressed && isAiming){
            animator.SetBool("isAiming", false);
            animator.SetInteger("AimState", nullState);
        }
    }

    void handleGravity(){
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;
        if(mainCharacterController.isGrounded){
            if(isJumpAnimating){
                animator.SetBool("isJumping", false);
                animator.SetInteger("JumpState", nullState);
                isJumpAnimating = false;
            }

            currentMovement.y = groundedGravity;
            appliedMovement.y = groundedGravity;
        } else if(isFalling){
            // Velocity Verlet Integration
            float prevYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + ( gravity * fallMultiplier *  Time.deltaTime);
            appliedMovement.y = Mathf.Max((prevYVelocity + currentMovement.y) * .5f, -20.0f);
        }
        else {
            float prevYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + ( gravity * Time.deltaTime);
            appliedMovement.y = (prevYVelocity + currentMovement.y) * .5f;
        }
    }

    void handleRotation(){
        Vector3 positionToLookAt;

        //change in position our charater should point to
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        //current rotation of our character
        Quaternion currentRotation = transform.rotation; 

        if(isMovementPressed){
            Quaternion tragetRotation =  Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, tragetRotation, rotationFactorPerframe * Time.deltaTime);
        }
    }
}

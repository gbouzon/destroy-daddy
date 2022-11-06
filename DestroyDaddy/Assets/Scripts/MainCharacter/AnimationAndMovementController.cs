using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AnimationAndMovementController : MonoBehaviour
{

    [SerializeField]
    GameObject enterShipCanvas;
    [SerializeField]
    Transform mainCamera;

    // PlayerInput class was generated from The New Input System in Inspector
    PlayerInput playerInput; 
    CharacterController mainCharacterController; 
    Animator animator;

    // variable to store player input values
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 appliedMovement;
    bool isMovementPressed;
    bool isRunPressed;
    bool isJumpPressed;
    bool isAimPressed;

    // Constant variable
    float rotationFactorPerframe = 45.0f;
    int zero = 0;
    float runMultiplier = 5.0f; 

    // gravity variable 
    float gravity = -9.8f;
    float groundedGravity = -.05f;

    // jumping variable
    bool isJumpAnimating;
    float initialJumpVelocity;
    float maxJumpHeight = 1.5f;
    float maxJumpTime = 0.75f;
    bool isJumping = false;

    Vector3 rotation;

    // Animation State
    int nullState = 0;
    int idleState = 1;
    int walkingState = 2;
    int runningState = 3;

    
    void Awake(){
        //camera = Camera.main.transform;
        // initially set reference variable
        playerInput = new PlayerInput();
        mainCharacterController = GetComponent<CharacterController>();
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

        setupJumpVariable();
    }


    void Update()
    {   
        
        handleMovementAnimation();
        handleAimingAnimation();

        handleRotation();
        //appliedMovement.x = mainCamera.rotation.eulerAngles.y; 
        appliedMovement.z = currentMovement.z;
        this.transform.eulerAngles = new Vector3(0,mainCamera.rotation.eulerAngles.y,0);
        //appliedMovement.y =  Quaternion.AngleAxis(mainCamera.transform.eulerAngles.y, Vector3.up);
        appliedMovement = Quaternion.AngleAxis(mainCamera.rotation.eulerAngles.y, Vector3.up) * appliedMovement;
        mainCharacterController.Move( appliedMovement * Time.deltaTime); // make the Character move
        
        handleGravity();
        handleJump();
    }
    
    //=================================================Handle functions for the playerInput CallBack==================================================================//

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
        // Vector 3 = {x, y, z};
        currentMovementInput = context.ReadValue<Vector2>();
        // x is horizentoal
        //currentMovement.x = !isRunPressed ? currentMovementInput.x : currentMovementInput.x * runMultiplier;
        // z is vertical
        currentMovement.z = !isRunPressed ? currentMovementInput.y : currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.x != zero || currentMovementInput.y != zero;
    }

    void OnEnable(){
        // enable the charater controls action map
        playerInput.MainCharacterControls.Enable();
    }

    void OnDisable(){
        // disable the charater controls action map
        playerInput.MainCharacterControls.Disable();
    }

//============================================================Handle Rotation==================================================================//    

    void handleRotation(){
        


        //Vector3 positionToLookAt;

        // //change in position our charater should point to
        // positionToLookAt.x = currentMovement.x;
        // positionToLookAt.y = 0.0f;
        // positionToLookAt.z = currentMovement.z;

            //positionToLookAt = Quaternion.AngleAxis(mainCamera.rotation.eulerAngles.y, Vector3.up) * currentMovement;
            //positionToLookAt.Normalize();

            


        //current rotation of our character
       //Quaternion currentRotation = transform.rotation;

    //  if(currentMovement != Vector3.zero){
    //     // create a new rotatio based on where the player is currently pressing
    //     Quaternion tragetRotation =  Quaternion.LookRotation(new Vector3(0,0,));
    //     Debug.Log(positionToLookAt);
    //     Debug.Log("TragetRotation: " + tragetRotation);
    //     Debug.Log(("Camera: " + mainCamera.rotation.eulerAngles.y));
        // // rotate the character to face teh positionToLookAt

        
        //transform.rotation = Quaternion.RotateTowards(currentRotation, tragetRotation, rotationFactorPerframe * Time.deltaTime);
     //}
        

       
    }

  
    // private void OnApplicationFocus (bool focus){
    //     if (focus)
    //       Cursor.lockState =  CursorLockMode. Locked;
    //     else  
    //       Cursor.lockState = CursorLockMode. None;

    // }
    


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
            
            isJumping = true;
            currentMovement.y = initialJumpVelocity ;
            appliedMovement.y = initialJumpVelocity;
        } else if(!isJumpPressed && isJumping && mainCharacterController.isGrounded){
            isJumping = false;
            animator.SetInteger("JumpState", nullState);
        }
    }


    void handleGravity(){
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;
        //apply proper gravity if the player is grounded or not
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


    void OnTriggerEnter(Collider col) {
        if(col.gameObject.name == "Ship"){
            enterShipCanvas.SetActive(true);
        }
    }

    void OnTriggerStay(Collider col) {
        if(col.gameObject.name == "Ship"){
            if (Input.GetKey(KeyCode.F)) {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                SceneManager.LoadScene("Space");
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if(col.gameObject.name == "Ship"){
            enterShipCanvas.SetActive(false);
        }
    }
}

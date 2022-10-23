using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        bool fowardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool runPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)  ;
        // bool isWalking = animator.GetBool("isWalking");
        // bool isRunning = animator.GetBool("isRunning");

        if(fowardPressed && velocity < 1.0f){
           // animator.SetBool("isWalking", true);
           velocity += Time.deltaTime * acceleration;
        }

        if(!fowardPressed && velocity > 0.0f ){
           // animator.SetBool("isWalking", true);
           velocity -= Time.deltaTime * acceleration;   
        }   

        
        if(!fowardPressed && velocity < 0.0f ){
           // animator.SetBool("isWalking", true);
           velocity  = 0.0f;
        }   

        // if(isWalking && !fowardPressed){
        //     animator.SetBool("isWalking", false);
        // }

        // if(!isRunning && (fowardPressed && runPressed)){
        //     animator.SetBool("isRunning", true);
        // }

        // if(isRunning && (!fowardPressed || !runPressed)){
           
        // }
        animator.SetFloat("Velocity", velocity);
        Debug.Log(velocity);
        }

         


    
}

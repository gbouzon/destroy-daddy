using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
   Animator animator;
   float velocityZ = 0.0f;
   float velocityX = 0.0f;
   public float acceleration = 0.2f;
   public float deceleration = 0.2f;
   public float maxWalkVelocity = 0.5f;
   public float maxRunVelocity = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
      bool fowardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
      bool runPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
      bool rightPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
      bool leftPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
      float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

      changeVelocity(fowardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
      lockOrResetVelocity(fowardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
      
      animator.SetFloat("Velocity Z", velocityZ);
      animator.SetFloat("Velocity X", velocityX);
      }

   void changeVelocity(bool fowardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity){
        if(fowardPressed && velocityZ < currentMaxVelocity){
         // animator.SetBool("isWalking", true);
         velocityZ += Time.deltaTime * acceleration;
      }

      if(leftPressed && velocityX > -currentMaxVelocity){
         velocityX -= Time.deltaTime * acceleration;
      }

      if(rightPressed && velocityX < currentMaxVelocity){
         velocityX += Time.deltaTime * acceleration;
      }

      if(!fowardPressed && velocityZ > 0.0f ){
         // animator.SetBool("isWalking", true);
         velocityZ -= Time.deltaTime * deceleration;   
      }   

      if(!fowardPressed && velocityZ < 0.0f ){
         // animator.SetBool("isWalking", true);
         velocityZ  = 0.0f;
      }   

      if(!leftPressed && velocityX < 0.0f){
         velocityX += Time.deltaTime * deceleration;
      }

      if(!rightPressed && velocityX > 0.0f){
         velocityX -= Time.deltaTime * deceleration;
      } 
   }

   void lockOrResetVelocity(bool fowardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity){
      if(!leftPressed && !rightPressed && 
      velocityX != 0.0f  &&
      (velocityX > -0.05f && velocityX < 0.05f)){
         velocityX  = 0.0f;
      }

      if(fowardPressed && runPressed && velocityZ > currentMaxVelocity){
         velocityZ = currentMaxVelocity;
      }else if(fowardPressed && velocityZ > currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.5f)){
          velocityZ -= Time.deltaTime * deceleration;
      }

      if(leftPressed && runPressed && velocityX < -currentMaxVelocity){
         velocityX = -currentMaxVelocity;
      }else if(leftPressed && velocityX < -currentMaxVelocity){
         velocityX += Time.deltaTime * deceleration;
         if(velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f)){
            velocityX = -currentMaxVelocity;
         }
      }else if(leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f)){
         velocityX = -currentMaxVelocity;
      }

      if(rightPressed && runPressed && velocityX > currentMaxVelocity){
         velocityX = currentMaxVelocity;
      }else if(rightPressed  && velocityX > currentMaxVelocity){
         velocityX += Time.deltaTime * deceleration;
         if(velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity - 0.05f)){
            velocityX = -currentMaxVelocity;
         }
      }else if(rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity + 0.05f)){
         velocityX = currentMaxVelocity;
      }
   }   

      
}

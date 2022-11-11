using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class GunController : MonoBehaviour
{   
    [SerializeField]
    public int damage = 10;


    // Shooting Variable
    [SerializeField]
    private float fireRate = 1f;
    public float range = 500f;
    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private GameObject muzzle;
    RaycastHit hit;

    [SerializeField]
    private  CinemachineVirtualCamera  aimCamera; 
    private float timer;

    
   
    // private AudioSource gunAudio;
    MovementController mainCharacterScript;
    Vector3 targetPosition = Vector3.zero; 


    void Awake(){
        mainCharacterScript = GetComponent<MovementController>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out hit, range)){
            targetPosition = hit.point;
            Shoot();
        }

         if(mainCharacterScript.getIsAim()){
            aimCamera.gameObject.SetActive(true);
            mainCharacterScript.setRotateOnMove(false);

            Vector3 worldAimTarget = targetPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDir = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20f);
        }else{
            aimCamera.gameObject.SetActive(false);
            mainCharacterScript.setRotateOnMove(true);
        }
    }

    void Shoot() {
         timer += Time.deltaTime;
         if(timer >= fireRate){
            if (Input.GetMouseButton(0))
            {
                // to-do
                /**
                if(tranform.postion.y != mouseWorlfPostion){
                    turn to the TargetPostion
                }
                */

                timer = 0f;
                if(hit.collider.gameObject.tag == "Enemy"){
                    //the send message method can't find the receiver of the message 
                    //muzzle.SendMessage("HitByRay");
                    /*var enemy = hit.collider.GetComponent<EnemyHealth>();
                    Debug.Log("Hit enemy: " + hit.transform.name);
                    enemy.TakeDamage(damage);
                    hit.transform.SendMessage("HitByRay");
                    Debug.Log(hit.transform.name + " health: " + enemy.currentHealth);*/
                }

                Vector3 aimDir = (targetPosition - firePoint.position).normalized;
                GameObject laser = GameObject.Instantiate(muzzle, firePoint.position, Quaternion.LookRotation(aimDir, Vector3.up)) as GameObject;
                laser.GetComponent<ShotBehavior>().setTarget(hit.point);
                GameObject.Destroy(laser, 1f);
            }
        }
    }
    
}
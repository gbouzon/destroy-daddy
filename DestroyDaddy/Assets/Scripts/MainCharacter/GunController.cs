using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class GunController : MonoBehaviour
{
    public int damage = 10;
    private float fireRate = 1;
    public float range = 10f;
    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private ParticleSystem muzzleParticle;

    [SerializeField]
    private CinemachineVirtualCamera normal;
    
    [SerializeField]
    private  CinemachineVirtualCamera  aimCamera; 

   // private LineRenderer laserLine;    
    [SerializeField]
    private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField]
    private Transform debugTranfrom;



         

    private float timer;
    // private AudioSource gunAudio;
    Animator animator;
    MovementController mainCharacterScript;
    PlayerInput playerInput; 
 

    void Start() {

    // Get and store a reference to our LineRenderer component
        //laserLine = GetComponent<LineRenderer>();

    }
    void Awake(){
        animator = GetComponent<Animator>();
        mainCharacterScript =  GameObject.Find("MainCharacter").GetComponent<MovementController>();
       
    }

    

    // Update is called once per frame
    void Update()
    {

        if(mainCharacterScript.getIsAim()){
            aimCamera.gameObject.SetActive(true);
        }else{
            aimCamera.gameObject.SetActive(false);
        }


        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit raycashHit, 999f, aimColliderLayerMask)) 
            debugTranfrom.position = raycashHit.point;
  
       // bool isAiming = animator.GetBool("isAiming");
        
        // timer += Time.deltaTime;
        // if(timer >= fireRate){
        //     if (Input.GetButtonDown("Fire1"))
        //     {  
        //         //mainCharacterScript.handleAimingAnimation();
        //         timer = 0f; 
        //      //   laserLine.enabled = true;
               
        //         Shoot();
               
        //     }
        // }
        
    }

    void Shoot() {
        muzzleParticle.Play();
      //  laserLine.SetPosition(0, firePoint.forward * 100);
        RaycastHit hit;
        if(Physics.Raycast(firePoint.position, firePoint.forward * 100, out hit, range)){
            if(hit.collider.gameObject.tag == "Enemy"){
                // laserLine.SetPosition(1, hit.point);
                var enemy = hit.collider.GetComponent<EnemyHealth>();
                Debug.Log("Hit enemy: " + hit.transform.name);
                enemy.TakeDamage(damage);
                Debug.Log(hit.transform.name + " health: " + enemy.currentHealth);
            }
           
        }
        // else {
        //     laserLine.SetPosition(1, firePoint.position + firePoint.forward * 100 * range);
        // }
        Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.yellow, range);
    
    }

   


        // gunAudio.Play();
        
    
}
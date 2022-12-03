using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class GunController : MonoBehaviour
{   
    [SerializeField]
    public int damage = 1;


    // Shooting Variable
    [SerializeField]
    private float fireRate = 2f;
    [SerializeField]
    public float range = 20f;
    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private GameObject muzzle;
    public float turnSpeed = 20;
    RaycastHit hit;

    [SerializeField]
    private  CinemachineVirtualCamera  aimCamera;
    private float timer = 10f;
    float rotationVelocity;
    [SerializeField] GameObject crosshair;
    [SerializeField] Camera camera;

    GameObject laser;
    
   public Hovl_Laser LaserScript;
    // private AudioSource gunAudio;
    MovementController mainCharacterScript;
    Vector3 targetPosition = Vector3.zero; 
     [SerializeField]
    private Transform rigTarget;
    public LineRenderer laserLine;



    void Awake(){
        mainCharacterScript = GetComponent<MovementController>();
        laserLine = GetComponent<LineRenderer>();        
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) {
            return;
        }

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out hit, range)){
            targetPosition = hit.point;
            rigTarget.position = hit.point;
            Shoot(hit);
        }else{
            targetPosition = Camera.main.transform.position + Camera.main.transform.forward * range;
            rigTarget.position = Camera.main.transform.position + Camera.main.transform.forward * range;
        }   
         
        if(mainCharacterScript.getIsAim()){
            aimCamera.gameObject.SetActive(true);
            mainCharacterScript.setRotateOnMove(false);
            aimRotation();
        }else{
            aimCamera.gameObject.SetActive(false);
            mainCharacterScript.setRotateOnMove(true);
        }
        

        if(Input.GetMouseButton(0)){
            aimRotation();
            laserInstance();
            crosshair.SetActive(true);
        }

        if(Input.GetMouseButtonUp(0)){
            Destroy(laser,0.2f);
            crosshair.SetActive(false);
        }    
    }

    void Shoot(RaycastHit hit) {
         timer += Time.deltaTime;
         if(timer >= fireRate){
            if (Input.GetMouseButton(0))
            {
                timer = 0f;
                if(hit.collider.gameObject.tag == "Enemy"){
                    //the send message method can't find the receiver of the message
                    GameObject enemyPre = GameObject.FindWithTag("enemyPrefab");
                    enemyPre.SendMessage("HitByRay");
                    //muzzle.SendMessage("HitByRay");
                    // var enemy = hit.collider.GetComponent<EnemyHealth>();
                    // Debug.Log("Hit enemy: " + enemy.currentHealth);
                    // enemy.TakeDamage(damage);
                    //enemy.TakeDamage(damage);
                    //hit.transform.SendMessage("HitByRay");
                    //Debug.Log(hit.transform.name + " health: " + enemy.currentHealth);
                }
                
            }
        }
    }

    void aimRotation(){ 
        Vector3 worldAimTarget = targetPosition;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDir = worldAimTarget - transform.position;
        Quaternion rotation = Quaternion.LookRotation(aimDir,  Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
    }

    void laserInstance(){
        Destroy(laser);
        Vector3 aimDir = (targetPosition - firePoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(aimDir,  Vector3.up);
        laser = Instantiate(muzzle, firePoint.position, rotation);
        laser.transform.parent = transform;
        LaserScript = laser.GetComponent<Hovl_Laser>();
        LaserScript.MaxLength = 26.5f;
    }
}

    
    

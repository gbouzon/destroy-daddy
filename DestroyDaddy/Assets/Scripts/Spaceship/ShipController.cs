using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{

    Rigidbody rb;
    float speed;
    private int count;
    private bool thrustOn;
    float fuel;

    [SerializeField]
    ParticleSystem leftThrust;
    [SerializeField]
    ParticleSystem rightThrust;
    [SerializeField]
    ParticleSystem leftJet;
    [SerializeField]
    ParticleSystem rightJet;

    AudioSource audioSource;

    void Start()
    {
        Application.targetFrameRate = 30;
        fuel = 100000;
        count = 0;
        thrustOn = false;
        speed = 50f;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            MoveInDirectionOfInput();
            BothJets();
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(0, -0.3f, 0);
            RightJet();
        }

        if (Input.GetKey(KeyCode.S)) {
            transform.Rotate(-0.3f, 0, 0);
            BothJets();
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(0, 0.3f, 0);
            LeftJet();
        }
        
        if (Input.GetKey(KeyCode.F)) {
            fuel = 1000;
        }

        if (fuel > 0) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (count % 2 == 0) 
                    StartThrust();
                else 
                    EndThrust();
                count++;
            }
        }
        else
            EndThrust();

        if (thrustOn) {
            rb.AddForce(transform.forward * speed);
            fuel -= 0.2f;
            Debug.Log(fuel);
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
            StopJets();
        }
    }

    void StartThrust() {
        thrustOn = true;
        leftThrust.Play();
        rightThrust.Play();
        audioSource.Play();
    }

    void EndThrust() {
        thrustOn = false;
        leftThrust.Stop();
        rightThrust.Stop();
        audioSource.Stop();
    }

    void BothJets() {
        leftJet.Play();
        rightJet.Play();
        fuel -= 0.002f;
    }

    void LeftJet() {
        leftJet.Play();
        rightJet.Stop();
        fuel -= 0.002f;
    }

    void RightJet() {
        rightJet.Play();
        leftJet.Stop();
        fuel -= 0.002f;
    }

    void StopJets() {
        leftJet.Stop();
        rightJet.Stop();
    }

    public void MoveInDirectionOfInput() {
        Vector3 dir = Vector3.zero;
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        
        Vector3 camDirection = Camera.main.transform.rotation * dir; //This takes all 3 axes (good for something flying in 3d space)    
        Vector3 targetDirection = new Vector3(camDirection.x, camDirection.y, camDirection.z); //This line removes the "space ship" 3D flying effect. We take the cam direction but remove the y axis value
            
        if (dir != Vector3.zero) { //turn the character to face the direction of travel when there is input
            transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(targetDirection),
            Time.deltaTime * 0.6f
            );
        }
        
        //rb.velocity = targetDirection.normalized * 0.5f;     //normalized prevents char moving faster than it should with diagonal input
    }

    void OnTriggerEnter(Collider col) {
        SceneManager.LoadScene(col.gameObject.name);
        SceneManager.UnloadSceneAsync("Space");
    }
}

// void matchCamera(targetAngle, currentAngle) {

// }

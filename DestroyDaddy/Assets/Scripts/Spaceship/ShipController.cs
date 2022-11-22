using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{

    Rigidbody rb;
    public static float speed = 1000f;
    public static float maxFuel = 100f;
    private int count;
    private bool thrustOn;
    public static float fuel = float.MaxValue;
    public static int startCount = 0;

    public static string lastPlanet = "Earth";

    private static Dictionary<string, Vector3[]> planetPositions = new Dictionary<string, Vector3[]> {
        {"Earth", new Vector3[] {new Vector3(-66f, 1074f, 149f), new Vector3(0, 166.174f, 0)}},
        {"Moon", new Vector3[] {new Vector3(1394f, 1443f, 508.6f), new Vector3(0, 199.774f, 0)}},
        {"Planet1", new Vector3[] {new Vector3(3970f, 157.9f, 335.2f), new Vector3(0, 261.289f, 0)}},
        {"Planet2", new Vector3[] {new Vector3(1165.8f, 2364.5f, -4775.4f), new Vector3(0, 352.994f, 0)}},
        {"Planet3", new Vector3[] {new Vector3(6790f, 364.5f, 5856f), new Vector3(0, 547.437f, 0)}},
        {"Planet4", new Vector3[] {new Vector3(-3754.5f, 2206.5f, -8481.3f), new Vector3(-15.359f, 724.954f, 0)}},
        {"Planet5", new Vector3[] {new Vector3(-8366.3f, -3915f, 8606.7f), new Vector3(-19.14f, 564.746f, 0.576f)}},
    };

    [SerializeField]
    ParticleSystem leftThrust;
    [SerializeField]
    ParticleSystem rightThrust;
    [SerializeField]
    ParticleSystem leftJet;
    [SerializeField]
    ParticleSystem rightJet;
    [SerializeField]
    ParticleSystem explosion;

    AudioSource thrustSource;
    AudioSource jetSource;

    void Start()
    {
        Application.targetFrameRate = 30;
        if (startCount == 0)
        {
            lastPlanet = GameObject.Find("Earth").gameObject.name;
            fuel = maxFuel;
            startCount++;
        }
        if (MainMenu.pd != null)
        {
            if (MainMenu.pd.sceneName == "Space") {
                transform.position = new Vector3(MainMenu.pd.playerPosition[0], MainMenu.pd.playerPosition[1], MainMenu.pd.playerPosition[2]);
                transform.rotation = new Quaternion(MainMenu.pd.playerRotation[0], MainMenu.pd.playerRotation[1], 
                    MainMenu.pd.playerRotation[2], MainMenu.pd.playerRotation[3]);
            }
            else {
                lastPlanet = MainMenu.pd.lastPlanet;
                LoadPosition();
            }
            MainMenu.pd = null;
        }
        else    
            LoadPosition();
        count = 0;
        thrustOn = false;
        rb = GetComponent<Rigidbody>();
        thrustSource = GetComponent<AudioSource>();
        jetSource = leftJet.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            Cursor.lockState = CursorLockMode.None;
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
                else { 
                    EndThrust();
                }
                count++;
            }
        }
        else {
            EndThrust();
            rb.angularDrag = 0;
            rb.drag = 0;
        }

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
        thrustSource.Play();
    }

    void EndThrust() {
        thrustOn = false;
        leftThrust.Stop();
        rightThrust.Stop();
        thrustSource.Stop();
    }

    void BothJets() {
        leftJet.Play();
        rightJet.Play();
        if (!jetSource.isPlaying)
            jetSource.Play();
        fuel -= 0.002f;
    }

    void LeftJet() {
        leftJet.Play();
        rightJet.Stop();
        if (!jetSource.isPlaying)
            jetSource.Play();
        fuel -= 0.002f;
    }

    void RightJet() {
        rightJet.Play();
        leftJet.Stop();
        if (!jetSource.isPlaying)
            jetSource.Play();
        fuel -= 0.002f;
    }

    void StopJets() {
        leftJet.Stop();
        rightJet.Stop();
        jetSource.Stop();
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
    }

    void OnTriggerEnter(Collider col) {
        lastPlanet = col.gameObject.name;
        GameObject.Find("SpaceUI").SetActive(false);
        if (col.gameObject.name == "SunObject") {
            EndThrust();
            rb.drag = 1000;
            explosion.Play();
            explosion.GetComponent<AudioSource>().Play();
            StartCoroutine(waiter());
        }
        else {
            LevelLoader.sceneTransition = true;
            LevelLoader.nextScene = col.gameObject.name;
        }
    }

    IEnumerator waiter() {
        yield return new WaitForSecondsRealtime(1.8f);
        Destroy(gameObject);
    }

    void LoadPosition() {
        Vector3[] positions = planetPositions[lastPlanet];
        transform.position = positions[0];
        transform.rotation = Quaternion.Euler(positions[1]);
        transform.localScale = new Vector3(3f, 3f, 3f);
    }
}

// void matchCamera(targetAngle, currentAngle) {

// }

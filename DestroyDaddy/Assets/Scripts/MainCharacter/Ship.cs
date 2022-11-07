using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject enterShipCanvas;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopCollision : MonoBehaviour
{
    [SerializeField]
    GameObject enterShopCanvas;
    [SerializeField]
    GameObject shopCanvas;
    [SerializeField]
    GameObject gunCrossHair;
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.name == "Shop"){
            enterShopCanvas.SetActive(true);
        }
    }

    void OnTriggerStay(Collider col) {
        if(col.gameObject.name == "Shop"){
            if (Input.GetKey(KeyCode.F)) {
                shopCanvas.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                gunCrossHair.SetActive(false);
            }
            if (Input.GetKey(KeyCode.Escape)) {
                shopCanvas.SetActive(false);
                Cursor.visible = false;
                gunCrossHair.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if(col.gameObject.name == "Shop"){
            enterShopCanvas.SetActive(false);
        }
    }
}

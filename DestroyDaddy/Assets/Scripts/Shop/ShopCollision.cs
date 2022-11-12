using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopCollision : MonoBehaviour
{
    [SerializeField]
    GameObject enterShopCanvas;
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.name == "Shop"){
            enterShopCanvas.SetActive(true);
        }
    }

    void OnTriggerStay(Collider col) {
        if(col.gameObject.name == "Shop"){
            if (Input.GetKey(KeyCode.F)) {
                //open canvas for shopping
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if(col.gameObject.name == "Shop"){
            enterShopCanvas.SetActive(false);
        }
    }
}

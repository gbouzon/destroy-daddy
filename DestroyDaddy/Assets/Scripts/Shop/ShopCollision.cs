using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopCollision : MonoBehaviour
{
    [SerializeField]
    GameObject enterShopCanvas;
    [SerializeField]
    GameObject fuelRechargeCanvas;
    [SerializeField]
    GameObject popUpWindow;
    [SerializeField]
    GameObject shop;
    // [SerializeField]
    // GameObject gunCrossHair;

    void Update() {
        Debug.Log("Max Fuel: " + ShipController.maxFuel);
        Debug.Log("Current Fuel: " + ShipController.fuel);

    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.name == "ShopTrigger"){
            enterShopCanvas.SetActive(true);
        }
        if (col.gameObject.name == "FuelTrigger" || col.gameObject.name == "FuelRecharge") {
            fuelRechargeCanvas.SetActive(true);
        }
    }

    void OnTriggerStay(Collider col) {
        if(col.gameObject.name == "ShopTrigger"){
            if (Input.GetKey(KeyCode.F)) {
                enterShopCanvas.SetActive(false);
                // gunCrossHair.SetActive(false);
                shop.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
               
            }
        }
        if (col.gameObject.name == "FuelTrigger" || col.gameObject.name == "FuelRecharge") {
            if (Input.GetKey(KeyCode.F)) {
                fuelRechargeCanvas.SetActive(false);
                // gunCrossHair.SetActive(false);
                ShipController.fuel = ShipController.maxFuel;
                Debug.Log("Current Fuel: " + ShipController.fuel);
                popUpWindow.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        if (Input.GetKey(KeyCode.Escape)) {
            popUpWindow.SetActive(false);
            Cursor.visible = false;
            // gunCrossHair.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col) {
        if (shop != null) {
            enterShopCanvas.SetActive(false);
            shop.SetActive(false);
        }
        popUpWindow.SetActive(false);
        fuelRechargeCanvas.SetActive(false);
      
    }
    public void SetTimeScale() {
        Time.timeScale = 1;
    }
}

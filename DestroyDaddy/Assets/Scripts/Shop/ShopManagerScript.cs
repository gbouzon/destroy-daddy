using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShopManagerScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MoneyText;
    public List<string> PurchasedItems = new List<string>();
    private int price1 = 500;
    private int price2 = 1000;
    private int price3 = 1500;
    private int price4 = 2000;
    [SerializeField] private GameObject success;
    [SerializeField] private GameObject fail;
 
    void Start()
    {
        //AddCash();
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = "Cash : " + PlayerCurrency.money;
    }

    public void AddCash() {
        PlayerCurrency.money += 1000;
    }

    public void greenUpgrade() {
        if (!PurchasedItems.Contains("greenUpgrade") && PlayerCurrency.money >= price1) {
            PlayerCurrency.money -= 500;
            ShipController.maxFuel = 300f;
            ShipController.speed = 1100f;
            ShipController.fuel = ShipController.maxFuel;
            PurchasedItems.Add("greenUpgrade");
            Debug.Log("Current Fuel: " + ShipController.fuel);
            Debug.Log("Current Speed: " + ShipController.speed);
            ShipController.materialPath = "Materials/ThrustFlameGreen";
            success.SetActive(true);
            fail.SetActive(false);
        }   
        else {
            fail.SetActive(true);
            success.SetActive(false);
        }
      
        //     success.SetActive(true);
        //     fail.SetActive(false);
        // }
        // if (PurchasedItems.Contains("greenUpgrade") || PlayerCurrency.money < price1) {
        //     fail.SetActive(true);
        // }
        

        // if (PlayerCurrency.money < price1) {
        //     fail.SetActive(true);)
        // }
    }

    public void pinkUpgrade() {
        if (!PurchasedItems.Contains("pinkUpgrade") && PlayerCurrency.money >= price2) {
            PlayerCurrency.money -= 1000;
            ShipController.maxFuel = 400f;
            ShipController.speed = 1200f;
            PurchasedItems.Add("pinkUpgrade");
            Debug.Log("Current Fuel: " + ShipController.fuel);
            Debug.Log("Current Speed: " + ShipController.speed);
            ShipController.materialPath = "Materials/ThrustFlamePink";
            success.SetActive(true);
            fail.SetActive(false);
        } 
        else {
            fail.SetActive(true);
            success.SetActive(false);
        }
    }

    public void purpleUpgrade() {
        if (!PurchasedItems.Contains("purpleUgrade") && PlayerCurrency.money >= price3) {
            PlayerCurrency.money -= 1500;
            ShipController.maxFuel = 700f;
            ShipController.speed = 1300f;
            Debug.Log("Current Fuel: " + ShipController.fuel);
            Debug.Log("Current Speed: " + ShipController.speed);
            PurchasedItems.Add("purpleUpgrade");
            ShipController.materialPath = "Materials/ThrustFlamePurple";
            success.SetActive(true);
            fail.SetActive(false);

        }
        else {
            fail.SetActive(true);
            success.SetActive(false);
        }
    }

    public void blueUpgrade() {
        if (!PurchasedItems.Contains("blueUpgrade") && PlayerCurrency.money >= price4) {
            PlayerCurrency.money -= 2000;
            ShipController.maxFuel = 1000f;
            ShipController.speed = 1400f;
            Debug.Log("Current Fuel: " + ShipController.fuel);
            Debug.Log("Current Speed: " + ShipController.speed);
            PurchasedItems.Add("blueUpgrade");
            ShipController.materialPath = "Materials/ThrustFlameBlue";
            success.SetActive(true);
            fail.SetActive(false);
        }
        else {
            fail.SetActive(true);
            success.SetActive(false);
        }
    }

}
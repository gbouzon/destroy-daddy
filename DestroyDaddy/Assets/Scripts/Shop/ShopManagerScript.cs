using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShopManagerScript : MonoBehaviour
{
    public static int MoneyAmount;
    public static int MaxMoneyAmount= 10000;
    [SerializeField] private int AddMoneyAmount;
    [SerializeField] private TextMeshProUGUI MoneyText;
    public List<string> PurchasedItems = new List<string>();
    // [SerializeField]  private TextMeshProUGUI BoughtList;

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (MoneyAmount > MaxMoneyAmount)
        {
            Debug.Log("TOO MUCH MONEY");
        }
        MoneyText.text = "Cash : "+ MoneyAmount;
        // BoughtList.text = "Bought : " + PurchasedItems.ToString();
    }

    public void AddCash() {
        MoneyAmount += AddMoneyAmount;
    }

    public void greenUpgrade() {
        MoneyAmount -= 500;
        ShipController.maxFuel = 300f;
        ShipController.speed = 1100f;
        ShipController.fuel = ShipController.maxFuel;
        PurchasedItems.Add("greenUpgrade");
        Debug.Log("Current Fuel: " + ShipController.fuel);
        Debug.Log("Current Speed: " + ShipController.speed);
        ShipController.materialPath = "Materials/ThrustFlameGreen";
        // BoughtList.text = BoughtList.text + "Upgrade1";
    }

    public void pinkUpgrade() {
        MoneyAmount -= 1000;
        ShipController.maxFuel = 400f;
        ShipController.speed = 1200f;
        PurchasedItems.Add("pinkUpgrade");
        Debug.Log("Current Fuel: " + ShipController.fuel);
        Debug.Log("Current Speed: " + ShipController.speed);
        ShipController.materialPath = "Materials/ThrustFlamePink";
        //BoughtList.text = BoughtList.text + "Upgrade2";
    }

    public void purpleUpgrade() {
        MoneyAmount -= 1500;
        ShipController.maxFuel = 700f;
        ShipController.speed = 1300f;
        Debug.Log("Current Fuel: " + ShipController.fuel);
        Debug.Log("Current Speed: " + ShipController.speed);
        PurchasedItems.Add("purpleUpgrade");
        ShipController.materialPath = "Materials/ThrustFlamePurple";
     //   BoughtList.text = BoughtList.text + "Upgrade3";
    }

    public void blueUpgrade() {
        MoneyAmount -= 2000;
        ShipController.maxFuel = 1000f;
        ShipController.speed = 1400f;
        Debug.Log("Current Fuel: " + ShipController.fuel);
        Debug.Log("Current Speed: " + ShipController.speed);
        PurchasedItems.Add("blueUpgrade");
        ShipController.materialPath = "Materials/ThrustFlameBlue";
     //   BoughtList.text = BoughtList.text + "Upgrade4";
    }
}

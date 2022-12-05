using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCurrency : MonoBehaviour
{
    public static int money = 1000;
    //public GameObject UIText;

    public TextMeshProUGUI UIText; 
    // Start is called before the first frame update
    void Start()
    {
        UIText.text = money + "$"; 
    }

    // Update is called once per frame
    void Update()
    {
        UIText.text = money + "$";
    }

    public void addMoney(int amount){
        money += amount;
    }

    public void removeMoney(int amount){
        money -= amount;
    }

}

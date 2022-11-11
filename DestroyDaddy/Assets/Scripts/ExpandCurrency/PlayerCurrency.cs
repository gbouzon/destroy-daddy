using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    public int money = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10,10,150,50), "Money: " + money);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarLogic : MonoBehaviour
{
    private Image fuelBar;
    
    [SerializeField]
    private Sprite highFuel;

    [SerializeField]
    private Sprite mediumFuel;

    [SerializeField]
    private Sprite lowFuel;

    [SerializeField]
    private float fuel;

    void Start()
    {
        fuelBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        fuel = ShipController.fuel;
        fuelBar.fillAmount = fuel / ShipController.maxFuel;
        if (fuelBar.fillAmount <= 0.5f && fuelBar.fillAmount > 0.25f)
            fuelBar.sprite = mediumFuel;
        else if (fuelBar.fillAmount <= 0.25f)
            fuelBar.sprite = lowFuel;
        else
            fuelBar.sprite = highFuel;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public float getHealthValue(){
        return slider.value;
    }
    public void SetMaxHealthValue(int health){
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealthValue(int health){
        slider.value = health;
    }
}

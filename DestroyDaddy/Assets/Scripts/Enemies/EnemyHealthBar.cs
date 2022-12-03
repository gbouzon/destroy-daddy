using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    
    public void setMaxEnemyHealth(int max)
    {
        slider.maxValue = max;
        slider.value = max;
    }
    public void setEnemyHealth(int health)
    {
        slider.value = health;
    }
}

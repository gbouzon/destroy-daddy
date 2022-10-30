using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int maxHealth = 50; 
    public int currentHealt; 
    // Start is called before the first frame update
    void OnEnable(){
            currentHealt = maxHealth;
    }

    public void TakeDamage(int damage){
        currentHealt -= damage;
        if(currentHealt <= 0){
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

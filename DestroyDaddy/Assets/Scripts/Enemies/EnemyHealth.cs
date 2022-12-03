using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject Money;
    [SerializeField]private GameObject Degud;
    int maxHealth = 100; 

    Vector3 randomPostion;
    public int currentHealth; 
    // Start is called before the first frame update
    float radius = 2.5f;

    void start(){
        
    }
    void OnEnable(){
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        if(currentHealth <= 0){
            gameObject.SetActive(false);
            Debug.Log(GameObject.Find("GoldCoin"));
            GameObject.Find("GoldCoin").GetComponentInChildren<Currency>().SpawnCurreny(transform, 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
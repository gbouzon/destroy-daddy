using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    float radius = 2.5f;
    public int valueOfCurrency = 10; 
    [SerializeField]
    private GameObject prefab;

    void Start(){
        prefab = GameObject.Find("GoldCoin");
    }

    void Update(){
        StartCoroutine(delele());
    }


    void OnTriggerEnter(Collider collider){
        if(collider.tag == "Player"){
            collider.gameObject.GetComponent<PlayerCurrency>().addMoney(valueOfCurrency);
            Destroy(gameObject);
        }
    }

    public void SpawnCurreny(Transform enemy, int amount){
        for(int i= 0; i < amount; i++){
            Vector3 newPosition = enemy.position;
            newPosition.x += Random.Range(-radius, radius);
            newPosition.y += Random.Range(-radius, radius);
            GameObject money = Instantiate(prefab, newPosition, prefab.transform.rotation);
        }
    }

    IEnumerator delele(){
        yield return new WaitForSeconds(60);
        Destroy(gameObject);    
    }    
}

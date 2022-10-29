using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        if(other.name == "MainCharacter")
        {
            other.GetComponent<PlayerCurrency>().money++;
            Destroy(gameObject);
        }
    }
   
}

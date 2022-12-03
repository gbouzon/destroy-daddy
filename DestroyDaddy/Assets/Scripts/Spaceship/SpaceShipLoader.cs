using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipLoader : MonoBehaviour
{
    public GameObject skinToLoad;
    
    // Start is called before the first frame update
    void Awake()
    {
        GameObject clone = Instantiate(skinToLoad, transform);
        clone.name = "Ship";
    }

}

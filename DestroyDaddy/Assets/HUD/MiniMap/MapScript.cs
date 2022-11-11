using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    [SerializeField]
    GameObject miniMap;
    [SerializeField]
    GameObject bigMap;
    [SerializeField]
    GameObject fuelBar;
    void Start()
    {
        bigMap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            bigMap.SetActive(true);
            miniMap.SetActive(false);
            fuelBar.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.M)) {
            bigMap.SetActive(false);
            miniMap.SetActive(true);
            fuelBar.SetActive(true);
        }
    }
}

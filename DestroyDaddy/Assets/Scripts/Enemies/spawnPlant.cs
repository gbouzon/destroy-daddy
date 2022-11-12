using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlant : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private float respawnTime = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enemyWave());
    }

    private void spawnPlantMonster()
    {
        GameObject plant = Instantiate(enemyPrefab) as GameObject;
        GameObject player = GameObject.FindWithTag("Player");
        Transform target = player.GetComponent<Transform>();
        //place plant monster near main character
        Vector3 newPlantPosition = new Vector3(target.position.x+20, target.position.y+20, target.position.z+20);
        plant.transform.position = newPlantPosition;
    }
    IEnumerator enemyWave()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnPlantMonster();
        }
    }
}

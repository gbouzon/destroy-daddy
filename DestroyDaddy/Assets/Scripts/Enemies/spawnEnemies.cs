using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private float respawnTime = 2.0f;
    private Vector3 location1 = new Vector3(800, 36, 420);
    private Vector3 location2 = new Vector3(902, 89, 462);
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enemyWave());
    }

    private void spawnEnemy()
    {
        GameObject bug = Instantiate(enemyPrefab) as GameObject;
        int spawnLocation = Random.Range(0, 2);
        switch (spawnLocation)
        {
            case (0):
                bug.transform.position = location1;
                break;
            case (1):
                bug.transform.position = location2;
                break;
        }
    }

    /*private void spawnPlantMonster()
    {
        GameObject plant = Instantiate(plantPrefab) as GameObject;
        GameObject player = GameObject.FindWithTag("Player");
        Transform target = player.GetComponent<Transform>();
        //place plant monster near main character
        Vector3 newPlantPosition = new Vector3(target.position.x-5, target.position.y+5, target.position.z-5);
        plant.transform.position = newPlantPosition;
    }*/
    IEnumerator enemyWave()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
    }
}
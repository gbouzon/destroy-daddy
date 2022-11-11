using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour
{
    [SerializeField]
    private GameObject bugPrefab;
    [SerializeField]
    private GameObject plantPrefab;
    [SerializeField]
    private GameObject diatrymaPrefab;
    [SerializeField]
    private GameObject slothPrefab;
    [SerializeField]
    private GameObject dragonPrefab;
    private float respawnTime = 2.0f;
    private float respawnTimePlant = 5.0f;
    private Vector3 location1 = new Vector3(800, 30, 420);
    private Vector3 location2 = new Vector3(645, 40, 225);
    private Vector3 location3 = new Vector3(480, 25, 290);
    // Start is called before the first frame update
    void Start()
    {
        //bugPrefab = GameObject.FindWithTag("bugPrefab");

        StartCoroutine(enemyWave());
        StartCoroutine(plantWave());
        //bugPrefab.tag = "Enemy";
        //bugPrefab.AddComponent<EnemyHealth>();
    }

    private void spawnEnemy()
    {
        GameObject bug = Instantiate(bugPrefab) as GameObject;
        /*GameObject diatryma = Instantiate(diatrymaPrefab) as GameObject;
        GameObject sloth = Instantiate(slothPrefab) as GameObject;
        GameObject dragon = Instantiate(dragonPrefab) as GameObject;*/
        int spawnLocation = Random.Range(0, 3);
        switch (spawnLocation)
        {
            case (0):
                bug.transform.position = location1;
                break;
            case (1):
                bug.transform.position = location2;
                break;
            case (2):
                bug.transform.position = location3;
                break;
        }
    }

    private void spawnPlantMonster()
    {
        GameObject plant = Instantiate(plantPrefab) as GameObject;
        GameObject player = GameObject.FindWithTag("Player");
        Transform target = player.GetComponent<Transform>();
        //place plant monster near main character
        Vector3 newPlantPosition = new Vector3(target.position.x-5, target.position.y+5, target.position.z-5);
        plant.transform.position = newPlantPosition;
    }
    IEnumerator enemyWave()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
    }
    IEnumerator plantWave()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(respawnTimePlant);
            spawnPlantMonster();
        }
    }
}
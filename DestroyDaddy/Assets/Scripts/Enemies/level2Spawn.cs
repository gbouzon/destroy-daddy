using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private int firstWaveAmount = 5;
    private int secondWaveAmount = 7;
    private float respawnTime;
    private int firstWave = 0;
    private int secondWave = 0;
    private bool isCalled;
    private bool isInFirstWave;
    private bool isNextWaveReady;
    private GameObject[] enemiesLeft;
    // Start is called before the first frame update
    void Start()
    {
        isInFirstWave = true;
        isNextWaveReady = false;
        isCalled = false;
        StartCoroutine(enemyWave1());
    }
    void Update()
    {
        enemiesLeft = GameObject.FindGameObjectsWithTag("enemyPrefab");
        if (enemiesLeft.Length == 0)
        {
            isNextWaveReady = true;
        }
        else
        {
            isNextWaveReady = false;
        }
        if (isNextWaveReady == true && isInFirstWave == false && isCalled == false)
        {
            isCalled = true;
            StartCoroutine(enemyWave2());
        }
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
    IEnumerator enemyWave1()
    {
        respawnTime = Random.Range(2.0f, 15.0f);
        for (firstWave = 0; firstWave < firstWaveAmount; firstWave++)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnPlantMonster();
        }
        isInFirstWave = false;
    }

    IEnumerator enemyWave2()
    {
        Debug.Log("in second wave");
        respawnTime = Random.Range(2.0f, 7.0f);
        for (secondWave = 0; secondWave < secondWaveAmount; secondWave++)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnPlantMonster();
        }
    }
}

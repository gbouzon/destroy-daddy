using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private int firstWaveAmount = 1;
    private int secondWaveAmount = 7;
    private float respawnTime;
    private int firstWave = 0;
    private int secondWave = 0;
    private bool isCalled;
    private bool isInFirstWave;
    private bool isNextWaveReady;
    private GameObject[] enemiesLeft;
    private Vector3 location1 = new Vector3(821, 30, 420);
    private Vector3 location2 = new Vector3(902, 89, 462);
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
    private void spawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab) as GameObject;
        int spawnLocation = Random.Range(0, 2);
        switch (spawnLocation)
        {
            case (0):
                enemy.transform.position = location1;
                break;
            case (1):
                enemy.transform.position = location2;
                break;
        }
    }
    IEnumerator enemyWave1()
    {
        respawnTime = Random.Range(2.0f, 15.0f);
        for (firstWave = 0; firstWave < firstWaveAmount; firstWave++)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
        isInFirstWave = false;
    }
    IEnumerator enemyWave2()
    {
        respawnTime = Random.Range(2.0f, 7.0f);
        for (secondWave = 0; secondWave < secondWaveAmount; secondWave++)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
    }
}
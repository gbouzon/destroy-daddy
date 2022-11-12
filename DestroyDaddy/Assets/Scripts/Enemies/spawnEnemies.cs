using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private int enemyAmount = 5;
    private float respawnTime = 5.0f;
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
    IEnumerator enemyWave()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour
{

    public GameObject bugPrefab;
    private float respawnTime = 2.0f;
    int pickSpawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        //bugPrefab = GameObject.FindWithTag("bugPrefab");
        StartCoroutine(enemyWave());
    }

    private void spawnEnemy()
    {
        GameObject a = Instantiate(bugPrefab) as GameObject;
        //this is where i randomize it's spawning position
        pickSpawnLocation = Random.Range(0, 3);
        switch (pickSpawnLocation)
        {
            case 0:
                a.transform.position = new Vector3(700,120,400);
                break;
            case 1:
                a.transform.position = new Vector3(815, 25, 105);
                break;
            case 2:
                a.transform.position = new Vector3(920, 28, 270);
                break;
            default:
                a.transform.position = new Vector3(555, 75, 315);
                break;
        }
        
    }
    IEnumerator enemyWave()
    {
        for(int i=0; i < 10; i++)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
        
    }
}

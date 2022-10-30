using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour
{

    public GameObject bugPrefab;
    private float respawnTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        //bugPrefab = GameObject.FindWithTag("bugPrefab");
      
        StartCoroutine(enemyWave());
        bugPrefab.AddComponent<EnemyHealth>();
    }

    void Update() {
        bugPrefab.tag = "Enemy";
    }
    private void spawnEnemy()
    {
        GameObject a = Instantiate(bugPrefab) as GameObject;
        //this is where i randomize it's spawning position
        a.transform.position = new Vector3(Random.Range(600f, 800f), Random.Range(100f, 130f), Random.Range(300f, 400f));
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

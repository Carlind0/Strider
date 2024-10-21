using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

   public float spawnRate = 3f;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(Spawner(spawnRate, EnemyPrefab));
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Spawner(float spawnRate, GameObject EnemyPrefab)
    {
       yield return new WaitForSeconds(spawnRate);
        GameObject newEnemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        StartCoroutine(Spawner(spawnRate, EnemyPrefab));
    }
}

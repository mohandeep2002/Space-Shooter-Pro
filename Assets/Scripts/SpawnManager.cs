using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] powerups;
   
    public void StartSpawing()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRouting());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-27.6f, 27.6f), 18f, 27.7f);
            //Vector3 posToSpawn2 = new Vector3(Random.Range(27.6f, 0), 22f, 27.7f);
            GameObject newEnemy_one_half = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            //GameObject newEnemy_second_half = Instantiate(_enemyPrefab, posToSpawn2, Quaternion.identity);
            newEnemy_one_half.transform.parent = _enemyContainer.transform;
            //newEnemy_second_half.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(4.0f);      
        }
    }
    IEnumerator SpawnPowerUpRouting()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-27.6f, 28.0f), 18f, 27.7f);
            int randomPowerUpPrefab = Random.Range(0, 3); 
            Instantiate(powerups[randomPowerUpPrefab], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f, 8f));
        }
    }
    public void OnplayerDeath()
    {
        _stopSpawning = true;
    }
}

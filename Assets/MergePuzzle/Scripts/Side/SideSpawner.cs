using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSpawner : MonoBehaviour
{
    [Header("Platforms")]
    [SerializeField] private ObjectPool platformPool;
    [SerializeField] private Transform platformSpawnPoint;

    [Header("Enemies")]
    [SerializeField] private EnemyObjectPool enemyPool;
    [SerializeField] private Transform enemySpawnPoint;

    [Header("Heroes")]
    [SerializeField] private List<GameObject> heroPrefabs;
    [SerializeField] private List<Transform> heroSpawnPoint;

    [SerializeField] private SideDespawner sideDespawner;
    [SerializeField] private EnemyDespawner enemyDespawner;

    [SerializeField] private PlayerLeveling leveling;

    private void Awake()
    {
        sideDespawner.DespawnedPlatformEvent += Despawner_DespawnedPlatformEvent;
        enemyDespawner.DespawnedEnemyEvent += Despawner_DespawnedEnemyEvent;

        var instance = Instantiate(heroPrefabs[0], heroSpawnPoint[0].position, Quaternion.identity);
        int nextPosition = 1;

        if (PlayerPrefs.GetInt(Constants.HUMAN_UNLOCK_BOOL) > 0)
        {
            instance = Instantiate(heroPrefabs[1], heroSpawnPoint[nextPosition].position, Quaternion.identity);
            nextPosition++;
        }
        if (PlayerPrefs.GetInt(Constants.ELF_UNLOCK_BOOL) > 0)
        {
            instance = Instantiate(heroPrefabs[2], heroSpawnPoint[nextPosition].position, Quaternion.identity);
        }

        StartCoroutine(SpawnStartEnemies());
    }

    private void Despawner_DespawnedEnemyEvent(object sender, System.EventArgs e)
    {
        SpawnEnemy();
    }

    private IEnumerator SpawnStartEnemies()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i< 3; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(2f);
        }
    }

    private void SpawnEnemy()
    {
        var enemy = enemyPool.GetObject();
        if (enemy != null)
        {
            enemy.gameObject.SetActive(true);
            enemy.transform.position = enemySpawnPoint.position;
        }
    }

    private void Despawner_DespawnedPlatformEvent(object sender, System.EventArgs e)
    {
        AddPlatform();
    }

    private void AddPlatform()
    {
        var platform = platformPool.GetObject();
        if (platform != null)
        {
            platform.gameObject.SetActive(true);
            platform.transform.position = platformSpawnPoint.position;
        }
    }


}

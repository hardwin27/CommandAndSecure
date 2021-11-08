using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    [SerializeField] private List<GameObject> enemyPrefabs;
    private int enemyAmount = 5;
    private int enemyIndex = -1;
    private GameObject enemyPrefab;
    private float spawnInterval;
    private float spawnTimer = 0f;
    private int enemySpawnerIndex;

    private bool isSpawning = false;

    private void Start()
    {
        UpdateSelectedEnemy();
        isSpawning = true;
    }

    private void Update()
    {
        if(!isSpawning)
        {
            return;
        }

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
        }
    }

    private void UpdateSelectedEnemy()
    {
        enemyIndex++;

        if(enemyIndex >= enemyAmount)
        {
            isSpawning = false;
            return;
        }

        enemyPrefab = enemyPrefabs[GameManager.Instance.enemiesData[enemyIndex].EnemyTypeNum];
        spawnInterval = GameManager.Instance.enemiesData[enemyIndex].SpawnInterval;
        spawnTimer = spawnInterval;
        enemySpawnerIndex = GameManager.Instance.enemiesData[enemyIndex].SpawnTileId;
    }

    private void SpawnEnemy()
    {
        gridManager.GetEnemySpawnTiles()[enemySpawnerIndex].SpawnEnemy(enemyPrefab, transform);
        UpdateSelectedEnemy();
    }
}

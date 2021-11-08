using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTile : Tile
{
    protected override void Awake()
    {
        base.Awake();
        isLowground = true;
    }

    protected override void Start()
    {

    }

    public void SpawnEnemy(GameObject enemyPrefab, Transform parent)
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity, parent);
    }
}

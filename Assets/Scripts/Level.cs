using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public List<string> mapData;
    public List<Door> doorsData;
    public List<Vector2Int> enemySpawnTileIndexs;
    public Vector2Int commanderInitTile;
    public float commanderInitHealth;
    public int agentLimit;
    public int initialPhotonAmount;
    public List<EnemyToSpawn> enemiesData;
}

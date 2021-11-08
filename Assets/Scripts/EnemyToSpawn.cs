using System.Collections;
using System.Collections.Generic;

public class EnemyToSpawn
{
    private int enemyTypeNum;
    private float spawnInterval;
    private int spawnTileId;

    public int EnemyTypeNum { get { return enemyTypeNum; } }
    public float SpawnInterval { get { return spawnInterval; } }
    public int SpawnTileId { get { return spawnTileId; } }

    public EnemyToSpawn(int typeNum, float interval, int tileId)
    {
        enemyTypeNum = typeNum;
        spawnInterval = interval;
        spawnTileId = tileId;
    }
}

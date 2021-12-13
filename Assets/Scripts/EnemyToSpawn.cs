using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemyToSpawn
{
    public int enemyTypeNum;
    public float spawnInterval;
    public int spawnTileId;

    /*public int EnemyTypeNum { get { return enemyTypeNum; } }
    public float SpawnInterval { get { return spawnInterval; } }
    public int SpawnTileId { get { return spawnTileId; } }

    public EnemyToSpawn(int typeNum, float interval, int tileId)
    {
        enemyTypeNum = typeNum;
        spawnInterval = interval;
        spawnTileId = tileId;
    }*/
}

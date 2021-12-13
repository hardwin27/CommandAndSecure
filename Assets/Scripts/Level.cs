using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public List<string> mapData;
    public List<Door> doorsData;
    public List<Vector2Int> enemySpawnTileIndexs;
    public Vector2 commanderInitTile;
    public float commanderInitHealth;
    public int agentLimit;
    public int initialPhotonAmount;
    public List<EnemyToSpawn> enemiesData;

    /*public LeveD:\Projects\GameProject\CommandAndSecure\Assets\Database\Level.jsonl(char[,] _mapData, Object[] _doorsData, int[,] _enemySpawnTileIndexs, string _commanderInitTile, 
        int _commanderIntHealth, int _initPhotonAmount, float [,] _enemiesData)
    {
        mapData = _mapData;

        foreach(Object doorData in _doorsData)
        {
            
        }
    }*/
}
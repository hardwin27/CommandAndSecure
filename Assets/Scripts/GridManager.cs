using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    private static GridManager _instance = null;
    public static GridManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GridManager>();
            }
            return _instance;
        }
    }

    private Grid grid;
    [SerializeField] private GameObject tilemap;

    [Header("ObjectPrefab")]
    [SerializeField] private GameObject lowgroundTilePrefab;
    [SerializeField] private GameObject highgroundTilePrefab;
    [SerializeField] private GameObject commanderTilePrefab;
    [SerializeField] private GameObject enemySpawnereTilePrefab;
    [SerializeField] private GameObject damageOverTimeTilePrefab;
    [SerializeField] private GameObject doorTilePrefab;

    private Tile[,] tiles = new Tile[10, 10];
    private List<EnemySpawnerTile> enemySpawnerTiles = new List<EnemySpawnerTile>();
    private List<DoorTile> doorTiles = new List<DoorTile>();

    [SerializeField] private Commander commander;
    //Index reference the position of commander in the Tilemap
    private Vector2Int commanderIndex = Vector2Int.zero;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Start()
    {
        LoadMap();
        AddTilesToArray();
        AddEnemySpawnerTileToList();
        AddDoorTileToList();
        UpdateCurrentCommanderTile();
        InitiateNeighborsForTiles();
        GoalbasedPathfinding();
    }

    private void LoadMap()
    {
        float startX = -4.5f;
        float startY = 4.5f;

        for (int indX = 0; indX < 10; indX++)
        {
            for (int indY = 0; indY < 10; indY++)
            {
                GameObject tempTile;
                switch(GameManager.Instance.mapData[indX,indY])
                {
                    case "#":
                        tempTile =  Instantiate(highgroundTilePrefab, 
                            grid.WorldToCell(new Vector3(startX, startY, 0)), Quaternion.identity, tilemap.transform);
                        break;
                    case "X":
                        tempTile = Instantiate(enemySpawnereTilePrefab, 
                            grid.WorldToCell(new Vector3(startX, startY, 0)), Quaternion.identity, tilemap.transform);
                        break;
                    case "V":
                        tempTile = Instantiate(commanderTilePrefab, 
                            grid.WorldToCell(new Vector3(startX, startY, 0)), Quaternion.identity, tilemap.transform);
                        break;
                    case "!":
                        tempTile = Instantiate(damageOverTimeTilePrefab, 
                            grid.WorldToCell(new Vector3(startX, startY, 0)), Quaternion.identity, tilemap.transform);
                        break;
                    case "?":
                        tempTile = Instantiate(doorTilePrefab, 
                            grid.WorldToCell(new Vector3(startX, startY, 0)), Quaternion.identity, tilemap.transform);
                        break;
                    default:
                        tempTile = Instantiate(lowgroundTilePrefab, 
                            grid.WorldToCell(new Vector3(startX, startY, 0)), Quaternion.identity, tilemap.transform);
                        break;
                }
                tempTile.transform.position = new Vector3(tempTile.transform.position.x - 0.5f, tempTile.transform.position.y + 0.5f, 0f);
                startX += 1f;
            }
            startX = -4.5f;
            startY -= 1f;
        }
        commanderIndex = GameManager.Instance.commanderInitTile;
    }

    private void AddTilesToArray()
    {
        int counterX = 0;
        int counterY = 0;
        foreach (Transform child in tilemap.transform)
        {
            Tile tempTile = child.GetComponent<Tile>();

            tiles[counterX, counterY] = tempTile;
            tempTile.SetIndex(new Vector2Int(counterX, counterY));
            counterX++;
            if (counterX >= 10)
            {
                counterX = 0;
                counterY++;
            }
        }
    }

    private void AddEnemySpawnerTileToList()
    {
        foreach(Vector2Int index in GameManager.Instance.enemySpawnTileIndexs)
        {
            try
            {
                enemySpawnerTiles.Add(tiles[index.x, index.y].gameObject.GetComponent<EnemySpawnerTile>());
            }
            catch
            {
                print("Error on EnemySpawnerData");
            }
        }
    }

    private void AddDoorTileToList()
    {
        foreach(Door doorData in GameManager.Instance.doorsData)
        {
            try
            {
                DoorTile tempDoorTile = tiles[doorData.doorIndex.x, doorData.doorIndex.y].gameObject.GetComponent<DoorTile>();
                tempDoorTile.SetGroupId(doorData.groupId);
                tempDoorTile.SetIsLowGround(doorData.initiallyOpen);
                doorTiles.Add(tempDoorTile);
            }
            catch
            {
                print("ERROR on DoorData");
                continue;
            }
        }
    }

    private void UpdateCurrentCommanderTile()
    {
        tiles[commanderIndex.x, commanderIndex.y].GetComponent<CommanderTile>().SetIsCurrentCommanderPosition(true);
        commander.transform.position = tiles[commanderIndex.x, commanderIndex.y].transform.position;
    }

    public List<EnemySpawnerTile> GetEnemySpawnTiles()
    {
        return enemySpawnerTiles;
    }

    private void InitiateNeighborsForTiles()
    {
        for (int indX = 0; indX < 10; indX++)
        {
            for (int indY = 0; indY < 10; indY++)
            {
                if (indX > 0)
                {
                    //left
                    tiles[indX, indY].AddNeighbor(tiles[indX - 1, indY]);
                }
                if (indX < 10 - 1)
                {
                    //right
                    tiles[indX, indY].AddNeighbor(tiles[indX + 1, indY]);
                }
                if (indY > 0)
                {
                    //up
                    tiles[indX, indY].AddNeighbor(tiles[indX, indY - 1]);
                }
                if (indY < 10 - 1)
                {
                    //down
                    tiles[indX, indY].AddNeighbor(tiles[indX, indY + 1]);
                }
            }
        }
    }

    private void GoalbasedPathfinding()
    {
        GenerateHeatmap();
        GenerateFlowField();
    }

    private void GenerateHeatmap()
    {
        for (int indX = 0; indX < 10; indX++)
        {
            for (int indY = 0; indY < 10; indY++)
            {
                tiles[indX, indY].SetMark(false);
            }
        }
        tiles[commanderIndex.x, commanderIndex.y].SetMark(true);
        tiles[commanderIndex.x, commanderIndex.y].SetDistance(0);
        Queue<Tile> queue = new Queue<Tile>();
        queue.Enqueue(tiles[commanderIndex.x, commanderIndex.y]);
        while (queue.Count != 0)
        {
            Tile currentTile = queue.Dequeue();
            List<Tile> currentNeighbors = currentTile.GetNeighbors();
            if (currentNeighbors.FindIndex(t => t.GetIsLowground()) != -1)
            {
                foreach (Tile neighbor in currentNeighbors)
                {
                    if (!neighbor.GetMark())
                    {
                        if (neighbor.GetIsLowground())
                        {
                            neighbor.SetDistance(currentTile.GetDistance() + 1);
                            neighbor.SetMark(true);
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }
        }
    }

    private void GenerateFlowField()
    {
        for (int indX = 0; indX < 10; indX++)
        {
            for (int indY = 0; indY < 10; indY++)
            {
                float distanceLeft = 1f;
                float distanceRight = 1f;
                float distanceUp = 1f;
                float distanceDown = 1f;
                if (tiles[indX, indY].GetIsLowground())
                {
                    if (indX > 0)
                    {
                        //left
                        if (tiles[indX - 1, indY].GetIsLowground())
                        {
                            distanceLeft = tiles[indX - 1, indY].GetDistance() - tiles[indX, indY].GetDistance();
                        }
                    }
                    if (indX < 10 - 1)
                    {
                        //right
                        if (tiles[indX + 1, indY].GetIsLowground())
                        {
                            distanceRight = tiles[indX + 1, indY].GetDistance() - tiles[indX, indY].GetDistance();
                        }
                    }
                    if (indY > 0)
                    {
                        //down
                        if (tiles[indX, indY - 1].GetIsLowground())
                        {
                            distanceDown = tiles[indX, indY - 1].GetDistance() - tiles[indX, indY].GetDistance();
                        }
                    }
                    if (indY < 10 - 1)
                    {
                        //up
                        if (tiles[indX, indY + 1].GetIsLowground())
                        {
                            distanceUp = tiles[indX, indY + 1].GetDistance() - tiles[indX, indY].GetDistance();
                        }
                    }
                    
                    tiles[indX, indY].SetDirection(distanceLeft, distanceRight, distanceUp, distanceDown);
                    
                }
            }
        }
    }

    public void UpdateMap()
    {
        UpdateCurrentCommanderTile();
        GoalbasedPathfinding();
        EnemyManager.Instance.ResetMovement();
    }

    public void ChangeCommanderTile(Vector2Int newIndex)
    {
        tiles[commanderIndex.x, commanderIndex.y].GetComponent<CommanderTile>().SetIsCurrentCommanderPosition(false);
        commanderIndex = newIndex;
        UpdateMap();
    }

    public void ToogleDoorGroup(int groupId)
    {
        List<DoorTile> tempDoors = new List<DoorTile>();

        foreach (DoorTile doorTile in doorTiles)
        {
            if(doorTile.GetGroupId() == groupId)
            {
                if(doorTile.detectedEnemies.Count > 0)
                {
                    return;
                }
                tempDoors.Add(doorTile);
            }
        }

        foreach (DoorTile doorTile in tempDoors)
        {
            doorTile.ToogleDoor();
        }

        UpdateMap();
    }
}

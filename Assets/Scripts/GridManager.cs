using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor.Tilemaps;

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
    [SerializeField] private PrefabBrush lowgroundTileBrush;
    [SerializeField] private PrefabBrush highgroundTileBrush;
    [SerializeField] private PrefabBrush commanderTileBrush;
    [SerializeField] private PrefabBrush enemySpawnereTileBrush;
    [SerializeField] private PrefabBrush effectDamageTileBrush;
    [SerializeField] private PrefabBrush doorTileBrush;

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
                switch(GameManager.Instance.mapData[indX, indY])
                {
                    case '#':
                        highgroundTileBrush.Paint(grid, tilemap, grid.WorldToCell(new Vector3(startX, startY, 0)));
                        break;
                    case 'X':
                        enemySpawnereTileBrush.Paint(grid, tilemap, grid.WorldToCell(new Vector3(startX, startY, 0)));
                        GameManager.Instance.enemySpawnTileIndexs.Add(new Vector2Int(indY, indX));
                        break;
                    case 'U':
                        commanderTileBrush.Paint(grid, tilemap, grid.WorldToCell(new Vector3(startX, startY, 0)));
                        break;
                    case '!':
                        effectDamageTileBrush.Paint(grid, tilemap, grid.WorldToCell(new Vector3(startX, startY, 0)));
                        break;
                    case '?':
                        doorTileBrush.Paint(grid, tilemap, grid.WorldToCell(new Vector3(startX, startY, 0)));
                        break;
                    case 'V':
                        commanderTileBrush.Paint(grid, tilemap, grid.WorldToCell(new Vector3(startX, startY, 0)));
                        commanderIndex = new Vector2Int(indY, indX);
                        break;
                    default:
                        lowgroundTileBrush.Paint(grid, tilemap, grid.WorldToCell(new Vector3(startX, startY, 0)));
                        break;
                }
                startX += 1f;
            }
            startX = -4.5f;
            startY -= 1f;
        }
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
                continue;
            }
        }
    }

    private void AddDoorTileToList()
    {
        foreach(Door doorData in GameManager.Instance.doorsData)
        {
            try
            {
                DoorTile tempDoorTile = tiles[doorData.DoorIndex.x, doorData.DoorIndex.y].gameObject.GetComponent<DoorTile>();
                tempDoorTile.SetGroupId(doorData.GroupId);
                tempDoorTile.SetIsLowGround(doorData.InitiallyOpen);
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
        foreach(DoorTile doorTile in doorTiles)
        {
            if(doorTile.GetGroupId() == groupId)
            {
                doorTile.ToogleDoor();
            }
        }

        UpdateMap();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor.Tilemaps;

public class GridManager : MonoBehaviour
{
    private Grid grid;
    [SerializeField] GameObject tilemap;
    [SerializeField] PrefabBrush lowgroundTileBrush;
    [SerializeField] PrefabBrush highgroundTileBrush;
    [SerializeField] PrefabBrush commanderTileBrush;

    private Tile[,] tiles = new Tile[10, 10];

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Start()
    {
        LoadMap();
        AddTilesToArray();
    }

    private void LoadMap()
    {
        float startX = -4.5f;
        float startY = 4.5f;

        for (int indX = 0; indX < 10; indX++)
        {
            for (int indY = 0; indY < 10; indY++)
            {
                if (GameManager.Instance.mapData[indX, indY] == '#')
                {
                    highgroundTileBrush.Paint(grid, tilemap, grid.WorldToCell(new Vector3(startX, startY, 0)));
                }
                else if(GameManager.Instance.mapData[indX, indY] == 'V')
                {
                    commanderTileBrush.Paint(grid, tilemap, grid.WorldToCell(new Vector3(startX, startY, 0)));
                    GameManager.Instance.SetCommanderPositionAndIndex(new Vector3(startX, startY, 0), new Vector2Int(indY, indX));
                }
                else
                {
                    lowgroundTileBrush.Paint(grid, tilemap, grid.WorldToCell(new Vector3(startX, startY, 0)));
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
            counterX++;
            if (counterX >= 10)
            {
                counterX = 0;
                counterY++;
            }
        }

        InitiateNeighborsForTiles();
        GoalbasedPathfinding();
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
        Vector2Int commanderIndex = GameManager.Instance.GetCommanderIndex();
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
                        //up
                        if (tiles[indX, indY - 1].GetIsLowground())
                        {
                            distanceUp = tiles[indX, indY - 1].GetDistance() - tiles[indX, indY].GetDistance();
                        }
                    }
                    if (indY < 10 - 1)
                    {
                        //down
                        if (tiles[indX, indY + 1].GetIsLowground())
                        {
                            distanceDown = tiles[indX, indY + 1].GetDistance() - tiles[indX, indY].GetDistance();
                        }
                    }
                    
                    tiles[indX, indY].SetDirection(new Vector3(distanceLeft - distanceRight, distanceDown - distanceUp, 0f).normalized);
                    
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor.Tilemaps;

public class GridController : MonoBehaviour
{
    private Grid grid;
    [SerializeField] GameObject tilemap;
    [SerializeField] PrefabBrush lowgroundTileBrush;
    [SerializeField] PrefabBrush highgroundTileBrush;
    [SerializeField] PrefabBrush commanderTileBrush;

    private TileController[,] tileControllers = new TileController[10, 10];

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
            TileController tempTileController = child.GetComponent<TileController>();
            tileControllers[counterX, counterY] = tempTileController;
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
                    tileControllers[indX, indY].AddNeighbor(tileControllers[indX - 1, indY]);
                }
                if (indX < 10 - 1)
                {
                    //right
                    tileControllers[indX, indY].AddNeighbor(tileControllers[indX + 1, indY]);
                }
                if (indY > 0)
                {
                    //up
                    tileControllers[indX, indY].AddNeighbor(tileControllers[indX, indY - 1]);
                }
                if (indY < 10 - 1)
                {
                    //down
                    tileControllers[indX, indY].AddNeighbor(tileControllers[indX, indY + 1]);
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
                tileControllers[indX, indY].SetMark(false);
            }
        }
        Vector2Int commanderIndex = GameManager.Instance.GetCommanderIndex();
        tileControllers[commanderIndex.x, commanderIndex.y].SetMark(true);
        tileControllers[commanderIndex.x, commanderIndex.y].SetDistance(0);
        Queue<TileController> queue = new Queue<TileController>();
        queue.Enqueue(tileControllers[commanderIndex.x, commanderIndex.y]);
        while (queue.Count != 0)
        {
            TileController currentTileController = queue.Dequeue();
            List<TileController> currentNeighbors = currentTileController.GetNeighbors();
            if (currentNeighbors.FindIndex(t => t.GetIsLowground()) != -1)
            {
                foreach (TileController neighbor in currentNeighbors)
                {
                    if (!neighbor.GetMark())
                    {
                        if (neighbor.GetIsLowground())
                        {
                            neighbor.SetDistance(currentTileController.GetDistance() + 1);
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
                if (tileControllers[indX, indY].GetIsLowground())
                {
                    if (indX > 0)
                    {
                        //left
                        if (tileControllers[indX - 1, indY].GetIsLowground())
                        {
                            distanceLeft = tileControllers[indX - 1, indY].GetDistance() - tileControllers[indX, indY].GetDistance();
                        }
                    }
                    if (indX < 10 - 1)
                    {
                        //right
                        if (tileControllers[indX + 1, indY].GetIsLowground())
                        {
                            distanceRight = tileControllers[indX + 1, indY].GetDistance() - tileControllers[indX, indY].GetDistance();
                        }
                    }
                    if (indY > 0)
                    {
                        //up
                        if (tileControllers[indX, indY - 1].GetIsLowground())
                        {
                            distanceUp = tileControllers[indX, indY - 1].GetDistance() - tileControllers[indX, indY].GetDistance();
                        }
                    }
                    if (indY < 10 - 1)
                    {
                        //down
                        if (tileControllers[indX, indY + 1].GetIsLowground())
                        {
                            distanceDown = tileControllers[indX, indY + 1].GetDistance() - tileControllers[indX, indY].GetDistance();
                        }
                    }

                    tileControllers[indX, indY].SetDirection(new Vector3(distanceLeft - distanceRight, distanceDown - distanceUp, 0f).normalized);
                }
            }
        }
    }
}

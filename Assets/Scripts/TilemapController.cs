using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapController : MonoBehaviour
{
    private TileController[,] tileControllers = new TileController[10, 10];

    public void AddTilesToArray()
    {
        int counterX = 0;
        int counterY = 0;
        foreach(Transform child in transform)
        {
            TileController tempTileController = child.GetComponent<TileController>();
            tileControllers[counterX, counterY] = tempTileController;
            counterX++;
            if(counterX >= 10)
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
        for(int indX = 0; indX < 10; indX++)
        {
            for(int indY = 0; indY < 10; indY++)
            {
                if (indX > 0)
                {
                    tileControllers[indX, indY].AddNeighbor(tileControllers[indX - 1, indY]);
                }
                if (indX < 10 - 1)
                {
                    tileControllers[indX, indY].AddNeighbor(tileControllers[indX + 1, indY]);
                }
                if (indY > 0)
                {
                    tileControllers[indX, indY].AddNeighbor(tileControllers[indX, indY - 1]);
                }
                if (indY < 10 - 1)
                {
                    tileControllers[indX, indY].AddNeighbor(tileControllers[indX, indY + 1]);
                }
            }
        }
    }

    private void GoalbasedPathfinding()
    {

        GenerateHeatmap();
    }

    public void GenerateHeatmap()
    {
        for (int indX = 0; indX < 10; indX++)
        {
            for (int indY = 0; indY < 10; indY++)
            {
                tileControllers[indX, indY].SetMark(false);
            }
        }
        Vector2Int commanderIndex = GameManager.Instance.GetCommanderIndex();
        print(commanderIndex);
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

        print("Done");
    }

    public void GenerateFlowField()
    {

    }
}

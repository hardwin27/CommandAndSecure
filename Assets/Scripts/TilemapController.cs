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
        GridbasedPathfinding();
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

    private void GridbasedPathfinding()
    {
        for (int indX = 0; indX < 10; indX++)
        {
            for (int indY = 0; indY < 10; indY++)
            {
                if(tileControllers[indX, indY].GetComponent<TileController>().GetIsCommanderTile())
                {
                    print(indX.ToString() + ", " + indY.ToString());
                }
            }
        }
    }
}

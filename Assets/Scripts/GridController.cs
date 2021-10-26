using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor.Tilemaps;

public class GridController : MonoBehaviour
{
    //Temporary Map Data
    private char[,] mapData = new char[10, 10] {
        {'#', '#', '#', '#', '#', 'O', '#', '#', '#', 'O' },
        {'#', '#', '#', '#', '#', 'O', '#', '#', '#', 'O' },
        {'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
        {'#', '#', 'O', 'O', '#', '#', '#', 'O', 'O', 'O' },
        {'#', '#', 'O', 'O', '#', '#', '#', 'O', 'O', 'O' },
        {'#', '#', 'O', 'O', '#', '#', '#', '#', '#', '#' },
        {'#', '#', 'O', 'O', '#', '#', '#', '#', '#', '#' },
        {'O', 'O', 'O', 'O', '#', '#', '#', 'O', 'O', 'O' },
        {'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
        {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
    };

    private Grid grid;
    [SerializeField] GameObject tilemap;
    [SerializeField] PrefabBrush lowgroundBrush;
    [SerializeField] PrefabBrush highgroundBrush;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Start()
    {
        LoadMap();
    }

    private void LoadMap()
    {
        float startX = -4.5f;
        float startY = 4.5f;

        for (int indX = 0; indX < 10; indX++)
        {
            for (int indY = 0; indY < 10; indY++)
            {
                if (mapData[indX, indY] == '#')
                {
                    highgroundBrush.Paint(grid, tilemap, grid.WorldToCell(new Vector3(startX, startY, 0)));
                }
                else
                {
                    lowgroundBrush.Paint(grid, tilemap, grid.WorldToCell(new Vector3(startX, startY, 0)));
                }
                startX += 1f;
            }
            startX = -4.5f;
            startY -= 1f;
        }
    }
}

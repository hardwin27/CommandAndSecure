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

        tilemap.GetComponent<TilemapController>().AddTilesToArray();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    //Temporary Map Data
    public char[,] mapData = new char[10, 10] {
        {'#', '#', '#', '#', '#', 'O', '#', '#', '#', 'O' },
        {'#', '#', '#', '#', '#', 'O', '#', '#', '#', 'O' },
        {'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
        {'#', '#', 'O', 'O', '#', '#', '#', 'O', 'O', 'O' },
        {'#', '#', 'O', 'O', '#', '#', '#', 'O', 'O', 'O' },
        {'#', '#', 'O', 'O', '#', '#', '#', '#', '#', '#' },
        {'#', '#', 'O', 'O', '#', '#', '#', '#', '#', '#' },
        {'O', 'O', 'O', 'O', '#', '#', '#', 'O', 'O', 'V' },
        {'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
        {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
    };
    
    [SerializeField] private CommanderController commander;
    private Vector3 commanderPosition = new Vector3(0f, 0f, 0f);
    //Index reference the position of commander in the Tilemap
    private Vector2Int commanderIndex = Vector2Int.zero;

    public void SetCommanderPositionAndIndex(Vector3 position, Vector2Int index)
    {
        commanderPosition = position;
        commanderIndex = index;
        commander.transform.position = commanderPosition;
    }

    public Vector3 GetCommanderPosition()
    {
        return commanderPosition;
    }

    public Vector2Int GetCommanderIndex()
    {
        return commanderIndex;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public char[,] mapData { private set; get; } = new char[10, 10] {
        //lvl 1
        /*{'#', '#', 'O', 'O', 'O', 'O', 'O', 'O', '#', '#' },
        {'#', '#', 'O', '#', '#', '#', 'O', 'O', '#', '#' },
        {'O', 'O', 'O', '#', '#', '#', 'O', 'O', '#', '#' },
        {'X', 'O', 'O', '#', '#', '#', 'O', '#', '#', '#' },
        {'#', '#', '#', '#', '#', '#', 'O', '#', '#', '#' },
        {'X', 'O', 'O', '#', '#', '#', 'O', '#', '#', '#' },
        {'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'V' },
        {'#', '#', '#', '#', 'O', 'O', 'O', '#', '#', '#' },
        {'#', '#', '#', '#', 'O', 'O', '#', '#', '#', '#' },
        {'X', 'O', 'O', 'O', 'O', 'O', '#', '#', '#', '#' },*/

        //lvl 2
        /*{'#', 'O', 'O', 'O', '#', '#', 'O', 'U', 'O', '#' },
        {'#', 'O', 'O', 'O', '#', '#', 'O', 'O', 'O', '#' },
        {'#', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'X', '#' },
        {'#', 'O', '#', '#', '#', '#', '#', '#', '#', '#' },
        {'#', 'X', '#', '#', '#', '#', '#', '#', '#', '#' },
        {'#', 'O', '#', 'O', 'O', 'O', 'O', 'O', 'O', '#' },
        {'#', 'O', 'O', 'O', '#', '#', '#', 'O', 'O', '#' },
        {'#', '#', 'U', 'O', 'O', 'O', '#', 'O', 'O', '#' },
        {'#', '#', '#', 'O', 'O', 'O', '#', 'O', 'O', '#' },
        {'#', '#', '#', 'O', 'O', 'V', '#', 'O', 'X', '#' },*/

        //lvl 3
        /*{'#', '#', '#', '#', '#', 'O', '#', '#', '#', 'O' },
        {'#', '#', '#', '#', '#', 'O', '#', '#', '#', 'O' },
        {'X', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
        {'#', '#', 'O', 'O', '#', '#', '#', 'O', 'O', 'O' },
        {'#', '#', '!', '!', '#', '#', '#', 'O', 'O', 'U' },
        {'#', '#', 'O', 'O', '#', '#', '#', '#', '#', '#' },
        {'#', '#', 'O', 'O', '#', '#', '#', '#', '#', '#' },
        {'O', 'O', 'O', 'O', '#', '#', '#', 'O', 'O', 'V' },
        {'X', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
        {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },*/

        //lvl 4
        {'#', '#', '#', '#', '#', '#', '#', 'O', 'O', 'U' },
        {'#', '#', 'O', 'O', 'O', '#', '#', 'O', '#', 'O' },
        {'X', 'O', 'O', 'O', 'O', '#', '#', 'O', '#', 'O' },
        {'O', 'O', '#', '#', '!', '#', '#', 'O', '#', 'O' },
        {'O', '#', '#', '#', '?', '#', '#', 'O', '#', 'O' },
        {'O', '#', 'O', '?', 'X', '?', 'O', 'O', 'O', 'O' },
        {'O', '#', 'O', '#', '?', '#', '#', '#', '#', 'O' },
        {'O', '#', 'O', '#', '!', '#', '#', 'O', 'O', 'O' },
        {'O', '#', 'O', '#', 'O', 'O', 'O', 'O', 'O', 'X' },
        {'V', 'O', 'O', '#', '#', '#', '#', '#', '#', '#' },

        /*{'#', '#', '#', '#', '#', 'O', '#', '#', '#', 'O' },
        {'#', '#', '#', '#', '#', 'O', '#', '#', '#', 'O' },
        {'X', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
        {'#', '#', 'O', 'O', '#', '#', '#', 'O', 'O', 'O' },
        {'#', '#', '!', '!', '#', '#', '#', 'O', 'O', 'U' },
        {'#', '#', 'O', 'O', '#', '#', '#', '#', '#', '#' },
        {'#', '#', 'O', 'O', '#', '#', '#', '#', '#', '#' },
        {'O', 'O', 'O', 'O', '#', '#', '#', 'O', 'O', 'V' },
        {'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
        {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },*/

        /*{'#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
        {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
        {'O', 'O', '?', 'X', 'O', 'O', 'O', 'O', 'O', 'O' },
        {'!', '!', '#', '#', 'O', 'O', 'O', 'X', 'O', 'O' },
        {'O', 'O', '#', '#', 'O', 'O', 'O', 'O', 'O', 'O' },
        {'O', 'O', '?', '!', '?', '#', '#', '#', '#', '?' },
        {'O', '#', '#', 'V', 'O', '#', '#', '#', '#', 'O' },
        {'O', '#', '#', 'O', 'O', 'O', 'O', 'O', '!', 'O' },
        {'V', '#', '#', 'O', 'O', 'O', 'O', 'O', '!', 'V' },
        {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },*/
    };

    //lvl 1
    /*public Vector2 commanderInitTile { private set; get; } = new Vector2(9, 6);
    public float commanderInitHealth { private set; get; } = 25f;*/

    //lvl 2
    /*public Vector2 commanderInitTile { private set; get; } = new Vector2(5, 9);
    public float commanderInitHealth { private set; get; } = 25f;*/

    //lvl 3
    /*public Vector2 commanderInitTile { private set; get; } = new Vector2(9, 7);
    public float commanderInitHealth { private set; get; } = 10f;*/

    //lvl 4
    public Vector2 commanderInitTile { private set; get; } = new Vector2(0, 9);
    public float commanderInitHealth { private set; get; } = 5f;

    public List<Door> doorsData { private set; get; } = new List<Door>()
    {
        //lvl 1

        //lvl 2

        //lvl 3

        //lvl 4
        new Door(new Vector2Int(4, 4), 0, false),
        new Door(new Vector2Int(3, 5), 0, true),
        new Door(new Vector2Int(4, 6), 1, false),
        new Door(new Vector2Int(5, 5), 1, true),

        /*new Door(new Vector2Int(2, 2), 0, false),
        new Door(new Vector2Int(2, 5), 0, true),
        new Door(new Vector2Int(4, 5), 1, false),
        new Door(new Vector2Int(9, 5), 1, true),*/
    };

    public Dictionary<int, EnemyToSpawn> enemiesData { private set; get; } = new Dictionary<int, EnemyToSpawn> ()
    {
        //lvl 1
        /*{0, new EnemyToSpawn(0, 5f, 2) },
        {1, new EnemyToSpawn(0, 5f, 2) },
        {2, new EnemyToSpawn(0, 3f, 2) },
        {3, new EnemyToSpawn(0, 3f, 2) },
        {4, new EnemyToSpawn(2, 0.5f, 1) },
        {5, new EnemyToSpawn(0, 3f, 2) },
        {6, new EnemyToSpawn(0, 3f, 2) },
        {7, new EnemyToSpawn(1, 1f, 0) },
        {8, new EnemyToSpawn(0, 1f, 2) },
        {9, new EnemyToSpawn(1, 3f, 0) },
        {10, new EnemyToSpawn(1, 0.5f, 0) },
        {11, new EnemyToSpawn(2, 0.5f, 1) },
        {12, new EnemyToSpawn(0, 3f, 2) },
        {13, new EnemyToSpawn(1, 0.5f, 0) },
        {14, new EnemyToSpawn(2, 1f, 1) },*/

        //lvl 2
        /*{0, new EnemyToSpawn(0, 5f, 1) },
        {1, new EnemyToSpawn(0, 2f, 1) },
        {2, new EnemyToSpawn(0, 2f, 1) },
        {3, new EnemyToSpawn(0, 3f, 1) },
        {4, new EnemyToSpawn(1, 0.5f, 0) },
        {5, new EnemyToSpawn(1, 1f, 0) },
        {6, new EnemyToSpawn(2, 0.5f, 2) },
        {7, new EnemyToSpawn(1, 1f, 1) },
        {8, new EnemyToSpawn(0, 5f, 2) },
        {9, new EnemyToSpawn(0, 2f, 1) },
        {10, new EnemyToSpawn(1, 2f, 2) },
        {11, new EnemyToSpawn(2, 0.5f, 2) },
        {12, new EnemyToSpawn(0, 5f, 1) },
        {13, new EnemyToSpawn(0, 2f, 1) },
        {14, new EnemyToSpawn(0, 5f, 0) },
        {15, new EnemyToSpawn(1, 2f, 0) },
        {16, new EnemyToSpawn(1, 2f, 0) },
        {17, new EnemyToSpawn(1, 2f, 0) },
        {18, new EnemyToSpawn(2, 0.5f, 0) },
        {19, new EnemyToSpawn(0, 5f, 2) },
        {20, new EnemyToSpawn(2, 1f, 0) },
        {21, new EnemyToSpawn(0, 2f, 2) },
        {22, new EnemyToSpawn(0, 2f, 0) },
        {23, new EnemyToSpawn(1, 0.5f, 2) },
        {24, new EnemyToSpawn(1, 1f, 1) },
        {25, new EnemyToSpawn(2, 5f, 2) },
        {26, new EnemyToSpawn(2, 0.5f, 1) },
        {27, new EnemyToSpawn(1, 5f, 0) },
        {28, new EnemyToSpawn(1, 1f, 2) },
        {29, new EnemyToSpawn(0, 2f, 1) },*/

        //lvl 3
        /*{0, new EnemyToSpawn(0, 5f, 0) },
        {1, new EnemyToSpawn(0, 2f, 0) },
        {2, new EnemyToSpawn(0, 1f, 0) },
        {3, new EnemyToSpawn(1, 0.5f, 0) },
        {4, new EnemyToSpawn(1, 0.5f, 0) },
        {5, new EnemyToSpawn(1, 0.5f, 0) },
        {6, new EnemyToSpawn(0, 0.5f, 0) },
        {7, new EnemyToSpawn(0, 1f, 1) },
        {8, new EnemyToSpawn(2, 1f, 0) },
        {9, new EnemyToSpawn(0, 1f, 1) },
        {10, new EnemyToSpawn(1, 3f, 1) },
        {11, new EnemyToSpawn(1, 0.5f, 1) },
        {12, new EnemyToSpawn(1, 0.5f, 1) },
        {13, new EnemyToSpawn(1, 0.5f, 1) },
        {14, new EnemyToSpawn(2, 0.5f, 0) },
        {15, new EnemyToSpawn(0, 2f, 0) },
        {16, new EnemyToSpawn(0, 2f, 1) },
        {17, new EnemyToSpawn(2, 3f, 1) },
        {18, new EnemyToSpawn(1, 2f, 0) },
        {19, new EnemyToSpawn(1, 0.5f, 0) },
        {20, new EnemyToSpawn(0, 3f, 0) },
        {21, new EnemyToSpawn(0, 1f, 1) },
        {22, new EnemyToSpawn(0, 3f, 0) },
        {23, new EnemyToSpawn(0, 1f, 1) },
        {24, new EnemyToSpawn(2, 0.5f, 0) },
        {25, new EnemyToSpawn(1, 1f, 0) },
        {26, new EnemyToSpawn(1, 0.5f, 1) },
        {27, new EnemyToSpawn(1, 0.5f, 1) },
        {28, new EnemyToSpawn(2, 3f, 1) },
        {29, new EnemyToSpawn(0, 1f, 1) },
        {30, new EnemyToSpawn(0, 1f, 0) },
        {31, new EnemyToSpawn(0, 3f, 1) },
        {32, new EnemyToSpawn(0, 1f, 0) },
        {33, new EnemyToSpawn(2, 3f, 0) },
        {34, new EnemyToSpawn(1, 0.5f, 0) },
        {35, new EnemyToSpawn(1, 0.5f, 0) },
        {36, new EnemyToSpawn(1, 0.5f, 0) },
        {37, new EnemyToSpawn(1, 1f, 1) },
        {38, new EnemyToSpawn(1, 0.5f, 1) },
        {39, new EnemyToSpawn(1, 0.5f, 1) },*/

        //lvl 4
        {0, new EnemyToSpawn(0, 0.5f, 2) },
        {1, new EnemyToSpawn(0, 1f, 2) },
        {2, new EnemyToSpawn(0, 1f, 2) },
        {3, new EnemyToSpawn(2, 3f, 0) },
        {4, new EnemyToSpawn(0, 1f, 0) },
        {5, new EnemyToSpawn(1, 3f, 1) },
        {6, new EnemyToSpawn(0, 1f, 0) },
        {7, new EnemyToSpawn(0, 1f, 0) },
        {8, new EnemyToSpawn(0, 1f, 0) },
        {9, new EnemyToSpawn(2, 5f, 2) },
        {10, new EnemyToSpawn(0, 2f, 1) },
        {11, new EnemyToSpawn(0, 2f, 1) },
        {12, new EnemyToSpawn(0, 2f, 1) },
        {13, new EnemyToSpawn(1, 2f, 0) },
        {14, new EnemyToSpawn(1, 0.5f, 0) },
        {15, new EnemyToSpawn(1, 0.5f, 0) },
        {16, new EnemyToSpawn(1, 0.5f, 0) },
        {17, new EnemyToSpawn(1, 0.5f, 0) },
        {18, new EnemyToSpawn(2, 2f, 2) },
        {19, new EnemyToSpawn(2, 2f, 2) },
        {20, new EnemyToSpawn(0, 1f, 2) },
        {21, new EnemyToSpawn(0, 1f, 2) },
        {22, new EnemyToSpawn(0, 1f, 2) },
        {23, new EnemyToSpawn(2, 5f, 1) },
        {24, new EnemyToSpawn(0, 2f, 2) },
        {25, new EnemyToSpawn(0, 2f, 2) },
        {26, new EnemyToSpawn(1, 2f, 2) },
        {27, new EnemyToSpawn(1, 0.5f, 2) },
        {28, new EnemyToSpawn(1, 0.5f, 2) },
        {29, new EnemyToSpawn(1, 0.5f, 2) },
        {30, new EnemyToSpawn(1, 0.5f, 2) },
        {31, new EnemyToSpawn(2, 2f, 0) },
        {32, new EnemyToSpawn(0, 5f, 1) },
        {33, new EnemyToSpawn(0, 2f, 1) },
        {34, new EnemyToSpawn(0, 2f, 1) },
        {35, new EnemyToSpawn(1, 2f, 2) },
        {36, new EnemyToSpawn(1, 0.5f, 2) },
        {37, new EnemyToSpawn(0, 2f, 1) },
        {38, new EnemyToSpawn(1, 2f, 0) },
        {39, new EnemyToSpawn(1, 0.5f, 0) },


        /*{0, new EnemyToSpawn(0, 1f, 0) },
        {1, new EnemyToSpawn(0, 1f, 0) },
        {2, new EnemyToSpawn(1, 0.5f, 0) },
        {3, new EnemyToSpawn(1, 0.5f, 0) },
        {4, new EnemyToSpawn(2, 2f, 0) },
        {5, new EnemyToSpawn(0, 1f, 0) },
        {6, new EnemyToSpawn(1, 1f, 0) },
        {7, new EnemyToSpawn(2, 0.5f, 0) },
        {8, new EnemyToSpawn(2, 0.5f, 0) },
        {9, new EnemyToSpawn(0, 2f, 0) },*/
    };

    public List<Vector2Int> enemySpawnTileIndexs { private set; get; } = new List<Vector2Int>()
    {
        //lvl 1
        /*new Vector2Int(0, 3),
        new Vector2Int(0, 5),
        new Vector2Int(0, 9),*/

        //lvl 2
        /*new Vector2Int(8, 2),
        new Vector2Int(1, 4),
        new Vector2Int(8, 9),*/

        //lvl 3
        /*new Vector2Int(0, 2),
        new Vector2Int(0, 8),*/

        //lvl 4
        new Vector2Int(0, 2),
        new Vector2Int(4, 5),
        new Vector2Int(9, 8),
    };

    //Agent related UI & Variable
    [SerializeField] private Transform agentUIParent;
    [SerializeField] private GameObject agentUIPrefab;

    [SerializeField] private Agent[] agents;
    [SerializeField] private Transform agentParent;

    //From database
    //lvl 1
    /*private int agentLimit = 6;*/

    //lvl 2
    /*private int agentLimit = 5;*/

    //lvl 3
    /*private int agentLimit = 2;*/

    //lvl 4
    private int agentLimit = 5;

    public int agentCounter { get; private set; }

    //Enemy related UI & variable
    [SerializeField] private Text enemyCounterText;
    private int totalEnemy;
    private int defeatedEnemyAmount = 0;

    //Photon related UI & variable
    [SerializeField] private Text photonCounterText;

    //From database
    //lvl 1
    /*private int initialPhotonAmount = 0;*/

    //lvl 2
    /*private int initialPhotonAmount = 15;*/

    //lvl 3
    /*private int initialPhotonAmount = 5;*/

    //lvl 4
    private int initialPhotonAmount = 0;

    public int photonAmount { get; private set; }
    [SerializeField] private float photonInterval = 1f;
    private float photonTimer;

    [SerializeField] private Camera mainCamera;

    private bool isPaused = false;

    private void Start()
    {
        InstantiateAllAgentUI();
        InstantiateEnemyCounter();

        photonTimer = photonInterval;
        InititatePhoton();
    }

    private void Update()
    {
        if (GetIsPaused())
        {
            return;
        }

        GeneratePhoton();
    }

    private void InstantiateAllAgentUI()
    {
        foreach (Agent agent in agents)
        {
            GameObject newAgentObj = Instantiate(agentUIPrefab.gameObject, agentUIParent);
            AgentUI newAgentUI = newAgentObj.GetComponent<AgentUI>();

            newAgentUI.SetAgent(agent);
            newAgentUI.transform.name = agent.name;
        }
    }

    private void InstantiateEnemyCounter()
    {
        totalEnemy = enemiesData.Count;
        UpdateEnemyCounter();
    }

    private void UpdateEnemyCounter()
    {
        enemyCounterText.text = defeatedEnemyAmount.ToString() + "/" + totalEnemy.ToString();
    }

    public void AddEnemyCounter(int amount)
    {
        defeatedEnemyAmount += amount;
        UpdateEnemyCounter();
        if(defeatedEnemyAmount == totalEnemy)
        {
            GameManager.Instance.SetIsPaused(true);
            Time.timeScale = 0;
        }
    }

    public Transform GetAgentParent()
    {
        return agentParent;
    }

    private void InititatePhoton()
    {
        photonAmount = initialPhotonAmount;
        UpdatePhotonCounter();
    }

    private void GeneratePhoton()
    {
        if(photonTimer > 0)
        {
            photonTimer -= Time.deltaTime;
        }
        else
        {
            AddPhoton(1);

            photonTimer = photonInterval;
        }
    }

    public void AddPhoton(int amount)
    {
        photonAmount += amount;
        UpdatePhotonCounter();
    }

    private void UpdatePhotonCounter()
    {
        photonCounterText.text = photonAmount.ToString();
    }

    public bool GetIfCanSpawnAgent()
    {
        return agentCounter < agentLimit;
    }

    public void AddAgent(int amount)
    {
        agentCounter += amount;
    }

    public Camera GetMainCamera()
    {
        return mainCamera;
    }

    public void SetIsPaused(bool value)
    {
        isPaused = value;
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }
}

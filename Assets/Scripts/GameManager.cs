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
    public char[,] mapData = new char[10, 10] {
        {'#', '#', '#', '#', '#', 'O', '#', '#', '#', 'O' },
        {'#', '#', '#', '#', '#', 'O', '#', '#', '#', 'O' },
        {'X', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
        {'#', '#', 'O', 'O', '#', '#', '#', 'O', 'O', 'O' },
        {'#', '#', '!', '!', '#', '#', '#', 'O', 'O', 'U' },
        {'#', '#', 'O', 'O', '#', '#', '#', '#', '#', '#' },
        {'#', '#', 'O', 'O', '#', '#', '#', '#', '#', '#' },
        {'O', 'O', 'O', 'O', '#', '#', '#', 'O', 'O', 'V' },
        {'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
        {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },

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

    public List<Door> doorsData = new List<Door>()
    {
        /*new Door(new Vector2Int(2, 2), 0, false),
        new Door(new Vector2Int(2, 5), 0, true),
        new Door(new Vector2Int(4, 5), 1, false),
        new Door(new Vector2Int(9, 5), 1, true),*/
    };

    public Dictionary<int, EnemyToSpawn> enemiesData = new Dictionary<int, EnemyToSpawn> ()
    {
        {0, new EnemyToSpawn(0, 1f, 0) },
        {1, new EnemyToSpawn(0, 1f, 0) },
        {2, new EnemyToSpawn(1, 0.5f, 0) },
        {3, new EnemyToSpawn(1, 0.5f, 0) },
        {4, new EnemyToSpawn(2, 2f, 0) },
        {5, new EnemyToSpawn(0, 1f, 0) },
        {6, new EnemyToSpawn(1, 1f, 0) },
        {7, new EnemyToSpawn(2, 0.5f, 0) },
        {8, new EnemyToSpawn(2, 0.5f, 0) },
        {9, new EnemyToSpawn(0, 2f, 0) },
    };

    public List<Vector2Int> enemySpawnTileIndexs = new List<Vector2Int>();

    //Agent related UI & Variable
    [SerializeField] private Transform agentUIParent;
    [SerializeField] private GameObject agentUIPrefab;

    [SerializeField] private Agent[] agents;
    [SerializeField] private Transform agentParent;
    [SerializeField] private int agentLimit = 4;
    public int agentCounter { get; private set; }

    //Enemy related UI & variable
    [SerializeField] private Text enemyCounterText;
    private int totalEnemy;
    private int defeatedEnemyAmount = 0;

    //Photon related UI & variable
    [SerializeField] private Text photonCounterText;
    [SerializeField] private int initialPhotonAmount = 10;
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

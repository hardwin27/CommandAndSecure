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

    //UI Related Variable
    [SerializeField] private Transform agentUIParent;
    [SerializeField] private GameObject agentUIPrefab;

    [SerializeField] private Agent[] agents;
    [SerializeField] private Transform agentParent;
    [SerializeField] private int agentLimit = 4;
    public int agentCounter { get; private set; }

    //Photon related
    [SerializeField] private Text photonCounterText;
    [SerializeField] private int initialPhotonAmount = 10;
    public int photonAmount { get; private set; }
    [SerializeField] private float photonInterval = 1f;
    private float photonTimer;

    private void Start()
    {
        InstantiateAllAgentUI();

        photonTimer = photonInterval;
        InititatePhoton();
    }

    private void Update()
    {
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
}

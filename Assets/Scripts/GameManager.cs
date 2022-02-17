using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public Commander commander;

    public TextAsset databaseJson;
    private Database database;

    public static int selectedLevel;

    //From Database
    public string[,] mapData { private set; get; } = new string[10, 10];
    public List<Door> doorsData { private set; get; }
    public List<Vector2Int> enemySpawnTileIndexs { private set; get; }
    public Vector2Int commanderInitTile { private set; get; } = new Vector2Int(0, 9);
    public float commanderInitHealth { private set; get; } = 5f;
    private int agentLimit;
    private int initialPhotonAmount = 0;
    public List<EnemyToSpawn> enemiesData { private set; get; }

    //Agent related UI & Variable
    [SerializeField] private Transform agentUIParent;
    [SerializeField] private GameObject agentUIPrefab;
    private AgentUI currentAgentUI;
    [SerializeField] private List<Agent> agents;
    [SerializeField] private Transform agentParent;
    public int agentCounter { get; private set; }

    //Enemy related UI & variable
    [SerializeField] private Text enemyCounterText;
    private int totalEnemy;
    private int defeatedEnemyAmount = 0;

    //Photon related UI & variable
    [SerializeField] private Text photonCounterText;
    public int photonAmount { get; private set; }
    [SerializeField] private float photonInterval = 1f;
    private float photonTimer;


    [SerializeField] private Camera mainCamera;

    //Overlay related
    [SerializeField] private GameObject overlayPanel;
    [SerializeField] private GameObject pauseGroup;
    [SerializeField] private GameObject winGroup;
    [SerializeField] private GameObject loseText;
    [SerializeField] private GameObject ratingView;
    [SerializeField] private Color ratingStarColor;
    [SerializeField] private GameObject buttons;
    private bool isPaused = false;
    private bool isGameOver = false;
    private bool isWin = false;

    private void Awake()
    {
        if(LevelSelection.instance == null)
        {
            selectedLevel = 0;
        }
        else
        {
            selectedLevel = LevelSelection.instance.selectedLevel;
        }
    }

    private void Start()
    {
        isPaused = false;
        isGameOver = false;
        isWin = false;
        Time.timeScale = 1f;
        
        ReadDatabase();

        InstantiateAllAgentUI();
        InstantiateEnemyCounter();

        photonTimer = photonInterval;
        InititatePhoton();

        UpdateOverlayPanel();
    }

    private void Update()
    {
        if(isGameOver)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetIsPaused(!GetIsPaused());
        }
        
        if (GetIsPaused())
        {
            return;
        }

        GeneratePhoton();
    }

    private void ReadDatabase()
    {
        database = JsonUtility.FromJson<Database>(databaseJson.text);
        List<string> tempMapData = database.levels[selectedLevel].mapData;
        int tempIndex = 0;
        for (int indX = 0; indX < 10; indX++)
        {
            for (int indY = 0; indY < 10; indY++)
            {
                mapData[indX, indY] = tempMapData[tempIndex];
                tempIndex++;
            }
        }
        doorsData = database.levels[selectedLevel].doorsData;
        enemySpawnTileIndexs = database.levels[selectedLevel].enemySpawnTileIndexs;
        commanderInitTile = database.levels[selectedLevel].commanderInitTile;
        commanderInitHealth = database.levels[selectedLevel].commanderInitHealth;
        agentLimit = database.levels[selectedLevel].agentLimit;
        initialPhotonAmount = database.levels[selectedLevel].initialPhotonAmount;
        enemiesData = database.levels[selectedLevel].enemiesData;
    }

    private void InstantiateAllAgentUI()
    {
        if(AgentSelectionManager.Instance != null)
        {
            agents.Clear();
            agents = AgentSelectionManager.Instance.selectedAgentsList;
        }

        foreach (Agent agent in agents)
        {
            GameObject newAgentObj = Instantiate(
                agentUIPrefab.gameObject, agentUIParent
                );
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
            GameManager.Instance.SetIsWin(true);
            Time.timeScale = 0f;
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
        if (isPaused == true)
        {
            ClearAgentUI();
        }
        UpdateOverlayPanel();
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }

    private void UpdateOverlayPanel()
    {
        overlayPanel.SetActive(isPaused);
        buttons.SetActive(isPaused);
        if(isPaused)
        {
            if (isGameOver)
            {
                if (isWin)
                {
                    winGroup.SetActive(true);
                    int rating = GetRating();
                    Image[] ratingStars = ratingView.GetComponentsInChildren<Image>();
                    for(int starIndex = 0; starIndex < rating; starIndex++)
                    {
                        ratingStars[starIndex].color = ratingStarColor;
                    }
                }
                else
                {
                    loseText.SetActive(true);
                }
            }
            else
            {
                pauseGroup.SetActive(true);
            }
        }
        else
        {
            pauseGroup.SetActive(false);
            winGroup.SetActive(false);
            loseText.SetActive(false);
        }
    }

    public void SetIsWin(bool winStatus)
    {
        isGameOver = true;
        isWin = winStatus;

        SetIsPaused(true);
    }

    private int GetRating()
    {
        float percHealthRemaining = commander.GetRemainingHealthPercentage();
        return Mathf.Clamp(Mathf.RoundToInt(percHealthRemaining / 0.25f), 1, 3);
    }

    public void SetCurrentSelectedAgent(AgentUI agentUI)
    {
        currentAgentUI = agentUI;
    }

    private void ClearAgentUI()
    {
        if(currentAgentUI != null)
        {
            currentAgentUI.CleanCurrentSelectedAgent();
        }
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

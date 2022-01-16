using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AgentSelectionManager : MonoBehaviour
{
    private static AgentSelectionManager _instance = null;
    public static AgentSelectionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AgentSelectionManager>();
            }
            return _instance;
        }
    }

    [SerializeField] private Transform agentSelectionPanel;
    [SerializeField] private GameObject agentSelectionOptionPrefab;
    [SerializeField] private List<Agent> agents;

    [SerializeField] private Button startButton;
    public List<Agent> selectedAgentsList { private set; get; }

    private int agentAmountReq = 3;
    public int AgentAmountCounter { private set; get; } = 0;
    public bool IsAgentFull { get { return (AgentAmountCounter >= agentAmountReq); } }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        startButton.interactable = false;
        startButton.onClick.AddListener(StartGame);
        InstantiateAgentSelection();
    }

    private void InstantiateAgentSelection()
    {
        foreach (Agent agent in agents)
        {
            GameObject newAgentObj = Instantiate(agentSelectionOptionPrefab.gameObject, agentSelectionPanel);
            AgentSelectionOption newAgentUI = newAgentObj.GetComponent<AgentSelectionOption>();

            newAgentUI.SetAgent(agent);
            newAgentUI.transform.name = agent.name;
        }
    }

    public void AddSelectedAgent(int amount)
    {
        AgentAmountCounter += amount;
        if(IsAgentFull)
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
    }

    public void StartGame()
    {
        if (selectedAgentsList == null)
        {
            selectedAgentsList = new List<Agent>();
        }
        else
        {
            selectedAgentsList.Clear();
        }

        foreach (AgentSelectionOption agentSelection in agentSelectionPanel.GetComponentsInChildren<AgentSelectionOption>())
        {
            if (agentSelection.IsSelected)
            {
                selectedAgentsList.Add(agentSelection.GetAgent());
            }
        }

        SceneManager.LoadScene(3);
    }

    public void BackToLevelSelect()
    {
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private Agent[] agents;

    private int agentAmountReq = 3;
    public int AgentAmountCounter { private set; get; } = 0;
    public bool IsAgentFull { get { return (AgentAmountCounter >= agentAmountReq); } }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
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
    }
}

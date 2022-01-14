using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSelection : MonoBehaviour
{
    [SerializeField] private Transform agentSelectionPanel;
    [SerializeField] private GameObject agentSelectionOptionPrefab;
    [SerializeField] private Agent[] agents;

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
}

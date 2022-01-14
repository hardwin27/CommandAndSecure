using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentSelectionOption : MonoBehaviour
{
    [SerializeField] private Image agentIcon;
    [SerializeField] private Text codeNameText;
    [SerializeField] private Text costText;
    [SerializeField] private Text descText;
    private Agent agent;

    public void SetAgent(Agent value)
    {
        agent = value;
        agentIcon.sprite = value.GetAgentIcon();
        codeNameText.text = value.CodeName;
        costText.text = "Photon Cost: " +  value.PhotonCost.ToString();
        descText.text = value.Description;
    }
}

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

    public bool IsSelected { private set; get; } = false; 
    private Image panelBackground;

    private Button button;

    private void Awake()
    {
        panelBackground = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(ToggleAgent);
        UpdatePanelColor();
    }

    public void SetAgent(Agent value)
    {
        agent = value;
        agentIcon.sprite = value.GetAgentIcon();
        codeNameText.text = value.CodeName;
        costText.text = "Photon Cost: " +  value.PhotonCost.ToString();
        descText.text = value.Description;
    }

    private void ToggleAgent()
    {
        if(!IsSelected)
        {
            if (AgentSelectionManager.Instance.IsAgentFull)
            {
                return;
            }
        }

        IsSelected = !IsSelected;
        AgentSelectionManager.Instance.AddSelectedAgent((IsSelected) ? 1 : -1);
        UpdatePanelColor();
    }

    private void UpdatePanelColor()
    {
        panelBackground.color = (IsSelected) ? Color.green : Color.white;
    }
}

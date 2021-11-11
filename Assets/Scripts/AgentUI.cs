using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AgentUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image agentIcon;

    private Agent agent;
    private Agent currentSelectedAgent;

    public void SetAgent(Agent value)
    {
        agent = value;
        agentIcon.sprite = value.GetAgentIcon();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject newAgentObj = Instantiate(agent.gameObject, GameManager.Instance.GetAgentParent());
        currentSelectedAgent = newAgentObj.GetComponent<Agent>();
        currentSelectedAgent.ToggleOrderInLayer(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Camera mainCamera = Camera.main;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        currentSelectedAgent.transform.position = targetPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(currentSelectedAgent.placedPosition == null)
        {
            Destroy(currentSelectedAgent.gameObject);
        }
        else
        {
            currentSelectedAgent.LockPlacement();
            currentSelectedAgent.ToggleOrderInLayer(false);
            currentSelectedAgent = null;
        }
    }
}

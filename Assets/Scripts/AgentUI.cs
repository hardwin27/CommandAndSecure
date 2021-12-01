using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AgentUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image agentIcon;
    [SerializeField] private Text costText;

    private Agent agent;
    private Agent currentSelectedAgent;

    private bool isDragging = false;

    public void SetAgent(Agent value)
    {
        agent = value;
        agentIcon.sprite = value.GetAgentIcon();
        costText.text = value.GetPhotonCost().ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.GetIsPaused())
        {
            return;
        }

        if (GameManager.Instance.photonAmount >= agent.GetPhotonCost() && GameManager.Instance.GetIfCanSpawnAgent())
        {
            GameObject newAgentObj = Instantiate(agent.gameObject, GameManager.Instance.GetAgentParent());
            currentSelectedAgent = newAgentObj.GetComponent<Agent>();
            currentSelectedAgent.ToggleOrderInLayer(true);
            isDragging = true;
        }
        else
        {
            isDragging = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.GetIsPaused())
        {
            return;
        }

        if (isDragging)
        {
            Camera mainCamera = Camera.main;
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -mainCamera.transform.position.z;
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            currentSelectedAgent.transform.position = targetPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.GetIsPaused())
        {
            return;
        }

        if (isDragging)
        {
            /*if (currentSelectedAgent.placedPosition == null)
            {
                Destroy(currentSelectedAgent.gameObject);
                print("NULL");
            }
            else
            {
                print("PLACED");
                currentSelectedAgent.LockPlacement();
                currentSelectedAgent.ToggleOrderInLayer(false);
                GameManager.Instance.AddPhoton(-1 * currentSelectedAgent.GetPhotonCost());
                GameManager.Instance.AddAgent(1);
                currentSelectedAgent = null;
            }*/
            currentSelectedAgent.CheckPlacement();
        }
    }
}

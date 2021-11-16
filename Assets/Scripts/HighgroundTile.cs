using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighgroundTile : Tile
{
    [SerializeField] private Agent placedAgent;

    protected override void Awake()
    {
        base.Awake();
        isLowground = false;
    }

    protected override void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(placedAgent != null)
        {
            return;
        }

        Agent agent = collision.GetComponent<Agent>();
        if(agent != null)
        {
            agent.SetPlacedPosition(transform.position);
            placedAgent = agent;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(placedAgent == null)
        {
            return;
        }

        placedAgent.SetPlacedPosition(null);
        placedAgent = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighgroundTile : Tile
{
    [SerializeField] private Agent placedAgent = null;

    protected override void Awake()
    {
        base.Awake();
        isLowground = false;
    }

    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        if (GameManager.Instance.GetIsPaused())
        {
            return;
        }

        base.Update();
    }

    public void SetPlacedAgent(Agent agent)
    {
        placedAgent = agent;
    }

    public Agent GetPlacedAgent()
    {
        return placedAgent;
    }
}

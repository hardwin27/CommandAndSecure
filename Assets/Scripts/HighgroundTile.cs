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

    public void SetPlacedAgent(Agent agent)
    {
        placedAgent = agent;
    }

    public Agent GetPlacedAgent()
    {
        return placedAgent;
    }
}

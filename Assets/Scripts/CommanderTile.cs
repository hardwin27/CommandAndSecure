using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderTile : Tile
{
    private bool isCurrentCommanderPosition = false;
    protected override void Awake()
    {
        base.Awake();
        isLowground = true;
        isCommanderTile = true;
    }

    protected override void Start()
    {
        
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        if(!isCurrentCommanderPosition)
        {
            GridManager.Instance.ChangeCommanderTile(index);
        }
    }

    public void SetIsCurrentCommanderPosition(bool value)
    {
        isCurrentCommanderPosition = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderTile : Tile
{
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
    }
}

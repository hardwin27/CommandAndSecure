using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderTileController : TileController
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
}

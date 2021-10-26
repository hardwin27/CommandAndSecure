using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighgroundTileController : TileController
{
    protected override void Awake()
    {
        base.Awake();
        isLowground = false;
    }

    protected override void Start()
    {
        
    }
}

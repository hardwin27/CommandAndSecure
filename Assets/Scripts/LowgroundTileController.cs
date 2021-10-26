using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowgroundTileController : TileController
{
    protected override void Awake()
    {
        base.Awake();
        isLowground = true;
    }

    protected override void Start()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : Tile
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

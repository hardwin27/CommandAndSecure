using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowgroundTile : Tile
{
    protected override void Awake()
    {
        base.Awake();
        isLowground = true;
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
}

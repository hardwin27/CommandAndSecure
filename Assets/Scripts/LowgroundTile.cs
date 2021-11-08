using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowgroundTile : Tile
{
    /*[SerializeField] private Canvas canvas;*/

    protected override void Awake()
    {
        base.Awake();
        isLowground = true;

        /*canvas.worldCamera = Camera.main;*/
    }

    protected override void Start()
    {
        
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
    }
}

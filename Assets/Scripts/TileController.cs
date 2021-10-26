using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    protected List<TileController> neighbors = new List<TileController>();

    protected int distance;
    protected Vector2 direction;

    protected bool isLowground;
    protected bool isCommanderTile = false;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        
    }

    public void AddNeighbor(TileController tileController)
    {
        neighbors.Add(tileController);
    }

    public List<TileController> GetNeighbors()
    {
        return neighbors;
    }

    public bool GetIsCommanderTile()
    {
        return isCommanderTile;
    }
}

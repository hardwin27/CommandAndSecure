using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    protected List<TileController> neighbors = new List<TileController>();

    protected int distance;
    protected Vector2 direction;
    protected TileController cameFrom;

    protected bool isLowground;
    protected bool isCommanderTile = false;

    //Mark used for the gridbased pathfinding
    protected bool mark = false;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void OnMouseDown()
    {
        print(distance);
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

    public bool GetIsLowground()
    {
        return isLowground;
    }

    public void SetDistance(int value)
    {
        distance = value;
    }

    public int GetDistance()
    {
        return distance;
    }

    public void SetDirection(Vector2 value)
    {
        direction = value;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public void SetMark(bool value)
    {
        mark = value;
    }

    public bool GetMark()
    {
        return mark;
    }
}

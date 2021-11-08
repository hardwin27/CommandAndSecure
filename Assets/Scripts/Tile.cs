using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    protected List<Tile> neighbors = new List<Tile>();

    protected int distance = 0;
    protected Vector3 direction = Vector3.zero;
    protected Tile cameFrom;

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

    protected virtual void Update()
    {

    }

    protected virtual void OnMouseDown()
    {

    }

    public void AddNeighbor(Tile Tile)
    {
        neighbors.Add(Tile);
    }

    public List<Tile> GetNeighbors()
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

    public void SetDirection(Vector3 value)
    {
        if(Mathf.Abs(value.x) > Mathf.Abs(value.y))
        {
            value = new Vector3(value.x / Mathf.Abs(value.x), 0f, 0f);
        }
        else if (Mathf.Abs(value.x) < Mathf.Abs(value.y))
        {
            value = new Vector3(0f, value.y / Mathf.Abs(value.y), 0f);
        }
        else
        {
            value = new Vector3(value.x / Mathf.Abs(value.x), value.y / Mathf.Abs(value.y), 0f);
        }
        direction = value;
    }

    public Vector3 GetDirection()
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

    //for debugging
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    protected List<Tile> neighbors = new List<Tile>();

    [SerializeField] protected int distance = 0;
    [SerializeField] protected Vector3 direction = Vector3.zero;
    [SerializeField] protected Tile cameFrom;

    [SerializeField] protected float distanceLeft = 0f;
    [SerializeField] protected float distanceRight = 0f;
    [SerializeField] protected float distanceUp = 0f;
    [SerializeField] protected float distanceDown = 0f;

    [SerializeField] protected bool isLowground;
    protected bool isCommanderTile = false;

    protected Vector2Int index;

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
        if (GameManager.Instance.GetIsPaused())
        {
            return;
        }
    }

    protected virtual void OnMouseDown()
    {
        if (GameManager.Instance.GetIsPaused())
        {
            return;
        }
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

    public void SetDirection(float left, float right, float up, float down)
    {
        distanceLeft = left;
        distanceRight = right;
        distanceUp = up;
        distanceDown = down;

        float newX = (left - right);
        float newY = up - down;
        Vector3 value = new Vector3(newX, newY, 0f).normalized;
        value = new Vector3(Mathf.Round(value.x), Mathf.Round(value.y), 0f);

        
        //Fixing bad value
        if (newX == 0 && newY == 0)
        {
            if(!isCommanderTile)
            {
                int tempDistance = distance;
                Transform tempTranform = transform;
                foreach(Tile neighbor in neighbors)
                {
                    if(neighbor.GetIsLowground())
                    {
                        if (neighbor.GetDistance() < tempDistance)
                        {
                            tempDistance = neighbor.GetDistance();
                            tempTranform = neighbor.transform;
                        }
                    }
                }
                value = (tempTranform.position - transform.position).normalized;
            }
        }
        else if(Mathf.Abs(value.x) == Mathf.Abs(value.y))
        {
            if(value.x == value.y)
            {
                value = new Vector3(0f, value.y, 0f);
            }
            else
            {
                value = new Vector3(value.x, 0f, 0f);
            }
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

    public void SetIndex(Vector2Int value)
    {
        index = value;
    }

    public Vector2Int GetIndex()
    {
        return index;
    }

    //for debugging
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}

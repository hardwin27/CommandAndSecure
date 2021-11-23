using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door
{
    private Vector2Int doorIndex;
    private int groupId;
    private bool initiallyOpen;

    public Vector2Int DoorIndex { get { return doorIndex; } }
    public int GroupId { get { return groupId; } }
    public bool InitiallyOpen { get { return initiallyOpen; } }

    public Door(Vector2Int index, int id, bool isOpen)
    {
        doorIndex = index;
        groupId = id;
        initiallyOpen = isOpen;
    }
}

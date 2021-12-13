using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Door
{
    public Vector2Int doorIndex;
    public int groupId;
    public bool initiallyOpen;

    /*public Vector2Int DoorIndex { get { return doorIndex; } }
    public int GroupId { get { return groupId; } }
    public bool InitiallyOpen { get { return initiallyOpen; } }*/

    /*public Door(Vector2Int index, int id, bool isOpen)
    {
        doorIndex = index;
        groupId = id;
        initiallyOpen = isOpen;
    }*/
}

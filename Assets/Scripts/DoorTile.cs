using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : Tile
{
    private int groupId;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite openDoorSprite;
    [SerializeField] private Sprite closeDoorSprite;

    private BoxCollider2D boxCollider;

    protected override void Awake()
    {
        base.Awake();

        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected override void Start()
    {
        UpdateDoor();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        GridManager.Instance.ToogleDoorGroup(groupId);
    }

    public void SetIsLowGround(bool initiallyOpen)
    {
        isLowground = initiallyOpen;
        print(isLowground);
    }

    public void SetGroupId(int id)
    {
        groupId = id;
    }

    public int GetGroupId()
    {
        return groupId;
    }

    public void ToogleDoor()
    {
        isLowground = !isLowground;
        UpdateDoor();
    }

    private void UpdateDoor()
    {
        if(isLowground)
        {
            spriteRenderer.sprite = openDoorSprite;
            boxCollider.size = new Vector2(1f, 1f);
        }
        else
        {
            spriteRenderer.sprite = closeDoorSprite;
            boxCollider.size = new Vector2(0.8f, 0.8f);
        }
    }
}

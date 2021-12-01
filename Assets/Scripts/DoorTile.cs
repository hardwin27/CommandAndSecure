using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : Tile
{
    private int groupId;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite openDoorSprite;
    [SerializeField] private Sprite closeDoorSprite;
    private float colorIncrease;
    private float colorValue;

    [SerializeField] private float cooldownDuration = 20f;
    private float cooldownUpdateInterval = 1f;
    private float cooldownTimer;
    private float cooldownCounter;
    private bool canBeUsed;

    private BoxCollider2D boxCollider;

    public List<Enemy> detectedEnemies { get; private set; } = new List<Enemy>();

    protected override void Awake()
    {
        base.Awake();

        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected override void Start()
    {
        UpdateDoor();

        canBeUsed = false;
        colorIncrease = 1f / cooldownDuration * 0.5f;
        colorValue = 0;
        UpdateSpriteColor();
        cooldownTimer = cooldownUpdateInterval;
        cooldownCounter = cooldownDuration;
    }

    protected override void Update()
    {
        if (GameManager.Instance.GetIsPaused())
        {
            return;
        }

        base.Update();
        UpdateCooldownTimer();
    }

    protected override void OnMouseDown()
    {
        if (GameManager.Instance.GetIsPaused())
        {
            return;
        }

        base.OnMouseDown();
        if(canBeUsed)
        {
            GridManager.Instance.ToogleDoorGroup(groupId);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy tempEnemy = collision.GetComponent<Enemy>();
        if (tempEnemy != null)
        {
            detectedEnemies.Add(tempEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy tempEnemy = collision.GetComponent<Enemy>();
        if (tempEnemy != null)
        {
            try
            {
                detectedEnemies.Remove(tempEnemy);
            }
            catch
            {
                print("Tile Enemy Error");
            }
        }
    }

    public void SetIsLowGround(bool initiallyOpen)
    {
        isLowground = initiallyOpen;
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
        SetDistance(0);
        SetDirection(0f, 0f, 0f, 0f);
        UpdateDoor();
    }

    private void UpdateDoor()
    {
        if(isLowground)
        {
            spriteRenderer.sprite = openDoorSprite;
            boxCollider.size = new Vector2(1f, 1f);
            boxCollider.isTrigger = true;
        }
        else
        {
            spriteRenderer.sprite = closeDoorSprite;
            boxCollider.size = new Vector2(0.8f, 0.8f);
            boxCollider.isTrigger = false;
        }

        canBeUsed = false;
        cooldownCounter = cooldownDuration;
        colorValue = 0;
        UpdateSpriteColor();
    }

    private void UpdateSpriteColor()
    {
        float rgbValue = Mathf.Clamp((colorValue * colorIncrease) + 0.5f, 0f, 1f);
        spriteRenderer.color = new Color(rgbValue, rgbValue, rgbValue);
    }

    private void UpdateCooldownTimer()
    {
        if (cooldownCounter <= 0f)
        {
            canBeUsed = true;
        }
        else
        {
            if (cooldownTimer <= 0f)
            {
                colorValue += 1;
                UpdateSpriteColor();
                cooldownCounter -= 1f;
                cooldownTimer = cooldownUpdateInterval;
            }
            else
            {
                cooldownTimer -= Time.deltaTime;
            }
        }
    }
}

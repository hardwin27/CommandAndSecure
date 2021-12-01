using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderTile : Tile
{
    private bool isCurrentCommanderPosition = false;

    private SpriteRenderer spriteRenderer;
    private float colorIncrease;
    private float colorValue;

    [SerializeField] private float cooldownDuration = 10f;
    private float cooldownUpdateInterval = 1f;
    private float cooldownTimer;
    private float cooldownCounter;
    private bool canBeUsed;

    protected override void Awake()
    {
        base.Awake();
        isLowground = true;
        isCommanderTile = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
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

        if (!isCurrentCommanderPosition)
        {
            if(canBeUsed)
            {
                GridManager.Instance.ChangeCommanderTile(index);
                canBeUsed = false;
                cooldownCounter = cooldownDuration;
                colorValue = 0;
                UpdateSpriteColor();
            }
        }
    }

    public void SetIsCurrentCommanderPosition(bool value)
    {
        isCurrentCommanderPosition = value;
    }

    private void UpdateSpriteColor()
    {
        float rgbValue = Mathf.Clamp((colorValue * colorIncrease) + 0.5f, 0f, 1f);
        spriteRenderer.color = new Color(rgbValue, rgbValue, rgbValue);
    }

    private void UpdateCooldownTimer()
    {
        if(cooldownCounter <= 0f)
        {
            canBeUsed = true;
        }
        else
        {
            if(cooldownTimer <= 0f)
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

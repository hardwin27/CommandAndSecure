using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform visual;
    private Rigidbody2D body;

    [SerializeField] private float maxHealth = 5f;
    private float health;

    private Vector3 lookDirection;
    private float angle;

    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private Vector2 moveDirection;

    [SerializeField] private EnemyTileDetector tileDetector;
    [SerializeField] private bool isWalkToTile;
    [SerializeField] private Vector3 tilePosition;

    private float dotInterval = 1f;
    private float dotDuration;
    private float dotTimer;
    private float dotDamage;

    public Commander detectedCommander = null;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float shootInterval = 1;
    [SerializeField] private float projectileSpeed = 5;
    [SerializeField] private float projectileDamage = 2;
    private float shootTimer;

    [SerializeField] private Canvas enemyUI;
    [SerializeField] private Slider healthBar;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        enemyUI.worldCamera = GameManager.Instance.GetMainCamera();
    }

    private void Start()
    {
        isWalkToTile = false;
        tilePosition = transform.position;
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;

        dotTimer = dotInterval;
        shootTimer = shootInterval;
    }

    private void Update()
    {
        DamageOverTime();
    }

    private void FixedUpdate()
    {
        if(detectedCommander == null)
        {
            RotateCharacter();
            MoveCharacter();
        }
        else
        {
            LookToCommander();
            Shoot();
        }
        
    }

    private void RotateCharacter()
    {
        /*if(Vector3.Distance(transform.position, tilePosition) <= 0.1f)
        {
            isWalkToTile = false;
            lookDirection = Vector3.zero;
        }*/


        if (isWalkToTile)
        {
            if (Vector3.Distance(transform.position, tilePosition) <= 0.1f)
            {
                isWalkToTile = false;
                lookDirection = Vector3.zero;
            }
            else
            {
                lookDirection = tilePosition - transform.position;
            }
        }
        else
        {
            lookDirection = tileDetector.GetDetectedDirection();
        }

        if (lookDirection != Vector3.zero)
        {
            lookDirection.Normalize();
            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            visual.rotation = Quaternion.Euler(0f,0f, angle);
        }

        moveDirection = lookDirection;
    }

    private void MoveCharacter()
    {
        body.MovePosition((Vector2)transform.position + (moveDirection * moveSpeed * Time.fixedDeltaTime));
    }

    public void SetNewTileTarget(Vector3 pos)
    {
        isWalkToTile = true;
        tilePosition = pos;
    }

    public void ResetMovement()
    {
        isWalkToTile = !isWalkToTile;
    }

    public void TakingDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetDoT(float duration, float damageValue)
    {
        if(duration > dotDuration)
        {
            dotDuration = duration;
        }

        if(damageValue > dotDamage)
        {
            dotDamage = damageValue;
        }
    }

    private void DamageOverTime()
    {
        if(dotDuration >= 0)
        {

            dotTimer -= Time.deltaTime;
            if(dotTimer <= 0)
            {
                TakingDamage(dotDamage);
                dotTimer = dotInterval;
            }
            dotDuration -= Time.deltaTime;
        }
        else
        {
            dotTimer = dotInterval;
        }
    }

    private void LookToCommander()
    {
        lookDirection = detectedCommander.transform.position - transform.position;
        lookDirection.Normalize();
        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        visual.rotation = Quaternion.Euler(0f, 0f, angle);
        lookDirection.Normalize();
    }
    
    private void Shoot()
    {
        if(detectedCommander == null)
        {
            shootTimer = shootInterval;
        }
        else
        {
            if(shootTimer > 0)
            {
                shootTimer -= Time.deltaTime;
            }
            else
            {
                shootTimer = shootInterval;
                GameObject projectile = Instantiate(projectilePrefab);
                projectile.transform.position = firePoint.position;
                projectile.GetComponent<Projectile>().SetProperty(detectedCommander.transform, projectileSpeed, projectileDamage);
            }
        }
    }
}

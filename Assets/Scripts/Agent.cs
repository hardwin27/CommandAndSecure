using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private Rigidbody2D body;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public List<Transform> detectedEnemies = new List<Transform>();
    private Vector3 lookDirection;

    [SerializeField] private int photonCost = 10;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float shootInterval = 1;
    [SerializeField] private float projectileSpeed = 5;
    [SerializeField] private float projectileDamage = 2;
    private float shootTimer;

    public Vector2? placedPosition { get; private set; }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        lookDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        shootTimer = shootInterval;
    }

    private void Update()
    {
        Shoot();
    }

    private void FixedUpdate()
    {
        RotateAgents();
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonUp(1))
        {
            GameManager.Instance.AddPhoton(photonCost / 2);
            GameManager.Instance.AddAgent(-1);
            Destroy(gameObject);
        }
    }

    private void RotateAgents()
    {
        if(detectedEnemies.Count > 0)
        {
            lookDirection = detectedEnemies[0].position - transform.position;
        }

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        body.rotation = angle;
        lookDirection.Normalize();
    }

    private void Shoot()
    {
        if(detectedEnemies.Count <= 0)
        {
            shootTimer = shootInterval;
        }
        else
        {
            if (shootTimer > 0)
            {
                shootTimer -= Time.deltaTime;
            }
            else
            {
                shootTimer = shootInterval;
                GameObject projectile = Instantiate(projectilePrefab);
                projectile.transform.position = firePoint.position;
                projectile.GetComponent<Projectile>().SetProperty(detectedEnemies[0].transform, projectileSpeed, projectileDamage);
            }
        }
    }

    public int GetPhotonCost()
    {
        return photonCost;
    }

    public Sprite GetAgentIcon()
    {
        return spriteRenderer.sprite;
    }

    public void SetPlacedPosition(Vector2? newPosition)
    {
        placedPosition = newPosition;
    }

    public void LockPlacement()
    {
        transform.position = (Vector2)placedPosition;
    }

    public void ToggleOrderInLayer(bool toFront)
    {
        int order = toFront ? 2 : 1;
        spriteRenderer.sortingOrder = order;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private Rigidbody2D body;

    private List<Transform> detectedEnemies = new List<Transform>();
    private Vector3 lookDirection;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float shootInterval = 1;
    [SerializeField] private float projectileSpeed = 5;
    [SerializeField] private float projectileDamage = 2;
    private float shootTimer;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedEnemies.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Transform temp = collision.transform;
        if (detectedEnemies.FindIndex(t => t == temp) != -1)
        {
            detectedEnemies.Remove(temp);
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
}

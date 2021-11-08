using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D body;

    private Vector3 lookDirection;
    private float angle;

    [SerializeField] private float moveSpeed = 1.5f;
    private Vector2 moveDirection;

    [SerializeField] private EnemyTileDetector tileDetector;
    private bool isWalkToTile;
    private Vector3 tilePosition;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isWalkToTile = false;
        tilePosition = transform.position;
    }

    private void Update()
    {
        RotateCharacter();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void RotateCharacter()
    {
        if(Vector3.Distance(transform.position, tilePosition) < 0.1f)
        {
            isWalkToTile = false;
        }

        if(isWalkToTile)
        {
            lookDirection = tilePosition - transform.position;
        }
        else
        {
            lookDirection = tileDetector.GetDetectedDirection();
        }

        if (lookDirection != Vector3.zero)
        {
            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            body.rotation = angle;
            lookDirection.Normalize();
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
}

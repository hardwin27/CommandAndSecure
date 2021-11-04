using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D body;

    private Vector3 lookDirection;
    private float angle;

    [SerializeField] private float moveSpeed = 1.5f;
    private Vector2 moveDirection;

    [SerializeField] private EnemyTileDetectorController tileDetector;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
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
        lookDirection = tileDetector.GetDetectedDirection();

        if(lookDirection != Vector3.zero)
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
}
